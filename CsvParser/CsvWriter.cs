// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Collections.Generic;
using System.IO;

namespace SoftCircuits.CsvParser
{
    /// <summary>
    /// Class for writing to comma-separated-value (CSV) files.
    /// </summary>
    public class CsvWriter : IDisposable
    {
        // Private members
        private StreamWriter Writer;
        private CsvSettings Settings;

        /// <summary>
        /// Initializes a new instance of the CsvWriter class for the
        /// specified stream.
        /// </summary>
        /// <param name="stream">The stream to write to</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvWriter(Stream stream, CsvSettings settings = null)
        {
            Writer = new StreamWriter(stream);
            Settings = settings ?? new CsvSettings();
        }

        /// <summary>
        /// Initializes a new instance of the CsvWriter class for the
        /// specified file path.
        /// </summary>
        /// <param name="path">The name of the CSV file to write to</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvWriter(string path, CsvSettings settings = null)
        {
            Writer = new StreamWriter(path);
            Settings = settings ?? new CsvSettings();
        }

        /// <summary>
        /// Writes a row of columns to the current CSV file.
        /// </summary>
        /// <param name="columns">The list of columns to write.</param>
        public void WriteRow(params string[] columns)
        {
            WriteRow((IEnumerable<string>)columns);
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
                // Write this column
                Writer.Write(Settings.CsvEncode(value));
            }
            Writer.WriteLine();
        }

        /// <summary>
        /// Clears all buffers and causes any unbuffered data to be written to the underlying stream.
        /// </summary>
        public void Flush() => Writer.Flush();

        // Propagate Dispose to StreamWriter
        public void Dispose()
        {
            Writer.Dispose();
        }
    }
}
