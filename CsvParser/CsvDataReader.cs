// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.IO;
using System.Text;

namespace SoftCircuits.CsvParser
{
    [Obsolete("This class will be removed in future versions. Please use 'CsvReader<T>' instead.", false)]
    /// <summary>
    /// Class to read from a CSV file with automatic mapping from CSV columns
    /// to object properties.
    /// </summary>
    /// <typeparam name="T">The object type being read.</typeparam>
    public class CsvDataReader<T> : CsvReader<T> where T : class, new()
    {
        /// <summary>
        /// Initializes a new instance of the CsvDataReader class for the specified file name.
        /// </summary>
        /// <param name="path">The name of the CSV file to read.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvDataReader(string path, CsvSettings? settings = null)
            : base(path, settings)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CsvDataReader class for the specified file name,
        /// with the specified character encoding.
        /// </summary>
        /// <param name="path">The name of the CSV file to read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvDataReader(string path, Encoding encoding, CsvSettings? settings = null)
            : base(path, encoding, settings)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CsvDataReader class for the specified file name,
        /// with the specified byte order mark detection option.
        /// </summary>
        /// <param name="path">The name of the CSV file to read.</param>
        /// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at
        /// <param name="settings">Optional custom settings.</param>
        public CsvDataReader(string path, bool detectEncodingFromByteOrderMarks, CsvSettings? settings = null)
            : base(path, detectEncodingFromByteOrderMarks, settings)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CsvDataReader class for the specified file name,
        /// with the specified character encoding and byte order mark detection option.
        /// </summary>
        /// <param name="path">The name of the CSV file to read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at
        /// <param name="settings">Optional custom settings.</param>
        public CsvDataReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, CsvSettings? settings = null)
            : base(path, encoding, detectEncodingFromByteOrderMarks, settings)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CsvDataReader class.
        /// </summary>
        /// <param name="stream">The stream to be read.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvDataReader(Stream stream, CsvSettings? settings = null)
            : base(stream, settings)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CsvDataReader class for the specified stream,
        /// with the specified character encoding.
        /// </summary>
        /// <param name="stream">The stream to be read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvDataReader(Stream stream, Encoding encoding, CsvSettings? settings = null)
            : base(stream, encoding, settings)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CsvDataReader class for the specified stream,
        /// with the specified byte order mark detection option.
        /// </summary>
        /// <param name="stream">The stream to be read.</param>
        /// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at
        /// the beginning of the file.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvDataReader(Stream stream, bool detectEncodingFromByteOrderMarks, CsvSettings? settings = null)
            : base(stream, detectEncodingFromByteOrderMarks, settings)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CsvDataReader class for the specified stream,
        /// with the specified character encoding and byte order mark detection option.
        /// </summary>
        /// <param name="stream">The stream to be read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at
        /// the beginning of the file.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvDataReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, CsvSettings? settings = null)
            : base(stream, encoding, detectEncodingFromByteOrderMarks, settings)
        {
        }
    }
}
