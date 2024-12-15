// Copyright (c) 2019-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace SoftCircuits.CsvParser
{
    internal class SingleArrayConverter : DataConverter<float[]>
    {
        public override string ConvertToString(float[]? array)
        {
            if (array == null || array.Length == 0)
                return string.Empty;

            return string.Join(";", array);
        }

        public override bool TryConvertFromString(string s, out float[]? array)
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
                    array = new float[tokens.Length];
                    for (int i = 0; i < tokens.Length; i++)
                        array[i] = float.Parse(tokens[i]);
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
