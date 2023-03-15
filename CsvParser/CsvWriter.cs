// Copyright (c) 2019-2023 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SoftCircuits.CsvParser
{
    /// <summary>
    /// Class for writing to comma-separated-value (CSV) files.
    /// </summary>
    public class CsvWriter : IDisposable
    {
        // Private members
        private readonly StreamWriter Writer;
        protected CsvSettings Settings;

        /// <summary>
        /// Gets or sets whether the underlying stream is left open after the
        /// <see cref="CsvWriter"/> object is disposed. If <c>false</c> (the
        /// default), the underlying stream is also disposed.
        /// </summary>
        public bool LeaveStreamOpen { get; set; }

        /// <summary>
        /// Initializes a new <see cref="CsvWriter"/> instance for the specified file
        /// using the default character encoding.
        /// </summary>
        /// <param name="path">The name of the CSV file to write to.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvWriter(string path, CsvSettings? settings = null)
        {
            Writer = new(path);
            Settings = settings ?? new();
            LeaveStreamOpen = false;
        }

        /// <summary>
        /// Initializes a new <see cref="CsvWriter"/> instance for the specified file
        /// using the specified character encoding.
        /// </summary>
        /// <param name="path">The name of the CSV file to write to.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvWriter(string path, Encoding encoding, CsvSettings? settings = null)
        {
            Writer = new(path, false, encoding);
            Settings = settings ?? new();
            LeaveStreamOpen = false;
        }

        /// <summary>
        /// Initializes a new <see cref="CsvWriter"/> instance for the specified stream
        /// using UTF-8 character encoding.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvWriter(Stream stream, CsvSettings? settings = null)
        {
            Writer = new(stream);
            Settings = settings ?? new();
            LeaveStreamOpen = false;
        }

        /// <summary>
        /// Initializes a new <see cref="CsvWriter"/> instance for the specified stream
        /// using the specified character encoding.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvWriter(Stream stream, Encoding encoding, CsvSettings? settings = null)
        {
            Writer = new(stream, encoding);
            Settings = settings ?? new();
            LeaveStreamOpen = false;
        }

        /// <summary>
        /// Writes a row of columns to the current CSV file.
        /// </summary>
        /// <param name="columns">The list of columns to write.</param>
        public void Write(params string[] columns) => Write(columns as IEnumerable<string>);

        /// <summary>
        /// Writes a row of columns to the current CSV file.
        /// </summary>
        /// <param name="columns">The list of columns to write.</param>
        public void Write(IEnumerable<string?> columns)
        {
            // Verify required argument
            if (columns == null)
                throw new ArgumentNullException(nameof(columns));

            // Write each column
            bool firstColumn = true;
            foreach (string? value in columns)
            {
                // Add delimiter if this isn't the first column
                if (firstColumn)
                    firstColumn = false;
                else
                    Writer.Write(Settings.ColumnDelimiter);
                // Write column value
                Writer.Write(Settings.CsvEncode(value ?? string.Empty));
            }
            Writer.WriteLine();
        }

        /// <summary>
        /// Asynchronously writes a row of columns to the current CSV file.
        /// </summary>
        /// <param name="columns">The list of columns to write.</param>
        public async Task WriteAsync(params string[] columns) => await WriteAsync(columns);

        /// <summary>
        /// Asynchronously writes a row of columns to the current CSV file.
        /// </summary>
        /// <param name="columns">The list of columns to write.</param>
        public async Task WriteAsync(IEnumerable<string?> columns)
        {
            // Verify required argument
            if (columns == null)
                throw new ArgumentNullException(nameof(columns));

            // Write each column
            bool firstColumn = true;
            foreach (string? value in columns)
            {
                // Add delimiter if this isn't the first column
                if (firstColumn)
                    firstColumn = false;
                else
                    await Writer.WriteAsync(Settings.ColumnDelimiter);
                // Write column value
                await Writer.WriteAsync(Settings.CsvEncode(value ?? string.Empty));
            }
            await Writer.WriteLineAsync();
        }

        #region Legacy

        /// <summary>
        /// Writes a row of columns to the current CSV file.
        /// </summary>
        /// <param name="columns">The list of columns to write.</param>
        public void WriteRow(IEnumerable<string?> columns) => Write(columns);

        /// <summary>
        /// Writes a row of columns to the current CSV file.
        /// </summary>
        /// <param name="columns">The list of columns to write.</param>
        public void WriteRow(params string[] columns) => Write(columns as IEnumerable<string>);

        /// <summary>
        /// Asynchronously writes a row of columns to the current CSV file.
        /// </summary>
        /// <param name="columns">The list of columns to write.</param>
        public async Task WriteRowAsync(IEnumerable<string?> columns) => await WriteAsync(columns);

        /// <summary>
        /// Asynchronously writes a row of columns to the current CSV file.
        /// </summary>
        /// <param name="columns">The list of columns to write.</param>
        public async Task WriteRowAsync(params string[] columns) => await WriteAsync(columns);


        #endregion

        /// <summary>
        /// Clears all buffers and causes any unbuffered data to be written to the underlying stream.
        /// </summary>
        public void Flush() => Writer.Flush();

        /// <summary>
        /// Closes the current CsvWriter and the underlying stream.
        /// </summary>
        public void Close() => Writer.Close();

        /// <summary>
        /// Releases resources used by the <see cref="CsvWriter"/> object. Leaves the stream
        /// open if <see cref="LeaveStreamOpen"/> is true.
        /// </summary>
        public void Dispose()
        {
            if (LeaveStreamOpen)
            {
                Flush();
            }
            else
            {
                Writer.Dispose();
                GC.SuppressFinalize(this);
            }
        }
    }
}
