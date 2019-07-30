// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace SoftCircuits.CsvParser.Converters
{
    internal class Int16ArrayConverter : DataConverter<short[]>
    {
        public override string ConvertToString(short[] array)
        {
            if (array == null)
                return string.Empty;

            return string.Join(";", array);
        }

        public override bool TryConvertFromString(string s, out short[] array)
        {
            try
            {
                array = null;

                if (string.IsNullOrWhiteSpace(s))
                    return false;

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