// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Globalization;

namespace SoftCircuits.CsvParser
{
    internal class DateTimeArrayConverter : DataConverter<DateTime[]>
    {
        public override string ConvertToString(DateTime[]? array)
        {
            if (array == null || array.Length == 0)
                return string.Empty;

            return string.Join(";", array);
        }

        public override bool TryConvertFromString(string s, out DateTime[]? array)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    array = Array.Empty<DateTime>();
                    return true;
                }

                string[] tokens = s.Split(';');
                array = new DateTime[tokens.Length];
                for (int i = 0; i < tokens.Length; i++)
                    array[i] = DateTime.Parse(tokens[i]);
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
