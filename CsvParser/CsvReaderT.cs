// Copyright (c) 2019-2020 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.IO;
using System.Text;

namespace SoftCircuits.CsvParser
{
    /// <summary>
    /// Class to read from a CSV file with automatic mapping from CSV columns
    /// to object properties.
    /// </summary>
    /// <typeparam name="T">The object type being read.</typeparam>
    public class CsvReader<T> : CsvReader where T : class, new()
    {
        private ColumnInfoCollection<T> ColumnsInfo;
        private string[] Columns;

        /// <summary>
        /// Returns the number of columns for the last row successfully read.
        /// Can be 0.
        /// </summary>
        public int ColumnCount => Columns.Length;

        /// <summary>
        /// Initializes a new instance of the CsvReader class for the specified file name.
        /// </summary>
        /// <param name="path">The name of the CSV file to read.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvReader(string path, CsvSettings settings = null)
            : base(path, settings)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the CsvReader class for the specified file name,
        /// with the specified character encoding.
        /// </summary>
        /// <param name="path">The name of the CSV file to read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvReader(string path, Encoding encoding, CsvSettings settings = null)
            : base(path, encoding, settings)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the CsvReader class for the specified file name,
        /// with the specified byte order mark detection option.
        /// </summary>
        /// <param name="path">The name of the CSV file to read.</param>
        /// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at
        /// <param name="settings">Optional custom settings.</param>
        public CsvReader(string path, bool detectEncodingFromByteOrderMarks, CsvSettings settings = null)
            : base(path, detectEncodingFromByteOrderMarks, settings)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the CsvReader class for the specified file name,
        /// with the specified character encoding and byte order mark detection option.
        /// </summary>
        /// <param name="path">The name of the CSV file to read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at
        /// <param name="settings">Optional custom settings.</param>
        public CsvReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, CsvSettings settings = null)
            : base(path, encoding, detectEncodingFromByteOrderMarks, settings)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the CsvReader class.
        /// </summary>
        /// <param name="stream">The stream to be read.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvReader(Stream stream, CsvSettings settings = null)
            : base(stream, settings)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the CsvReader class for the specified stream,
        /// with the specified character encoding.
        /// </summary>
        /// <param name="stream">The stream to be read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvReader(Stream stream, Encoding encoding, CsvSettings settings = null)
            : base(stream, encoding, settings)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the CsvReader class for the specified stream,
        /// with the specified byte order mark detection option.
        /// </summary>
        /// <param name="stream">The stream to be read.</param>
        /// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at
        /// the beginning of the file.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvReader(Stream stream, bool detectEncodingFromByteOrderMarks, CsvSettings settings = null)
            : base(stream, detectEncodingFromByteOrderMarks, settings)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the CsvReader class for the specified stream,
        /// with the specified character encoding and byte order mark detection option.
        /// </summary>
        /// <param name="stream">The stream to be read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at
        /// the beginning of the file.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, CsvSettings settings = null)
            : base(stream, encoding, detectEncodingFromByteOrderMarks, settings)
        {
            Initialize();
        }

        /// <summary>
        /// Common initialization.
        /// </summary>
        private void Initialize()
        {
            ColumnsInfo = new ColumnInfoCollection<T>();
            Columns = null;
        }

        /// <summary>
        /// Applies <see cref="ColumnMaps{T}"></see> mappings to the reader.
        /// </summary>
        public void MapColumns<TMaps>() where TMaps : ColumnMaps<T>, new()
        {
            TMaps columnMaps = Activator.CreateInstance<TMaps>();
            ColumnsInfo.ApplyColumnMaps(columnMaps.GetCustomMaps());
        }

        /// <summary>
        /// Reads a row of columns from the input stream. If <paramref name="mapColumnsFromHeaders"/>
        /// is true, the column headers are used to map columns to class members.
        /// </summary>
        /// <param name="mapColumnsFromHeaders">Specifies whether the column headers
        /// should be used to map columns to class members.</param>
        /// <returns>True if successful, false if the end of the file was reached.</returns>
        public bool ReadHeaders(bool mapColumnsFromHeaders)
        {
            if (ReadRow(ref Columns))
            {
                // Will delete all column mapping if headers are empty
                if (mapColumnsFromHeaders)
                    ColumnsInfo.ApplyHeaders(Columns, Settings.ColumnHeaderStringComparison);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Reads an item from the input stream.
        /// </summary>
        /// <param name="item">Receives the item read.</param>
        /// <returns>True if successful, false if the end of the file was reached.</returns>
        public bool Read(out T item)
        {
            if (ReadRow(ref Columns))
            {
                item = Activator.CreateInstance<T>();
                foreach (ColumnInfo column in ColumnsInfo.FilteredColumns)
                {
                    if (column.Index < Columns.Length)
                        column.SetValue(item, Columns[column.Index], Settings.InvalidDataRaisesException);
                }
                return true;
            }
            item = null;
            return false;
        }
    }
}
