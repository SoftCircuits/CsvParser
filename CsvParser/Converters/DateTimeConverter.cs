// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Globalization;

namespace SoftCircuits.CsvParser.Converters
{
    internal class DateTimeConverter : CustomConverter<DateTime>
    {
        public override string ConvertToString(DateTime value) => value.ToString();

        public override bool TryConvertFromString(string s, out DateTime value)
        {
            return DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.None, out value);
        }
    }
}
