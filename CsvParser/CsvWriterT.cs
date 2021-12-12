// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftCircuits.CsvParser
{
    /// <summary>
    /// Class to write to a CSV file with automatic mapping between class properties
    /// and CSV columns.
    /// </summary>
    /// <typeparam name="T">The type being written.</typeparam>
    public class CsvWriter<T> : CsvWriter where T : class, new()
    {
        private readonly ColumnInfoCollection<T> ColumnsInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvWriter{T}"></see> class for the
        /// specified file using the default character encoding.
        /// </summary>
        /// <param name="path">The name of the CSV file to write to.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvWriter(string path, CsvSettings? settings = null)
            : base(path, settings)
        {
            ColumnsInfo = new();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvWriter{T}"></see> class for the
        /// specified file using the specified character encoding.
        /// </summary>
        /// <param name="path">The name of the CSV file to write to.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvWriter(string path, Encoding encoding, CsvSettings? settings = null)
            : base(path, encoding, settings)
        {
            ColumnsInfo = new();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvWriter{T}"></see> class for the
        /// specified stream using the default character encoding.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvWriter(Stream stream, CsvSettings? settings = null)
            : base(stream, settings)
        {
            ColumnsInfo = new();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvWriter{T}"></see> class for the
        /// specified stream using the specified character encoding.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvWriter(Stream stream, Encoding encoding, CsvSettings? settings = null)
            : base(stream, encoding, settings)
        {
            ColumnsInfo = new();
        }

        /// <summary>
        /// Applies <see cref="ColumnMaps{T}"></see> mappings to the writer.
        /// </summary>
        public void MapColumns<TMaps>() where TMaps : ColumnMaps<T>, new()
        {
            TMaps columnMaps = Activator.CreateInstance<TMaps>();
            ColumnsInfo.ApplyColumnMaps(columnMaps.GetCustomMaps());
        }

        /// <summary>
        /// Writes column headers to the output stream.
        /// </summary>
        public void WriteHeaders()
        {
            WriteRow(ColumnsInfo.FilteredColumns.Select(fc => fc.Name));
        }

        /// <summary>
        /// Asynchronously writes column headers to the output stream.
        /// </summary>
        public async Task WriteHeadersAsync()
        {
            await WriteRowAsync(ColumnsInfo.FilteredColumns.Select(fc => fc.Name));
        }

        /// <summary>
        /// Writes the specified item to the output stream.
        /// </summary>
        /// <param name="item">The item to write.</param>
        public void Write(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            WriteRow(ColumnsInfo.FilteredColumns.Select(fc => fc.GetValue(item)));
        }

        /// <summary>
        /// Asyncrhonously writes the specified item to the output stream.
        /// </summary>
        /// <param name="item">The item to write.</param>
        public async Task WriteAsync(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            await WriteRowAsync(ColumnsInfo.FilteredColumns.Select(fc => fc.GetValue(item)));
        }

        /// <summary>
        /// Writes the specified items to the output stream.
        /// </summary>
        /// <param name="items">The items to write.</param>
        public void Write(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            foreach (T item in items)
                Write(item);
        }

        /// <summary>
        /// Asynchronously writes the specified items to the output stream.
        /// </summary>
        /// <param name="items">The items to write.</param>
        public async Task WriteAsync(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            foreach (T item in items)
                await WriteAsync(item);
        }
    }
}
