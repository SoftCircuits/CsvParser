// Copyright (c) 2019-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace SoftCircuits.CsvParser
{
    internal class BooleanArrayConverter : DataConverter<bool[]>
    {
        public override string ConvertToString(bool[]? array)
        {
            if (array == null || array.Length == 0)
                return string.Empty;

            return string.Join(";", array);
        }

        public override bool TryConvertFromString(string s, out bool[]? array)
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
                    array = new bool[tokens.Length];
                    for (int i = 0; i < tokens.Length; i++)
                        array[i] = bool.Parse(tokens[i]);
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
