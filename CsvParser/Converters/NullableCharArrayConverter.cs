// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Linq;

namespace SoftCircuits.CsvParser
{
    internal class NullableCharArrayConverter : DataConverter<char?[]>
    {
        public override string ConvertToString(char?[] array)
        {
            if (array == null || array.Length == 0)
                return string.Empty;

            return string.Join(";", array.Select(c => c.HasValue ? ((int)c).ToString() : string.Empty));
        }

        public override bool TryConvertFromString(string s, out char?[] array)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    array = null;
                    return (s != null);
                }

                string[] tokens = s.Split(';');
                array = new char?[tokens.Length];
                for (int i = 0; i < tokens.Length; i++)
                    array[i] = (tokens[i].Length > 0) ? (char?)int.Parse(tokens[i]) : null;
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
