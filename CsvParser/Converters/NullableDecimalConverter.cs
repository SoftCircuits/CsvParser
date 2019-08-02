// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser
{
    internal class NullableDecimalConverter : DataConverter<decimal?>
    {
        public override string ConvertToString(decimal? value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out decimal? value)
        {
            value = null;

            if (string.IsNullOrWhiteSpace(s))
                return (s != null);

            if (decimal.TryParse(s, out decimal temp))
            {
                value = temp;
                return true;
            }
            return false;
        }
    }
}
