// Copyright (c) 2019-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using CsvParser.Helpers;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SoftCircuits.CsvParser
{
    /// <summary>
    /// Class for reading from comma-separated-value (CSV) files.
    /// </summary>
    public class CsvReader : IDisposable
    {
        private readonly StreamReader Reader;
        private readonly CharBuffer Buffer;
        private readonly LineBuffer Line;
        private readonly FastStringBuilder QuotedStringBuilder;
        private readonly ColumnCollection InternalColumns;

        /// <summary>
        /// Current reader settings.
        /// </summary>
        protected CsvSettings Settings;

        /// <summary>
        /// Gets the columns values read during the last successful call to <see cref="Read()"/> or
        /// <see cref="ReadAsync()"/>.
        /// </summary>
        public string[]? Columns { get; private set; }

        /// <summary>
        /// Gets or sets whether the underlying stream is left open after the
        /// <see cref="CsvReader"/> object is disposed. If <c>false</c> (the
        /// default), the underlying stream is also disposed.
        /// </summary>
        public bool LeaveStreamOpen { get; set; }

        /// <summary>
        /// Gets a value that indicates whether the current file position is at the
        /// end of the file.
        /// </summary>
        public bool EndOfFile => Reader.EndOfStream;

        /// <summary>
        /// Initializes a new <see cref="CsvReader"/> instance for the specified file.
        /// </summary>
        /// <param name="path">The name of the CSV file to read.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvReader(string path, CsvSettings? settings = null)
            : this(path, Encoding.UTF8, true, settings)
        {
        }

        /// <summary>
        /// Initializes a new <see cref="CsvReader"/> instance for the specified file,
        /// with the specified byte order mark detection option.
        /// </summary>
        /// <param name="path">The name of the CSV file to read.</param>
        /// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at
        /// the beginning of the file.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvReader(string path, bool detectEncodingFromByteOrderMarks, CsvSettings? settings = null)
            : this(path, Encoding.UTF8, detectEncodingFromByteOrderMarks, settings)
        {
        }

        /// <summary>
        /// Initializes a new <see cref="CsvReader"/> instance for the specified file,
        /// with the specified character encoding.
        /// </summary>
        /// <param name="path">The name of the CSV file to read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvReader(string path, Encoding encoding, CsvSettings? settings = null)
            : this(path, encoding, true, settings)
        {
        }

        /// <summary>
        /// Initializes a new <see cref="CsvReader"/> instance for the specified file,
        /// with the specified character encoding and byte order mark detection option.
        /// </summary>
        /// <param name="path">The name of the CSV file to read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at
        /// the beginning of the file.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, CsvSettings? settings = null)
        {
            Reader = new(path, encoding, detectEncodingFromByteOrderMarks);
            Settings = settings ?? new();
            Buffer = new(Settings.BufferSize);
            Line = new();
            QuotedStringBuilder = new();
            InternalColumns = new();
            Columns = null;
            LeaveStreamOpen = false;
        }

        /// <summary>
        /// Initializes a new <see cref="CsvReader"/> instance for the specified stream.
        /// </summary>
        /// <param name="stream">The stream to be read.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvReader(Stream stream, CsvSettings? settings = null)
            : this(stream, Encoding.UTF8, false, settings)
        {
        }

        /// <summary>
        /// Initializes a new <see cref="CsvReader"/> instance for the specified stream,
        /// with the specified byte order mark detection option.
        /// </summary>
        /// <param name="stream">The stream to be read.</param>
        /// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at
        /// the beginning of the file.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvReader(Stream stream, bool detectEncodingFromByteOrderMarks, CsvSettings? settings = null)
            : this(stream, Encoding.UTF8, detectEncodingFromByteOrderMarks, settings)
        {
        }

        /// <summary>
        /// Initializes a new <see cref="CsvReader"/> instance for the specified stream,
        /// with the specified character encoding.
        /// </summary>
        /// <param name="stream">The stream to be read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvReader(Stream stream, Encoding encoding, CsvSettings? settings = null)
            : this(stream, encoding, false, settings)
        {
        }

        /// <summary>
        /// Initializes a new <see cref="CsvReader"/> instance for the specified stream,
        /// with the specified character encoding and byte order mark detection option.
        /// </summary>
        /// <param name="stream">The stream to be read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at
        /// the beginning of the file.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, CsvSettings? settings = null)
        {
            Reader = new(stream, encoding, detectEncodingFromByteOrderMarks);
            Settings = settings ?? new();
            Buffer = new(Settings.BufferSize);
            Line = new();
            QuotedStringBuilder = new();
            InternalColumns = new();
            Columns = null;
            LeaveStreamOpen = false;
        }

        /// <summary>
        /// Reads a record from the current file. If successful, this method returns true and the
        /// <see cref="Columns"/> property contains the columns that were read. Returns false if
        /// the end of the file was reached.
        /// </summary>
        /// <returns>True if a record was read, false if the end of the file was reached.</returns>
#if !NETSTANDARD2_0
        [MemberNotNullWhen(true, nameof(Columns))]
#endif
        public bool Read()
        {
            Columns = null;
            InternalColumns.Reset();

        ReadNextLine:
            if (!ReadLine())
                return false;

            // Test for empty line
            if (Line.Length == 0)
            {
                switch (Settings.EmptyLineBehavior)
                {
                    case EmptyLineBehavior.NoColumns:
                        Columns = Array.Empty<string>();
                        return true;
                    case EmptyLineBehavior.Ignore:
                        goto ReadNextLine;
                    case EmptyLineBehavior.EndOfFile:
                        return false;
                }
            }

            // Parse values in this line
            while (true)
            {
                // Read next column
                if (Line.Position < Line.Length && Line[Line.Position] == Settings.QuoteCharacter)
                    InternalColumns.Append(ReadQuotedColumn());
                else
                    InternalColumns.Append(ReadUnquotedColumn());
                // Break if we reached the end of the line
                if (Line.Position >= Line.Length)
                    break;
                // Otherwise skip delimiter
                Debug.Assert(Line[Line.Position] == Settings.ColumnDelimiter);
                Line.Position++;
            }

            // Return results
            Columns = InternalColumns.TrimUnused();
            return true;
        }

        /// <summary>
        /// Asynchronously reads a record from the current file. If successful, this method returns true and the
        /// <see cref="Columns"/> property contains the columns that were read. Returns false if the end of the
        /// file was reached.
        /// </summary>
        /// <returns>True if a record was read, false if the end of the file was reached.</returns>
#if !NETSTANDARD2_0
        [MemberNotNullWhen(true, nameof(Columns))]
#endif
        public async Task<bool> ReadAsync()
        {
            Columns = null;
            InternalColumns.Reset();

        ReadNextLine:
            if (!await ReadLineAsync())
                return false;

            // Test for empty line
            if (Line.Length == 0)
            {
                switch (Settings.EmptyLineBehavior)
                {
                    case EmptyLineBehavior.NoColumns:
                        Columns = Array.Empty<string>();
                        return true;
                    case EmptyLineBehavior.Ignore:
                        goto ReadNextLine;
                    case EmptyLineBehavior.EndOfFile:
                        return false;
                }
            }

            // Parse values in this line
            while (true)
            {
                // Read next column
                if (Line.Position < Line.Length && Line[Line.Position] == Settings.QuoteCharacter)
                    InternalColumns.Append(await ReadQuotedColumnAsync());
                else
                    InternalColumns.Append(ReadUnquotedColumn());
                // Break if we reached the end of the line
                if (Line.Position >= Line.Length)
                    break;
                // Otherwise skip delimiter
                Debug.Assert(Line[Line.Position] == Settings.ColumnDelimiter);
                Line.Position++;
            }

            // Return results
            Columns = InternalColumns.TrimUnused();
            return true;
        }

        #region Legacy

        /// <summary>
        /// Reads a record from the current file. Returns false if no more data could be read because the
        /// end of the file was reached.
        /// </summary>
        /// <param name="columns">Array to hold the columns read. Okay if it's <c>null</c>.</param>
        /// <remarks>
        /// This method is deprecated in favor of using <see cref="Read()"/> and then using the <see cref="Columns"/>
        /// property to read the values read. However, this method has not been marked as obsolete because the
        /// recommended method does not exactly match this method's functionality.
        /// </remarks>
#if !NETSTANDARD2_0
        public bool ReadRow([NotNullWhen(true)] ref string[]? columns)
#else
        public bool ReadRow(ref string[]? columns)
#endif
        {
            bool result = Read();
            columns = Columns;
            return result;
        }

        /// <summary>
        /// Reads a record from the current file. Returns null if at the end of the file.
        /// </summary>
        /// <returns>The column values read or null if the at the end of the file.</returns>
        /// <remarks>
        /// This method is deprecated in favor of using <see cref="Read()"/> and then using the <see cref="Columns"/>
        /// property to read the values read. However, this method has not been marked as obsolete because the
        /// recommended method does not exactly match this method's functionality.
        /// </remarks>
        public string[]? ReadRow() => Read() ? Columns : null;

        /// <summary>
        /// Asynchronously reads a record from the current file. Returns null if at the end of the file.
        /// </summary>
        /// <returns>The column values read or null if the at the end of the file.</returns>
        [Obsolete("This method is deprecated and will be removed in a future version. Please use ReadAsync() instead.")]
        public async Task<string[]?> ReadRowAsync() => await ReadAsync() ? Columns : null;

        #endregion

        /// <summary>
        /// Reads the next line from the input stream and resets the current line
        /// position.
        /// </summary>
        /// <returns>True if the next line was read, false if the end of the stream
        /// was reached.</returns>
        protected bool ReadLine()
        {
            Line.Reset();

            if (Buffer.Position >= Buffer.Length)
            {
                if (!Buffer.Read(Reader))
                    return false;
            }

            while (true)
            {
                int pos = Buffer.IndexOfNewLine(Buffer.Position);

                if (pos >= 0)
                {
                    // If the entire line is within one block of data, we simply reference
                    // that part of the buffer instead of copying the data
                    if (Line.Length == 0)
                        Line.SetExternalBuffer(Buffer, Buffer.Position, pos - Buffer.Position);
                    else
                        Line.Append(Buffer, Buffer.Position, pos - Buffer.Position);

                    Buffer.Position = pos + 1;

                    // Check for multi-character new line
                    if (Buffer[pos] == '\r' && (Buffer.Position < Buffer.Length || Buffer.Read(Reader, Line)))
                    {
                        if (Buffer[Buffer.Position] == '\n')
                            Buffer.Position++;
                    }
                    return true;
                }
                else
                {
                    Line.Append(Buffer, Buffer.Position, Buffer.Length - Buffer.Position);
                    if (!Buffer.Read(Reader))
                        return true;
                }
            }
        }

        /// <summary>
        /// Asynchronously reads the next line from the input stream and resets the current line
        /// position.
        /// </summary>
        /// <returns>True if the next line was read, false if the end of the stream
        /// was reached.</returns>
        protected async Task<bool> ReadLineAsync()
        {
            Line.Reset();

            if (Buffer.Position >= Buffer.Length)
            {
                if (!await Buffer.ReadAsync(Reader))
                    return false;
            }

            while (true)
            {
                int pos = Buffer.IndexOfNewLine(Buffer.Position);

                if (pos >= 0)
                {
                    // If the entire line is within one block of data, we simply reference
                    // that part of the buffer instead of copying the data
                    if (Line.Length == 0)
                        Line.SetExternalBuffer(Buffer, Buffer.Position, pos - Buffer.Position);
                    else
                        Line.Append(Buffer, Buffer.Position, pos - Buffer.Position);

                    Buffer.Position = pos + 1;

                    // Check for multi-character new line
                    if (Buffer[pos] == '\r' && (Buffer.Position < Buffer.Length || Buffer.Read(Reader, Line)))
                    {
                        if (Buffer[Buffer.Position] == '\n')
                            Buffer.Position++;
                    }
                    return true;
                }
                else
                {
                    Line.Append(Buffer, Buffer.Position, Buffer.Length - Buffer.Position);
                    if (!await Buffer.ReadAsync(Reader))
                        return true;
                }
            }
        }

        /// <summary>
        /// Reads a quoted column by reading from the current line until a
        /// closing quote is found or the end of the file is reached. On return,
        /// the current position points to the delimiter or the end of the last
        /// line in the file. Note: Line may be set to null on return.
        /// </summary>
        protected string ReadQuotedColumn()
        {
            // Skip opening quote character
            Debug.Assert(Line.Position < Line.Length && Line[Line.Position] == Settings.QuoteCharacter);
            Line.Position++;

            QuotedStringBuilder.Clear();
            int pos = Line.Position;

            while (true)
            {
                pos = Line.IndexOf(Settings.QuoteCharacter, pos);

                if (pos >= 0)
                {
                    QuotedStringBuilder.Append(Line, Line.Position, pos - Line.Position);
                    Line.Position = ++pos;

                    // Two quotes: skip the first and treat the second as a literal
                    // One quote: end of the sequence
                    if (pos < Line.Length && Line[pos] == Settings.QuoteCharacter)
                        pos++;
                    else
                        break;
                }
                else
                {
                    do
                    {
                        // No ending quote: field extends to next line
                        QuotedStringBuilder.Append(Line, Line.Position, Line.Length - Line.Position);

                        if (ReadLine())
                        {
                            // ReadLine() resets position
                            pos = Line.Position;
                            QuotedStringBuilder.Append(Environment.NewLine);
                        }
                        else
                        {
                            // End of file
                            return QuotedStringBuilder.ToString();
                        }
                    } while (pos >= Line.Length);
                }
            }

            Debug.Assert(pos == Line.Position);

            // Skip any additional characters before next delimiter
            if (pos < Line.Length && Line[pos] != Settings.ColumnDelimiter)
            {
                pos = Line.IndexOf(Settings.ColumnDelimiter, pos + 1);
                if (pos == -1)
                    pos = Line.Length;
                Line.Position = pos;
            }

            // Return column value
            return QuotedStringBuilder.ToString();
        }

        /// <summary>
        /// Asynchronously reads a quoted column by reading from the current line until a
        /// closing quote is found or the end of the file is reached. On return,
        /// the current position points to the delimiter or the end of the last
        /// line in the file. Note: Line may be set to null on return.
        /// </summary>
        protected async Task<string> ReadQuotedColumnAsync()
        {
            // Skip opening quote character
            Debug.Assert(Line.Position < Line.Length && Line[Line.Position] == Settings.QuoteCharacter);
            Line.Position++;

            QuotedStringBuilder.Clear();
            int pos = Line.Position;

            while (true)
            {
                pos = Line.IndexOf(Settings.QuoteCharacter, pos);

                if (pos >= 0)
                {
                    QuotedStringBuilder.Append(Line, Line.Position, pos - Line.Position);
                    Line.Position = ++pos;

                    // Two quotes: skip the first and treat the second as a literal
                    // One quote: end of the sequence
                    if (pos < Line.Length && Line[pos] == Settings.QuoteCharacter)
                        pos++;
                    else
                        break;
                }
                else
                {
                    do
                    {
                        // No ending quote: field extends to next line
                        QuotedStringBuilder.Append(Line, Line.Position, Line.Length - Line.Position);

                        if (await ReadLineAsync())
                        {
                            // ReadLine() resets position
                            pos = Line.Position;
                            QuotedStringBuilder.Append(Environment.NewLine);
                        }
                        else
                        {
                            // End of file
                            return QuotedStringBuilder.ToString();
                        }
                    } while (pos >= Line.Length);
                }
            }

            Debug.Assert(pos == Line.Position);

            // Skip any additional characters before next delimiter
            if (pos < Line.Length && Line[pos] != Settings.ColumnDelimiter)
            {
                pos = Line.IndexOf(Settings.ColumnDelimiter, pos + 1);
                if (pos == -1)
                    pos = Line.Length;
                Line.Position = pos;
            }

            // Return column value
            return QuotedStringBuilder.ToString();
        }

        /// <summary>
        /// Reads an unquoted column by reading from the current line until a
        /// delimiter is found or the end of the line is reached. On return, the
        /// current position points to the delimiter or the end of the current
        /// line.
        /// </summary>
        protected string ReadUnquotedColumn()
        {
            int start = Line.Position;
            int end = Line.IndexOf(Settings.ColumnDelimiter, start);
            if (end == -1)
                end = Line.Length;
            Line.Position = end;
            return Line.Substring(start, end - start);
        }

        /// <summary>
        /// Closes the CsvReader object and the underlying stream, and releases
        /// any resources used.
        /// </summary>
        public void Close() => Reader.Close();

        /// <summary>
        /// Releases resources used by this CsvReader object. Leaves the stream open
        /// if <see cref="LeaveStreamOpen"/> is true.
        /// </summary>
        public void Dispose()
        {
            if (!LeaveStreamOpen)
            {
                Reader.Dispose();
                GC.SuppressFinalize(this);
            }
        }
    }
}
