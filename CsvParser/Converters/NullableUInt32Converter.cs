// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser.Converters
{
    internal class NullableUInt32Converter : CustomConverter<uint?>
    {
        public override string ConvertToString(uint? value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out uint? value)
        {
            value = null;

            if (s == string.Empty)
                return true;
            
            if (uint.TryParse(s, out uint temp))
            {
                value = temp;
                return true;
            }
            return false;
        }
    }
}
