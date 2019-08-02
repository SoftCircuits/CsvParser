// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace SoftCircuits.CsvParser
{
    internal class Int16ArrayConverter : DataConverter<short[]>
    {
        public override string ConvertToString(short[] array)
        {
            if (array == null || array.Length == 0)
                return string.Empty;

            return string.Join(";", array);
        }

        public override bool TryConvertFromString(string s, out short[] array)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    array = null;
                    return (s != null);
                }

                string[] tokens = s.Split(';');
                array = new short[tokens.Length];
                for (int i = 0; i < tokens.Length; i++)
                    array[i] = short.Parse(tokens[i]);
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
