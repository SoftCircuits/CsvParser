// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Diagnostics;
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
        // Private members
        private readonly StreamReader Reader;
        protected CsvSettings Settings;

        private string Line;
        private int LinePosition;
        private StringBuilder? StringBuilder;
        private readonly GrowableArray<string> InternalColumns;

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
        /// Returns the columns read on the last call to <see cref="ReadRow()"/> or
        /// <see cref="ReadRowAsync()"/>.
        /// </summary>
        public string[] Columns => InternalColumns;

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
            Line = string.Empty;
            LinePosition = 0;
            StringBuilder = null;
            InternalColumns = new();
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
        /// the beginning of the file.</param>
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
            Line = string.Empty;
            LinePosition = 0;
            StringBuilder = null;
            InternalColumns = new();
            LeaveStreamOpen = false;
        }

        /// <summary>
        /// Reads a row of columns from the current CSV file. Returns false if no
        /// more data could be read because the end of the file was reached.
        /// </summary>
        /// <param name="columns">Array to hold the columns read. Okay if it's <c>null</c>.</param>
        public bool ReadRow(ref string[]? columns)
        {
            columns = ReadRow();
            return columns != null;
        }

        /// <summary>
        /// Reads a row of columns from the current CSV file. Returns null if at the end of the file.
        /// </summary>
        /// <returns>The column values read or null if the at the end of the file.</returns>
        public string[]? ReadRow()
        {
            InternalColumns.Clear();

            // Read line
            while (true)
            {
                if (!ReadLine())
                    return null;

                // Handle empty line
                if (Line.Length == 0)
                {
                    switch (Settings.EmptyLineBehavior)
                    {
                        case EmptyLineBehavior.NoColumns:
                            return Array.Empty<string>();
                        case EmptyLineBehavior.Ignore:
                            continue;
                        case EmptyLineBehavior.EndOfFile:
                            return null;
                        default:
                            break;
                    }
                }
                break;
            }

            // Parse column values
            while (true)
            {
                // Read next column
                if (LinePosition < Line.Length && Line[LinePosition] == Settings.QuoteCharacter)
                {
                    if (StringBuilder != null)
                        StringBuilder.Clear();
                    else
                        StringBuilder = new();

                    LinePosition++;
                    while (ReadQuotedColumn(StringBuilder))
                    {
                        if (!ReadLine())
                            break;
                        StringBuilder.AppendLine();
                    }
                    InternalColumns.Append(StringBuilder.ToString());
                }
                else
                {
                    InternalColumns.Append(ReadUnquotedColumn());
                }

                // Skip delimiter if any more columns
                if (LinePosition < Line.Length)
                {
                    Debug.Assert(Line[LinePosition] == Settings.ColumnDelimiter);
                    LinePosition++;
                }
                else break;
            }

            // Set results
            return Columns;
        }

        /// <summary>
        /// Asynchronously reads a row of columns from the current CSV file. Returns null if at the end of the file.
        /// </summary>
        /// <returns>The column values read or null if the at the end of the file.</returns>
        public async Task<string[]?> ReadRowAsync()
        {
            InternalColumns.Clear();

            // Read line
            while (true)
            {
                if (!await ReadLineAsync())
                    return null;

                // Handle empty line
                if (Line.Length == 0)
                {
                    switch (Settings.EmptyLineBehavior)
                    {
                        case EmptyLineBehavior.NoColumns:
                            return Array.Empty<string>();
                        case EmptyLineBehavior.Ignore:
                            continue;
                        case EmptyLineBehavior.EndOfFile:
                            return null;
                        default:
                            break;
                    }
                }
                break;
            }

            // Parse column values
            while (true)
            {
                // Read next column
                if (LinePosition < Line.Length && Line[LinePosition] == Settings.QuoteCharacter)
                {
                    if (StringBuilder != null)
                        StringBuilder.Clear();
                    else
                        StringBuilder = new();

                    LinePosition++;
                    while (ReadQuotedColumn(StringBuilder))
                    {
                        if (!await ReadLineAsync())
                            break;
                        StringBuilder.AppendLine();
                    }
                    InternalColumns.Append(StringBuilder.ToString());
                }
                else
                {
                    InternalColumns.Append(ReadUnquotedColumn());
                }

                // Skip delimiter if any more columns
                if (LinePosition < Line.Length)
                {
                    Debug.Assert(Line[LinePosition] == Settings.ColumnDelimiter);
                    LinePosition++;
                }
                else break;
            }

            // Set results
            return Columns;
        }

        /// <summary>
        /// Reads the next line from the input stream and resets the current line
        /// position.
        /// </summary>
        /// <returns>True if the next line was read, false if the end of the stream
        /// was reached.</returns>
        private bool ReadLine()
        {
            string? line = Reader.ReadLine();
            if (line != null)
            {
                Line = line;
                LinePosition = 0;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Reads one line asynchronously from the current file.
        /// </summary>
        /// <returns>True if a line was read. False if the end of the file was reached.</returns>
        private async Task<bool> ReadLineAsync()
        {
            string? line = await Reader.ReadLineAsync();
            if (line != null)
            {
                Line = line;
                LinePosition = 0;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Reads a quoted column by reading from the current line until a
        /// closing quote is found or the end of the file is reached. On return,
        /// the current position points to the delimiter or the end of the last
        /// line in the file. Note: Line may be set to null on return.
        /// </summary>
        private bool ReadQuotedColumn(StringBuilder builder)
        {
            while (true)
            {
                // End of line was reached without ending quote
                // Indicate we need another line
                while (LinePosition == Line.Length)
                    return true;

                // Test for quote character
                if (Line[LinePosition] == Settings.QuoteCharacter)
                {
                    // If two quotes, skip first and treat second as literal
                    int nextPos = (LinePosition + 1);
                    if (nextPos < Line.Length && Line[nextPos] == Settings.QuoteCharacter)
                        LinePosition++;
                    else
                        break;  // Single quote ends quoted sequence
                }
                // Add current character to the column
                builder.Append(Line[LinePosition++]);
            }

            // Consume closing quote
            Debug.Assert(LinePosition < Line.Length);
            Debug.Assert(Line[LinePosition] == Settings.QuoteCharacter);
            LinePosition++;

            // Append any additional characters appearing before next delimiter
            builder.Append(ReadUnquotedColumn());

            // Indicate we are finished
            return false;
        }

        /// <summary>
        /// Reads an unquoted column by reading from the current line until a
        /// delimiter is found or the end of the line is reached. On return, the
        /// current position points to the delimiter or the end of the current
        /// line.
        /// </summary>
        private string ReadUnquotedColumn()
        {
            int startPos = LinePosition;
            LinePosition = Line.IndexOf(Settings.ColumnDelimiter, LinePosition);
            if (LinePosition == -1)
                LinePosition = Line.Length;
#if !NETSTANDARD2_0
            return Line[startPos..LinePosition];
#else
            return Line.Substring(startPos, LinePosition - startPos);
#endif
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
