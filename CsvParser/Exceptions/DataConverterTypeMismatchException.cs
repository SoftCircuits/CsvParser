// Copyright (c) 2019-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace SoftCircuits.CsvParser
{
    /// <summary>
    /// This exception indicates a custom data converter was assigned to a property, but
    /// the converter data type did not match the property data type. To resolve this
    /// error, ensure that all custom data converters must be for the same type as the
    /// property they are assigned to.
    /// </summary>
    public class DataConverterTypeMismatchException : Exception
    {
        public DataConverterTypeMismatchException()
        {
        }

        public DataConverterTypeMismatchException(string propertyName, Type propertyType, Type converterType)
            : base($"A custom data converter for type '{converterType.FullName}' was assigned to the property '{propertyName}', which is of type '{propertyType.FullName}'.")
        {
        }
    }
}
