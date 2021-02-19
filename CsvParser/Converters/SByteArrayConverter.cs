// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace SoftCircuits.CsvParser
{
    internal class SByteArrayConverter : DataConverter<sbyte[]>
    {
        public override string ConvertToString(sbyte[]? array)
        {
            if (array == null || array.Length == 0)
                return string.Empty;

            return string.Join(";", array);
        }

        public override bool TryConvertFromString(string s, out sbyte[]? array)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    array = Array.Empty<sbyte>();
                }
                else
                {
                    string[] tokens = s.Split(';');
                    array = new sbyte[tokens.Length];
                    for (int i = 0; i < tokens.Length; i++)
                        array[i] = sbyte.Parse(tokens[i]);
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
