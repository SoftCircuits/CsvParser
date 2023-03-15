// Copyright (c) 2019-2023 Jonathan Wood (www.softcircuits.com)
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
            [typeof(Nullable<char>)] = () => new NullableCharConverter(),
            [typeof(Nullable<char>[])] = () => new NullableCharArrayConverter(),
            [typeof(bool)] = () => new BooleanConverter(),
            [typeof(bool[])] = () => new BooleanArrayConverter(),
            [typeof(Nullable<bool>)] = () => new NullableBooleanConverter(),
            [typeof(Nullable<bool>[])] = () => new NullableBooleanArrayConverter(),
            [typeof(byte)] = () => new ByteConverter(),
            [typeof(byte[])] = () => new ByteArrayConverter(),
            [typeof(Nullable<byte>)] = () => new NullableByteConverter(),
            [typeof(Nullable<byte>[])] = () => new NullableByteArrayConverter(),
            [typeof(sbyte)] = () => new SByteConverter(),
            [typeof(sbyte[])] = () => new SByteArrayConverter(),
            [typeof(Nullable<sbyte>)] = () => new NullableSByteConverter(),
            [typeof(Nullable<sbyte>[])] = () => new NullableSByteArrayConverter(),
            [typeof(short)] = () => new Int16Converter(),
            [typeof(short[])] = () => new Int16ArrayConverter(),
            [typeof(Nullable<short>)] = () => new NullableInt16Converter(),
            [typeof(Nullable<short>[])] = () => new NullableInt16ArrayConverter(),
            [typeof(ushort)] = () => new UInt16Converter(),
            [typeof(ushort[])] = () => new UInt16ArrayConverter(),
            [typeof(Nullable<ushort>)] = () => new NullableUInt16Converter(),
            [typeof(Nullable<ushort>[])] = () => new NullableUInt16ArrayConverter(),
            [typeof(int)] = () => new Int32Converter(),
            [typeof(int[])] = () => new Int32ArrayConverter(),
            [typeof(Nullable<int>)] = () => new NullableInt32Converter(),
            [typeof(Nullable<int>[])] = () => new NullableInt32ArrayConverter(),
            [typeof(uint)] = () => new UInt32Converter(),
            [typeof(uint[])] = () => new UInt32ArrayConverter(),
            [typeof(Nullable<uint>)] = () => new NullableUInt32Converter(),
            [typeof(Nullable<uint>[])] = () => new NullableUInt32ArrayConverter(),
            [typeof(long)] = () => new Int64Converter(),
            [typeof(long[])] = () => new Int64ArrayConverter(),
            [typeof(Nullable<long>)] = () => new NullableInt64Converter(),
            [typeof(Nullable<long>[])] = () => new NullableInt64ArrayConverter(),
            [typeof(ulong)] = () => new UInt64Converter(),
            [typeof(ulong[])] = () => new UInt64ArrayConverter(),
            [typeof(Nullable<ulong>)] = () => new NullableUInt64Converter(),
            [typeof(Nullable<ulong>[])] = () => new NullableUInt64ArrayConverter(),
            [typeof(float)] = () => new SingleConverter(),
            [typeof(float[])] = () => new SingleArrayConverter(),
            [typeof(Nullable<float>)] = () => new NullableSingleConverter(),
            [typeof(Nullable<float>[])] = () => new NullableSingleArrayConverter(),
            [typeof(double)] = () => new DoubleConverter(),
            [typeof(double[])] = () => new DoubleArrayConverter(),
            [typeof(Nullable<double>)] = () => new NullableDoubleConverter(),
            [typeof(Nullable<double>[])] = () => new NullableDoubleArrayConverter(),
            [typeof(decimal)] = () => new DecimalConverter(),
            [typeof(decimal[])] = () => new DecimalArrayConverter(),
            [typeof(Nullable<decimal>)] = () => new NullableDecimalConverter(),
            [typeof(Nullable<decimal>[])] = () => new NullableDecimalArrayConverter(),
            [typeof(Guid)] = () => new GuidConverter(),
            [typeof(Guid[])] = () => new GuidArrayConverter(),
            [typeof(Nullable<Guid>)] = () => new NullableGuidConverter(),
            [typeof(Nullable<Guid>[])] = () => new NullableGuidArrayConverter(),
            [typeof(DateTime)] = () => new DateTimeConverter(),
            [typeof(DateTime[])] = () => new DateTimeArrayConverter(),
            [typeof(Nullable<DateTime>)] = () => new NullableDateTimeConverter(),
            [typeof(Nullable<DateTime>[])] = () => new NullableDateTimeArrayConverter(),
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
            return ConverterLookup.TryGetValue(type, out Func<IDataConverter>? func) ?
                func() :
                new UnsupportedTypeConverter(type);
        }
    }
}
