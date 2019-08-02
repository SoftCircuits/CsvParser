// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("CsvParserTests")]

namespace SoftCircuits.CsvParser
{
    internal static class DataConverters
    {
        private static readonly Dictionary<Type, Func<IDataConverter>> ConverterLookup = new Dictionary<Type, Func<IDataConverter>>
        {
            [typeof(string)] = () => new StringConverter(),
            [typeof(string[])] = () => new StringArrayConverter(),
            [typeof(char)] = () => new CharConverter(),
            [typeof(char[])] = () => new CharArrayConverter(),
            [typeof(char?)] = () => new NullableCharConverter(),
            [typeof(char?[])] = () => new NullableCharArrayConverter(),
            [typeof(bool)] = () => new BooleanConverter(),
            [typeof(bool[])] = () => new BooleanArrayConverter(),
            [typeof(bool?)] = () => new NullableBooleanConverter(),
            [typeof(bool?[])] = () => new NullableBooleanArrayConverter(),
            [typeof(byte)] = () => new ByteConverter(),
            [typeof(byte[])] = () => new ByteArrayConverter(),
            [typeof(byte?)] = () => new NullableByteConverter(),
            [typeof(byte?[])] = () => new NullableByteArrayConverter(),
            [typeof(sbyte)] = () => new SByteConverter(),
            [typeof(sbyte[])] = () => new SByteArrayConverter(),
            [typeof(sbyte?)] = () => new NullableSByteConverter(),
            [typeof(sbyte?[])] = () => new NullableSByteArrayConverter(),
            [typeof(short)] = () => new Int16Converter(),
            [typeof(short[])] = () => new Int16ArrayConverter(),
            [typeof(short?)] = () => new NullableInt16Converter(),
            [typeof(short?[])] = () => new NullableInt16ArrayConverter(),
            [typeof(ushort)] = () => new UInt16Converter(),
            [typeof(ushort[])] = () => new UInt16ArrayConverter(),
            [typeof(ushort?)] = () => new NullableUInt16Converter(),
            [typeof(ushort?[])] = () => new NullableUInt16ArrayConverter(),
            [typeof(int)] = () => new Int32Converter(),
            [typeof(int[])] = () => new Int32ArrayConverter(),
            [typeof(int?)] = () => new NullableInt32Converter(),
            [typeof(int?[])] = () => new NullableInt32ArrayConverter(),
            [typeof(uint)] = () => new UInt32Converter(),
            [typeof(uint[])] = () => new UInt32ArrayConverter(),
            [typeof(uint?)] = () => new NullableUInt32Converter(),
            [typeof(uint?[])] = () => new NullableUInt32ArrayConverter(),
            [typeof(long)] = () => new Int64Converter(),
            [typeof(long[])] = () => new Int64ArrayConverter(),
            [typeof(long?)] = () => new NullableInt64Converter(),
            [typeof(long?[])] = () => new NullableInt64ArrayConverter(),
            [typeof(ulong)] = () => new UInt64Converter(),
            [typeof(ulong[])] = () => new UInt64ArrayConverter(),
            [typeof(ulong?)] = () => new NullableUInt64Converter(),
            [typeof(ulong?[])] = () => new NullableUInt64ArrayConverter(),
            [typeof(float)] = () => new SingleConverter(),
            [typeof(float[])] = () => new SingleArrayConverter(),
            [typeof(float?)] = () => new NullableSingleConverter(),
            [typeof(float?[])] = () => new NullableSingleArrayConverter(),
            [typeof(double)] = () => new DoubleConverter(),
            [typeof(double[])] = () => new DoubleArrayConverter(),
            [typeof(double?)] = () => new NullableDoubleConverter(),
            [typeof(double?[])] = () => new NullableDoubleArrayConverter(),
            [typeof(decimal)] = () => new DecimalConverter(),
            [typeof(decimal[])] = () => new DecimalArrayConverter(),
            [typeof(decimal?)] = () => new NullableDecimalConverter(),
            [typeof(decimal?[])] = () => new NullableDecimalArrayConverter(),
            [typeof(Guid)] = () => new GuidConverter(),
            [typeof(Guid[])] = () => new GuidArrayConverter(),
            [typeof(Guid?)] = () => new NullableGuidConverter(),
            [typeof(Guid?[])] = () => new NullableGuidArrayConverter(),
            [typeof(DateTime)] = () => new DateTimeConverter(),
            [typeof(DateTime[])] = () => new DateTimeArrayConverter(),
            [typeof(DateTime?)] = () => new NullableDateTimeConverter(),
            [typeof(DateTime?[])] = () => new NullableDateTimeArrayConverter(),
        };

        /// <summary>
        /// Returns the data converter for the specified type. Returns an instance
        /// of <see cref="UnsupportedConverter"></see> if there are no matching
        /// types.
        /// </summary>
        /// <param name="type">The type to find a converter for.</param>
        /// <returns>Returns a class that derives from
        /// <see cref="DataConverter{T}"></see>.</returns>
        public static IDataConverter GetConverter(Type type)
        {
            return ConverterLookup.TryGetValue(type, out Func<IDataConverter> func) ?
                func() :
                new UnsupportedTypeConverter(type);
        }
    }
}
