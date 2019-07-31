// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Globalization;

namespace SoftCircuits.CsvParser.Converters
{
    internal class DateTimeArrayConverter : DataConverter<DateTime[]>
    {
        public override string ConvertToString(DateTime[] array)
        {
            if (array == null || array.Length == 0)
                return string.Empty;

            return string.Join(";", array);
        }

        public override bool TryConvertFromString(string s, out DateTime[] array)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    array = null;
                    return (s != null);
                }

                string[] tokens = s.Split(';');
                array = new DateTime[tokens.Length];
                for (int i = 0; i < tokens.Length; i++)
                    array[i] = DateTime.Parse(tokens[i], CultureInfo.InvariantCulture);
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
