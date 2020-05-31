// Copyright (c) 2019-2020 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

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
        private int CurrPos;

        /// <summary>
        /// Gets or sets whether the underlying stream is left open after the
        /// <see cref="CsvReader"/> object is disposed. If <c>false</c> (the
        /// default), the underlying stream is also disposed.
        /// </summary>
        public bool LeaveStreamOpen { get; set; }

        /// <summary>
        /// Initializes a new <see cref="CsvReader"/> instance for the specified file.
        /// </summary>
        /// <param name="path">The name of the CSV file to read.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvReader(string path, CsvSettings settings = null)
        {
            Reader = new StreamReader(path);
            Settings = settings ?? new CsvSettings();
            LeaveStreamOpen = false;
        }

        /// <summary>
        /// Initializes a new <see cref="CsvReader"/> instance for the specified file,
        /// with the specified byte order mark detection option.
        /// </summary>
        /// <param name="path">The name of the CSV file to read.</param>
        /// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at
        /// the beginning of the file.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvReader(string path, bool detectEncodingFromByteOrderMarks, CsvSettings settings = null)
        {
            Reader = new StreamReader(path, detectEncodingFromByteOrderMarks);
            Settings = settings ?? new CsvSettings();
            LeaveStreamOpen = false;
        }

        /// <summary>
        /// Initializes a new <see cref="CsvReader"/> instance for the specified file,
        /// with the specified character encoding.
        /// </summary>
        /// <param name="path">The name of the CSV file to read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvReader(string path, Encoding encoding, CsvSettings settings = null)
        {
            Reader = new StreamReader(path, encoding);
            Settings = settings ?? new CsvSettings();
            LeaveStreamOpen = false;
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
        public CsvReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, CsvSettings settings = null)
        {
            Reader = new StreamReader(path, encoding, detectEncodingFromByteOrderMarks);
            Settings = settings ?? new CsvSettings();
            LeaveStreamOpen = false;
        }

        /// <summary>
        /// Initializes a new <see cref="CsvReader"/> instance for the specified stream.
        /// </summary>
        /// <param name="stream">The stream to be read.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvReader(Stream stream, CsvSettings settings = null)
        {
            Reader = new StreamReader(stream);
            Settings = settings ?? new CsvSettings();
            LeaveStreamOpen = false;
        }

        /// <summary>
        /// Initializes a new <see cref="CsvReader"/> instance for the specified stream,
        /// with the specified byte order mark detection option.
        /// </summary>
        /// <param name="stream">The stream to be read.</param>
        /// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at
        /// the beginning of the file.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvReader(Stream stream, bool detectEncodingFromByteOrderMarks, CsvSettings settings = null)
        {
            Reader = new StreamReader(stream, detectEncodingFromByteOrderMarks);
            Settings = settings ?? new CsvSettings();
            LeaveStreamOpen = false;
        }

        /// <summary>
        /// Initializes a new <see cref="CsvReader"/> instance for the specified stream,
        /// with the specified character encoding.
        /// </summary>
        /// <param name="stream">The stream to be read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// the beginning of the file.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvReader(Stream stream, Encoding encoding, CsvSettings settings = null)
        {
            Reader = new StreamReader(stream, encoding);
            Settings = settings ?? new CsvSettings();
            LeaveStreamOpen = false;
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
        public CsvReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, CsvSettings settings = null)
        {
            Reader = new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks);
            Settings = settings ?? new CsvSettings();
            LeaveStreamOpen = false;
        }

        /// <summary>
        /// Reads a row of columns from the current CSV file. Returns false if no
        /// more data could be read because the end of the file was reached.
        /// </summary>
        /// <param name="columns">Array to hold the columns read. Okay if it's <c>null</c>.</param>
        public bool ReadRow(ref string[] columns)
        {
            ArrayManager<string> results = new ArrayManager<string>(columns);

        ReadNextLine:
            // Read next line from the file
            Line = Reader.ReadLine();
            CurrPos = 0;

            // Test for end of file
            if (Line == null)
                return false;

            // Test for empty line
            if (Line.Length == 0)
            {
                switch (Settings.EmptyLineBehavior)
                {
                    case EmptyLineBehavior.NoColumns:
                        columns = new string[0];
                        return true;
                    case EmptyLineBehavior.Ignore:
                        goto ReadNextLine;
                    case EmptyLineBehavior.EndOfFile:
                        return false;
                }
            }

            // Parse values in this line
            string column;
            while (true)
            {
                // Read next column
                if (CurrPos < Line.Length && Line[CurrPos] == Settings.QuoteCharacter)
                    column = ReadQuotedColumn();
                else
                    column = ReadUnquotedColumn();
                // Add column to list
                results.Add(column);
                // Break if we reached the end of the line
                if (Line == null || CurrPos == Line.Length)
                    break;
                // Otherwise skip delimiter
                Debug.Assert(Line[CurrPos] == Settings.ColumnDelimiter);
                CurrPos++;
            }

            // Returns results
            columns = results.GetResults();

            // Indicate success
            return true;
        }

        /// <summary>
        /// Reads a quoted column by reading from the current line until a
        /// closing quote is found or the end of the file is reached. On return,
        /// the current position points to the delimiter or the end of the last
        /// line in the file. Note: Line may be set to null on return.
        /// </summary>
        private string ReadQuotedColumn()
        {
            // Skip opening quote character
            Debug.Assert(CurrPos < Line.Length && Line[CurrPos] == Settings.QuoteCharacter);
            CurrPos++;

            // Parse column
            StringBuilder builder = new StringBuilder();
            while (true)
            {
                while (CurrPos == Line.Length)
                {
                    // End of line so attempt to read the next line
                    Line = Reader.ReadLine();
                    CurrPos = 0;
                    // Done if we reached the end of the file
                    if (Line == null)
                        return builder.ToString();
                    // Otherwise, treat as a multi-line field
                    builder.Append(Environment.NewLine);
                }

                // Test for quote character
                if (Line[CurrPos] == Settings.QuoteCharacter)
                {
                    // If two quotes, skip first and treat second as literal
                    int nextPos = (CurrPos + 1);
                    if (nextPos < Line.Length && Line[nextPos] == Settings.QuoteCharacter)
                        CurrPos++;
                    else
                        break;  // Single quote ends quoted sequence
                }
                // Add current character to the column
                builder.Append(Line[CurrPos++]);
            }

            if (CurrPos < Line.Length)
            {
                // Consume closing quote
                Debug.Assert(Line[CurrPos] == Settings.QuoteCharacter);
                CurrPos++;
                // Append any additional characters appearing before next delimiter
                builder.Append(ReadUnquotedColumn());
            }
            // Return column value
            return builder.ToString();
        }

        /// <summary>
        /// Reads an unquoted column by reading from the current line until a
        /// delimiter is found or the end of the line is reached. On return, the
        /// current position points to the delimiter or the end of the current
        /// line.
        /// </summary>
        private string ReadUnquotedColumn()
        {
            int startPos = CurrPos;
            CurrPos = Line.IndexOf(Settings.ColumnDelimiter, CurrPos);
            if (CurrPos == -1)
                CurrPos = Line.Length;
            return Line.Substring(startPos, CurrPos - startPos);
        }

        /// <summary>
        /// Closes the CsvReader object and the underlying stream, and releases
        /// any resources used.
        /// </summary>
        public void Close() => Reader.Close();

        /// <summary>
        /// Releases all resources used by the CsvReader object.
        /// </summary>
        public void Dispose()
        {
            if (!LeaveStreamOpen)
                Reader.Dispose();
        }
    }
}
