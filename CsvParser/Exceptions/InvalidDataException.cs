// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace SoftCircuits.CsvParser
{
    /// <summary>
    /// Exception that indicates a column read from a file could not be converted to the class
    /// property it was mapped to. Resolve by correcting the data, excluding the column or by
    /// implementing your own custom data converter.
    /// </summary>
    public class InvalidDataException : Exception
    {
        public InvalidDataException()
        {
        }

        public InvalidDataException(string data, string member, string memberType)
            : base($"Unable to convert the string {((data != null) ? $"\"{data}\"" : "(null)")} to class member '{member}' (type {memberType}).")
        {
        }
    }
}
