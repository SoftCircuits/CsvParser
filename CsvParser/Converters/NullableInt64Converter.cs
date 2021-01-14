// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser
{
    internal class NullableInt64Converter : DataConverter<long?>
    {
        public override string ConvertToString(long? value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out long? value)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                value = null;
                return true;
            }

            if (long.TryParse(s, out long temp))
            {
                value = temp;
                return true;
            }

            value = null;
            return false;
        }
    }
}
