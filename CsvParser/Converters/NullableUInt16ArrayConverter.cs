// Copyright (c) 2019-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Linq;

namespace SoftCircuits.CsvParser
{
    internal class NullableUInt16ArrayConverter : DataConverter<Nullable<ushort>[]>
    {
        public override string ConvertToString(Nullable<ushort>[]? array)
        {
            if (array == null || array.Length == 0)
                return string.Empty;

            return string.Join(";", array.Select(v => v.HasValue ? v.Value.ToString() : string.Empty));
        }

        public override bool TryConvertFromString(string s, out Nullable<ushort>[]? array)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    array = [];
                }
                else
                {
                    string[] tokens = s.Split(';');
                    array = new ushort?[tokens.Length];
                    for (int i = 0; i < tokens.Length; i++)
                        array[i] = (tokens[i].Length > 0) ? (ushort?)ushort.Parse(tokens[i]) : null;
                }
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
