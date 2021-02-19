// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
namespace SoftCircuits.CsvParser
{
    internal class NullableDecimalConverter : DataConverter<Nullable<decimal>>
    {
        public override string ConvertToString(Nullable<decimal> value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out Nullable<decimal> value)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                value = null;
                return true;
            }

            if (decimal.TryParse(s, out decimal temp))
            {
                value = temp;
                return true;
            }

            value = null;
            return false;
        }
    }
}
