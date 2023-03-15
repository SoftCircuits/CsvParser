// Copyright (c) 2019-2023 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Linq;

namespace SoftCircuits.CsvParser
{
    internal class NullableBooleanArrayConverter : DataConverter<Nullable<bool>[]>
    {
        public override string ConvertToString(Nullable<bool>[]? array)
        {
            if (array == null || array.Length == 0)
                return string.Empty;

            return string.Join(";", array.Select(v => v.HasValue ? v.Value.ToString() : string.Empty));
        }

        public override bool TryConvertFromString(string s, out Nullable<bool>[]? array)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    array = Array.Empty<bool?>();
                }
                else
                {
                    string[] tokens = s.Split(';');
                    array = new bool?[tokens.Length];
                    for (int i = 0; i < tokens.Length; i++)
                        array[i] = (tokens[i].Length > 0) ? (bool?)bool.Parse(tokens[i]) : null;
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
