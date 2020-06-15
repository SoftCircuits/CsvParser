// Copyright (c) 2019-2020 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser
{
    internal class NullableUInt64Converter : DataConverter<ulong?>
    {
        public override string ConvertToString(ulong? value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out ulong? value)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                value = null;
                return true;
            }

            if (ulong.TryParse(s, out ulong temp))
            {
                value = temp;
                return true;
            }

            value = null;
            return false;
        }
    }
}
