// Copyright (c) 2019-2020 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Globalization;
using System.Linq;

namespace SoftCircuits.CsvParser
{
    internal class NullableDateTimeArrayConverter : DataConverter<DateTime?[]>
    {
        public override string ConvertToString(DateTime?[] array)
        {
            if (array == null || array.Length == 0)
                return string.Empty;

            return string.Join(";", array.Select(v => v.HasValue ? v.Value.ToString() : string.Empty));
        }

        public override bool TryConvertFromString(string s, out DateTime?[] array)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    array = null;
                    return (s != null);
                }

                string[] tokens = s.Split(';');
                array = new DateTime?[tokens.Length];
                for (int i = 0; i < tokens.Length; i++)
                    array[i] = (tokens[i].Length > 0) ? (DateTime?)DateTime.Parse(tokens[i], CultureInfo.InvariantCulture) : null;
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
