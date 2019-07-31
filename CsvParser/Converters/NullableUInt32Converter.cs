// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser.Converters
{
    internal class NullableUInt32Converter : DataConverter<uint?>
    {
        public override string ConvertToString(uint? value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out uint? value)
        {
            value = null;

            if (string.IsNullOrWhiteSpace(s))
                return (s != null);

            if (uint.TryParse(s, out uint temp))
            {
                value = temp;
                return true;
            }
            return false;
        }
    }
}
