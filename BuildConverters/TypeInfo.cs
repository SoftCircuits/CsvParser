// Copyright (c) 2019-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Collections.Generic;

namespace BuildConverters
{
    public class TypeInfo
    {
        private readonly Random Random;

        public Type Type { get; private set; }
        public string Name => Type.Name;
        public string CName => CSharpNameLookup[Type];
        public bool IsValueType => Type.IsValueType;

        public TypeInfo(Type type)
        {
            Type = type;
            Random = new Random();
        }

        /// <summary>
        /// Generates sample data for the specified mode.
        /// </summary>
        public string GetSampleData(TypeVariation mode)
        {
            Func<TypeInfo, string> func = SampleDataLookup[Type];

            if (mode == TypeVariation.Standard || mode == TypeVariation.Nullable)
            {
                string s;
                if (mode == TypeVariation.Standard)
                    s = func(this);
                else // Nullable
                    s = (Random.Next(3) != 1) ? func(this) : "null";
                //return $"({CompleteType.GetFullTypeCName(this, mode)}){s}";

                return $"{s}";

            }
            else // Array or NullableArray
            {
                string[] array = new string[10];
                if (mode == TypeVariation.Array)
                {
                    for (int i = 0; i < array.Length; i++)
                        array[i] = func(this);
                }
                else // NullableArray
                {
                    for (int i = 0; i < array.Length; i++)
                        array[i] = (Random.Next(3) != 1) ? func(this) : "null";
                }
                return $"new {CompleteType.GetFullTypeCName(this, mode)} {{ {string.Join(", ", array)} }}";
            }
        }

        #region C# type names

        /// <summary>
        /// Lookup to find C# names for each type.
        /// </summary>
        private readonly Dictionary<Type, string> CSharpNameLookup = new Dictionary<Type, string>
        {
            [typeof(string)] = "string",
            [typeof(char)] = "char",
            [typeof(bool)] = "bool",
            [typeof(byte)] = "byte",
            [typeof(sbyte)] = "sbyte",
            [typeof(short)] = "short",
            [typeof(ushort)] = "ushort",
            [typeof(int)] = "int",
            [typeof(uint)] = "uint",
            [typeof(long)] = "long",
            [typeof(ulong)] = "ulong",
            [typeof(float)] = "float",
            [typeof(double)] = "double",
            [typeof(decimal)] = "decimal",
            [typeof(Guid)] = "Guid",
            [typeof(DateTime)] = "DateTime",
        };

        #endregion

        #region Sample generators

        private Dictionary<Type, Func<TypeInfo, string>> SampleDataLookup = new Dictionary<Type, Func<TypeInfo, string>>
        {
            [typeof(string)] = x => x.GetSampleString(),
            [typeof(char)] = x => x.GetSampleChar(),
            [typeof(bool)] = x => x.GetSampleBool(),
            [typeof(byte)] = x => x.GetSampleByte(),
            [typeof(sbyte)] = x => x.GetSampleSByte(),
            [typeof(short)] = x => x.GetSampleShort(),
            [typeof(ushort)] = x => x.GetSampleUShort(),
            [typeof(int)] = x => x.GetSampleInt(),
            [typeof(uint)] = x => x.GetSampleUInt(),
            [typeof(long)] = x => x.GetSampleLong(),
            [typeof(ulong)] = x => x.GetSampleULong(),
            [typeof(float)] = x => x.GetSampleFloat(),
            [typeof(double)] = x => x.GetSampleDouble(),
            [typeof(decimal)] = x => x.GetSampleDecimal(),
            [typeof(Guid)] = x => x.GetSampleGuid(),
            [typeof(DateTime)] = x => x.GetSampleDateTime(),
        };

        static readonly string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789.,?;:'\"!@#$% ";

        private string GetSampleString()
        {
            char[] chars = new char[10];
            for (int i = 0; i < chars.Length; i++)
                chars[i] = Characters[Random.Next(Characters.Length)];
            return $"\"{new string(chars).Replace("\"", "\\\"")}\"";
        }

        private string GetSampleChar()
        {
            char c = Characters[Random.Next(Characters.Length)];
            return $"'{c.ToString().Replace("'", "\\'")}'";
        }

        private string GetSampleBool() => (Random.Next(2) == 1) ? "true" : "false";

        private string GetSampleByte()
        {
            byte value = (byte)(Random.Next(byte.MinValue, byte.MaxValue) & 0xff);
            return value.ToString();
        }

        private string GetSampleSByte()
        {
            sbyte value = (sbyte)(Random.Next(sbyte.MinValue, sbyte.MaxValue) & 0xff);
            return value.ToString();
        }

        private string GetSampleShort()
        {
            byte[] buffer = new byte[sizeof(short)];
            Random.NextBytes(buffer);
            return BitConverter.ToInt16(buffer).ToString();
        }

        private string GetSampleUShort()
        {
            byte[] buffer = new byte[sizeof(ushort)];
            Random.NextBytes(buffer);
            return BitConverter.ToUInt16(buffer).ToString();
        }

        private string GetSampleInt()
        {
            byte[] buffer = new byte[sizeof(int)];
            Random.NextBytes(buffer);
            return BitConverter.ToInt32(buffer).ToString();
        }

        private string GetSampleUInt()
        {
            byte[] buffer = new byte[sizeof(uint)];
            Random.NextBytes(buffer);
            return BitConverter.ToUInt32(buffer).ToString();
        }

        private string GetSampleLong()
        {
            byte[] buffer = new byte[sizeof(long)];
            Random.NextBytes(buffer);
            return BitConverter.ToInt64(buffer).ToString();
        }

        private string GetSampleULong()
        {
            byte[] buffer = new byte[sizeof(ulong)];
            Random.NextBytes(buffer);
            return BitConverter.ToUInt64(buffer).ToString();
        }

        private string GetSampleFloat() => $"{(float)Math.Round(Random.NextDouble(), 4)}F";

        // Round to prevent micro rounding errors
        private string GetSampleDouble() => Math.Round(Random.NextDouble(), 4).ToString();

        private string GetSampleDecimal() => $"{(decimal)Random.NextDouble()}m";

        private string GetSampleGuid() => "Guid.NewGuid()";

        private string GetSampleDateTime() => $"DateTime.Now.AddSeconds({Random.Next(-350000000, 350000000)})";

        #endregion

    }
}
