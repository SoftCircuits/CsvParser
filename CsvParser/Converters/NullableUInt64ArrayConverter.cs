// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Linq;

namespace SoftCircuits.CsvParser
{
    internal class NullableUInt64ArrayConverter : DataConverter<ulong?[]>
    {
        public override string ConvertToString(ulong?[] array)
        {
            if (array == null || array.Length == 0)
                return string.Empty;

            return string.Join(";", array.Select(v => v.HasValue ? v.Value.ToString() : string.Empty));
        }

        public override bool TryConvertFromString(string s, out ulong?[] array)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    array = new ulong?[0];
                    return true;
                }

                string[] tokens = s.Split(';');
                array = new ulong?[tokens.Length];
                for (int i = 0; i < tokens.Length; i++)
                    array[i] = (tokens[i].Length > 0) ? (ulong?)ulong.Parse(tokens[i]) : null;
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
