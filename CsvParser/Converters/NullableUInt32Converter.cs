// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser
{
    internal class NullableUInt32Converter : DataConverter<uint?>
    {
        public override string ConvertToString(uint? value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out uint? value)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                value = null;
                return true;
            }

            if (uint.TryParse(s, out uint temp))
            {
                value = temp;
                return true;
            }

            value = null;
            return false;
        }
    }
}
