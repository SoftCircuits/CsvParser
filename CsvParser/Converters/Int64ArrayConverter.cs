// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace SoftCircuits.CsvParser
{
    internal class Int64ArrayConverter : DataConverter<long[]>
    {
        public override string ConvertToString(long[] array)
        {
            if (array == null || array.Length == 0)
                return string.Empty;

            return string.Join(";", array);
        }

        public override bool TryConvertFromString(string s, out long[] array)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    array = new long[0];
                    return true;
                }

                string[] tokens = s.Split(';');
                array = new long[tokens.Length];
                for (int i = 0; i < tokens.Length; i++)
                    array[i] = long.Parse(tokens[i]);
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
