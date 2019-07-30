// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Globalization;

namespace SoftCircuits.CsvParser.Converters
{
    internal class NullableDateTimeConverter : DataConverter<DateTime?>
    {
        public override string ConvertToString(DateTime? value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out DateTime? value)
        {
            value = null;

            if (s == string.Empty)
                return true;
            
            if (DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime temp))
            {
                value = temp;
                return true;
            }
            return false;
        }
    }
}