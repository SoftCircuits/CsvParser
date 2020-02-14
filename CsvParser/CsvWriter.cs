// Copyright (c) 2019-2020 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SoftCircuits.CsvParser
{
    /// <summary>
    /// Class for writing to comma-separated-value (CSV) files.
    /// </summary>
    public class CsvWriter : IDisposable
    {
        // Private members
        private StreamWriter Writer;
        protected CsvSettings Settings;

        // Used for formatting quoted columns
        private string OneQuoteString = null;
        private string TwoQuoteString = null;

        /// <summary>
        /// Initializes a new instance of the CsvWriter class for the specified file
        /// using the default character encoding.
        /// </summary>
        /// <param name="path">The name of the CSV file to write to.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvWriter(string path, CsvSettings settings = null)
        {
            Writer = new StreamWriter(path);
            Settings = settings ?? new CsvSettings();
        }

        /// <summary>
        /// Initializes a new instance of the CsvWriter class for the specified file
        /// using the specified character encoding.
        /// </summary>
        /// <param name="path">The name of the CSV file to write to.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvWriter(string path, Encoding encoding, CsvSettings settings = null)
        {
            Writer = new StreamWriter(path, false, encoding);
            Settings = settings ?? new CsvSettings();
        }

        /// <summary>
        /// Initializes a new instance of the CsvWriter class for the specified stream
        /// using UTF-8 character encoding.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvWriter(Stream stream, CsvSettings settings = null)
        {
            Writer = new StreamWriter(stream);
            Settings = settings ?? new CsvSettings();
        }

        /// <summary>
        /// Initializes a new instance of the CsvWriter class for the specified stream
        /// using the specified character encoding.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvWriter(Stream stream, Encoding encoding, CsvSettings settings = null)
        {
            Writer = new StreamWriter(stream, encoding);
            Settings = settings ?? new CsvSettings();
        }

        /// <summary>
        /// Writes a row of columns to the current CSV file.
        /// </summary>
        /// <param name="columns">The list of columns to write.</param>
        public void WriteRow(params string[] columns)
        {
            WriteRow(columns as IEnumerable<string>);
        }

        /// <summary>
        /// Writes a row of columns to the current CSV file.
        /// </summary>
        /// <param name="columns">The list of columns to write.</param>
        public void WriteRow(IEnumerable<string> columns)
        {
            // Verify required argument
            if (columns == null)
                throw new ArgumentNullException(nameof(columns));

            // Write each column
            bool firstColumn = true;
            foreach (string value in columns)
            {
                // Add delimiter if this isn't the first column
                if (firstColumn)
                    firstColumn = false;
                else
                    Writer.Write(Settings.ColumnDelimiter);
                // Write column value
                Writer.Write(CsvEncode(value));
            }
            Writer.WriteLine();
        }

        /// <summary>
        /// Encodes the given string as needed to be valid within the CSV file.
        /// </summary>
        internal string CsvEncode(string s)
        {
            if (!Settings.HasSpecialCharacter(s))
                return s;
            // Ensure we're using current quote character
            if (OneQuoteString == null || OneQuoteString[0] != Settings.QuoteCharacter)
            {
                OneQuoteString = Settings.QuoteCharacter.ToString();
                TwoQuoteString = string.Format("{0}{0}", Settings.QuoteCharacter);
            }
            return string.Format("{0}{1}{0}", Settings.QuoteCharacter, s.Replace(OneQuoteString, TwoQuoteString));
        }

        /// <summary>
        /// Clears all buffers and causes any unbuffered data to be written to the underlying stream.
        /// </summary>
        public void Flush() => Writer.Flush();

        /// <summary>
        /// Closes the current CsvWriter and the underlying stream.
        /// </summary>
        public void Close() => Writer.Close();

        /// <summary>
        /// Releases all resources used by the CsvWriter object.
        /// </summary>
        public void Dispose() => Writer.Dispose();
    }
}
