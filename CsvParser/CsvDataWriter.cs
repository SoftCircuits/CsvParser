// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.IO;
using System.Text;

namespace SoftCircuits.CsvParser
{
    [Obsolete("This class will be removed in future versions. Please use 'CsvWriter<T>' instead.", false)]
    /// <summary>
    /// Class to write to a CSV file with automatic mapping from object properties
    /// to CSV columns.
    /// </summary>
    /// <typeparam name="T">The object type being written.</typeparam>
    public class CsvDataWriter<T> : CsvWriter<T> where T : class, new()
    {
        /// <summary>
        /// Initializes a new instance of the CsvDataWriter class for the specified file
        /// using the default character encoding.
        /// </summary>
        /// <param name="path">The name of the CSV file to write to.</param>
        /// <param name="settings">Optional custom settings.</param>
        public CsvDataWriter(string path, CsvSettings settings = null)
            : base(path, settings)
        {
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
        }
    }
}
