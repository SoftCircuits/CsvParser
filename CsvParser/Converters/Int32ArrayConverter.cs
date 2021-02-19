// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace SoftCircuits.CsvParser
{
    internal class Int32ArrayConverter : DataConverter<int[]>
    {
        public override string ConvertToString(int[]? array)
        {
            if (array == null || array.Length == 0)
                return string.Empty;

            return string.Join(";", array);
        }

        public override bool TryConvertFromString(string s, out int[]? array)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    array = Array.Empty<int>();
                }
                else
                {
                    string[] tokens = s.Split(';');
                    array = new int[tokens.Length];
                    for (int i = 0; i < tokens.Length; i++)
                        array[i] = int.Parse(tokens[i]);
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
