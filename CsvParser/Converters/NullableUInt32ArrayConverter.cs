// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Linq;

namespace SoftCircuits.CsvParser.Converters
{
    internal class NullableUInt32ArrayConverter : DataConverter<uint?[]>
    {
        public override string ConvertToString(uint?[] array)
        {
            if (array == null)
                return string.Empty;

            return string.Join(";", array.Select(v => v.HasValue ? v.Value.ToString() : string.Empty));
        }

        public override bool TryConvertFromString(string s, out uint?[] array)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    array = null;
                    return true;
                }

                string[] tokens = s.Split(';');
                array = new uint?[tokens.Length];
                for (int i = 0; i < tokens.Length; i++)
                    array[i] = (tokens[i].Length > 0) ? (uint?)uint.Parse(tokens[i]) : null;
                return true;
            }
            catch (Exception)
            {
                array = null;
                return false;
            }
        }
    }
}