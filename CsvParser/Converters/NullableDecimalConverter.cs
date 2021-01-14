// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser
{
    internal class NullableDecimalConverter : DataConverter<decimal?>
    {
        public override string ConvertToString(decimal? value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out decimal? value)
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
