// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace SoftCircuits.CsvParser
{
    internal class DecimalArrayConverter : DataConverter<decimal[]>
    {
        public override string ConvertToString(decimal[] array)
        {
            if (array == null || array.Length == 0)
                return string.Empty;

            return string.Join(";", array);
        }

        public override bool TryConvertFromString(string s, out decimal[] array)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    array = new decimal[0];
                    return true;
                }

                string[] tokens = s.Split(';');
                array = new decimal[tokens.Length];
                for (int i = 0; i < tokens.Length; i++)
                    array[i] = decimal.Parse(tokens[i]);
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
