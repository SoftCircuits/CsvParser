// Copyright (c) 2019-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace SoftCircuits.CsvParser
{
    /// <summary>
    /// Exception that indicates a column read from a file could not be converted to the class
    /// property it was mapped to. Resolve by correcting the data, excluding the column,
    /// implementing your own custom data converter or by setting
    /// <see cref="CsvSettings.InvalidDataRaisesException"/> to <c>false</c>.
    /// </summary>
    public class BadDataFormatException : Exception
    {
        /// <summary>
        /// Creates a <see cref="BadDataFormatException"/> instance.
        /// </summary>
        public BadDataFormatException()
        {
        }

        /// <summary>
        /// Creates a <see cref="BadDataFormatException"/> instance.
        /// </summary>
        public BadDataFormatException(string data, string member, Type memberType)
            : base($"Unable to convert the string {((data != null) ? $"\"{data}\"" : "(null)")} to class member '{member}' ({memberType.FullName ?? "(Unknown Type)"}).")
        {
        }
    }
}
