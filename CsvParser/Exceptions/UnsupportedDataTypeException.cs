// Copyright (c) 2019-2020 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace SoftCircuits.CsvParser
{
    /// <summary>
    /// Exception that indicates a class property that was mapped to a column was not of a
    /// supported data type. Resolve by excluding the column or by implementing your own
    /// custom data converter.
    /// </summary>
    public class UnsupportedDataTypeException : Exception
    {
        public UnsupportedDataTypeException()
        {
        }

        public UnsupportedDataTypeException(string typeName)
            : base($"The type '{typeName}' has no built-in conversion support, and no custom data converter has been specified for the class property.")
        {
        }
    }
}
