// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser
{
    internal class NullableUInt64Converter : DataConverter<ulong?>
    {
        public override string ConvertToString(ulong? value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out ulong? value)
        {
            value = null;

            if (string.IsNullOrWhiteSpace(s))
                return (s != null);

            if (ulong.TryParse(s, out ulong temp))
            {
                value = temp;
                return true;
            }
            return false;
        }
    }
}
