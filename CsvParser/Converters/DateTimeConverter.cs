// Copyright (c) 2019-2020 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Globalization;

namespace SoftCircuits.CsvParser
{
    internal class DateTimeConverter : DataConverter<DateTime>
    {
        public override string ConvertToString(DateTime value) => value.ToString();

        public override bool TryConvertFromString(string s, out DateTime value)
        {
            return DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.None, out value);
        }
    }
}
