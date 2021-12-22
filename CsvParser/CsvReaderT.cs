// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SoftCircuits.CsvParser
{
    /// <summary>
    /// Class to read from a CSV file with automatic mapping between class properties
    /// and CSV columns.
    /// </summary>
    /// <typeparam name="T">The type being read.</typeparam>
    public class CsvReader<T> : CsvReader where T : class, new()
    {
        private readonly ColumnInfoCollection<T> ColumnsInfo;

        /// <summary>
        /// Returns the number of columns for the last row successfully read.
        /// Can be 0.
        /// </summary>
        public int ColumnCount => Columns?.Length ?? 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvReader{T}"></see> class for the
        /// specified file name.
        /// </summary>
        /// <param name="path">The name of the CSV file to read.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvReader(string path, CsvSettings? settings = null)
            : this(path, Encoding.UTF8, true, settings)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvReader{T}"></see> class for the
        /// specified file name, with the specified character encoding.
        /// </summary>
        /// <param name="path">The name of the CSV file to read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvReader(string path, Encoding encoding, CsvSettings? settings = null)
            : this(path, encoding, true, settings)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvReader{T}"></see> class for the
        /// specified file name, with the specified byte order mark detection option.
        /// </summary>
        /// <param name="path">The name of the CSV file to read.</param>
        /// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for
        /// byte order marks at
        /// <param name="settings">Optional custom settings.</param>
        public CsvReader(string path, bool detectEncodingFromByteOrderMarks, CsvSettings? settings = null)
            : this(path, Encoding.UTF8, detectEncodingFromByteOrderMarks, settings)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvReader{T}"></see> class for the
        /// specified file name, with the specified character encoding and byte order mark
        /// detection option.
        /// </summary>
        /// <param name="path">The name of the CSV file to read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at
        /// <param name="settings">Optional custom settings.</param>
        public CsvReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, CsvSettings? settings = null)
            : base(path, encoding, detectEncodingFromByteOrderMarks, settings)
        {
            ColumnsInfo = new();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvReader{T}"></see> class.
        /// </summary>
        /// <param name="stream">The stream to be read.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvReader(Stream stream, CsvSettings? settings = null)
            : this(stream, Encoding.UTF8, true, settings)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvReader{T}"></see> class for the
        /// specified stream, with the specified character encoding.
        /// </summary>
        /// <param name="stream">The stream to be read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvReader(Stream stream, Encoding encoding, CsvSettings? settings = null)
            : this(stream, encoding, true, settings)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvReader{T}"></see> class for the
        /// specified stream, with the specified byte order mark detection option.
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
        /// Initializes a new instance of the <see cref="CsvReader{T}"></see> class for the
        /// specified stream, with the specified character encoding and byte order mark
        /// detection option.
        /// </summary>
        /// <param name="stream">The stream to be read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at
        /// the beginning of the file.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, CsvSettings? settings = null)
            : base(stream, encoding, detectEncodingFromByteOrderMarks, settings)
        {
            ColumnsInfo = new();
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
        /// is <c>true</c>, the column headers are used to map columns to class members.
        /// </summary>
        /// <param name="mapColumnsFromHeaders">Specifies whether the column headers
        /// should be used to map columns to class members.</param>
        /// <returns>True if successful, false if the end of the file was reached.</returns>
        public bool ReadHeaders(bool mapColumnsFromHeaders)
        {
            string[]? columns = ReadRow();
            if (columns != null)
            {
                // Will exclude all column mapping if headers are empty
                if (mapColumnsFromHeaders)
                    ColumnsInfo.ApplyHeaders(columns, Settings.ColumnHeaderStringComparison);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Asynchronously reads a row of columns from the input stream. If <paramref name="mapColumnsFromHeaders"/>
        /// is <c>true</c>, the column headers are used to map columns to class members.
        /// </summary>
        /// <param name="mapColumnsFromHeaders">Specifies whether the column headers
        /// should be used to map columns to class members.</param>
        /// <returns>True if successful, false if the end of the file was reached.</returns>
        public async Task<bool> ReadHeadersAsync(bool mapColumnsFromHeaders)
        {
            string[]? columns = await ReadRowAsync();
            if (columns != null)
            {
                // Will exclude all column mapping if headers are empty
                if (mapColumnsFromHeaders)
                    ColumnsInfo.ApplyHeaders(columns, Settings.ColumnHeaderStringComparison);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Reads an item from the input stream.
        /// </summary>
        /// <param name="item">Receives the item read.</param>
        /// <returns>True if successful, false if the end of the file was reached.</returns>
#if !NETSTANDARD2_0
        public bool Read([NotNullWhen(true)] out T? item)
#else
        public bool Read(out T? item)
#endif
        {
            item = Read();
            return item != null;
        }

        /// <summary>
        /// Reads an item from the input stream.
        /// </summary>
        /// <param name="item">Receives the item read.</param>
        /// <returns>True if successful, false if the end of the file was reached.</returns>
#if !NETSTANDARD2_0
        public T? Read()
#else
        public T Read()
#endif
        {
            string[]? columns = ReadRow();
            if (columns != null)
            {
                T item = Activator.CreateInstance<T>();
                foreach (ColumnInfo column in ColumnsInfo.FilteredColumns)
                {
                    if (column.Index < columns!.Length)
                        column.SetValue(item, columns[column.Index], Settings.InvalidDataRaisesException);
                }
                return item;
            }
            return null;
        }

        /// <summary>
        /// Asynchronously reads an item from the input stream.
        /// </summary>
        /// <param name="item">Receives the item read.</param>
        /// <returns>True if successful, false if the end of the file was reached.</returns>
#if !NETSTANDARD2_0
        public async Task<T?> ReadAsync()
#else
        public async Task<T> ReadAsync()
#endif
        {
            string[]? columns = await ReadRowAsync();
            if (columns != null)
            {
                T item = Activator.CreateInstance<T>();
                foreach (ColumnInfo column in ColumnsInfo.FilteredColumns)
                {
                    if (column.Index < columns!.Length)
                        column.SetValue(item, columns[column.Index], Settings.InvalidDataRaisesException);
                }
                return item;
            }
            return null;
        }
    }
}
