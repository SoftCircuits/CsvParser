// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SoftCircuits.CsvParser
{
    /// <summary>
    /// Class to write to a CSV file with automatic mapping from object properties
    /// to CSV columns.
    /// </summary>
    /// <typeparam name="T">The object type being written.</typeparam>
    public class CsvDataWriter<T> : CsvWriter where T : class, new()
    {
        private ColumnInfoCollection ColumnsInfo;
        private ColumnInfo[] MappedColumnsInfo;
        private string[] Columns;

        /// <summary>
        /// Initializes a new instance of the CsvDataWriter class for the specified file
        /// using the default character encoding.
        /// </summary>
        /// <param name="path">The name of the CSV file to write to.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvDataWriter(string path, CsvSettings settings = null)
            : base(path, settings)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the CsvDataWriter class for the specified file
        /// using the specified character encoding.
        /// </summary>
        /// <param name="path">The name of the CSV file to write to.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvDataWriter(string path, Encoding encoding, CsvSettings settings = null)
            : base(path, encoding, settings)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the CsvDataWriter class for the specified stream
        /// using the default character encoding.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvDataWriter(Stream stream, CsvSettings settings = null)
            : base(stream, settings)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the CsvDataWriter class for the specified stream
        /// using the specified character encoding.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvDataWriter(Stream stream, Encoding encoding, CsvSettings settings = null)
            : base(stream, encoding, settings)
        {
            Initialize();
        }

        /// <summary>
        /// Common initialization.
        /// </summary>
        private void Initialize()
        {
            ColumnsInfo = new ColumnInfoCollection();
            ColumnsInfo.BuildColumnInfoCollection<T>();
            MappedColumnsInfo = ColumnsInfo.SortAndFilter();
            Columns = null;
        }

        /// <summary>
        /// Applies <see cref="ColumnMaps{T}"></see> mappings to the writer.
        /// </summary>
        public void MapColumns<TMaps>() where TMaps : ColumnMaps<T>, new()
        {
            TMaps columnMaps = Activator.CreateInstance<TMaps>();
            MappedColumnsInfo = ColumnsInfo.ApplyColumnMaps(columnMaps.GetCustomMaps());
        }

        /// <summary>
        /// Writes column headers to the output stream.
        /// </summary>
        public void WriteHeaders()
        {
            Debug.Assert(MappedColumnsInfo != null);

            // Ensure correct number of columns
            if (Columns == null || Columns.Length != MappedColumnsInfo.Length)
                Columns = new string[MappedColumnsInfo.Length];

            for (int i = 0; i < Columns.Length; i++)
                Columns[i] = MappedColumnsInfo[i].Name;

            WriteRow(Columns);
        }

        /// <summary>
        /// Writes the specified item to the output stream.
        /// </summary>
        /// <param name="item">The item to write.</param>
        public void Write(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            Debug.Assert(MappedColumnsInfo != null);

            // Ensure correct number of columns
            if (Columns == null || Columns.Length != MappedColumnsInfo.Length)
                Columns = new string[MappedColumnsInfo.Length];

            for (int i = 0; i < Columns.Length; i++)
                Columns[i] = MappedColumnsInfo[i].GetValue(item);

            WriteRow(Columns);
        }

        /// <summary>
        /// Writes the specified items to the output stream.
        /// </summary>
        /// <param name="items">The items to write.</param>
        public void Write(IEnumerable<T> items)
        {
            foreach (T item in items)
                Write(item);
        }
    }
}
