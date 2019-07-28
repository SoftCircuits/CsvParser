// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser.Converters
{
    internal class NullableUInt64Converter : CustomConverter<ulong?>
    {
        public override string ConvertToString(ulong? value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out ulong? value)
        {
            value = null;

            if (s == string.Empty)
                return true;
            
            if (ulong.TryParse(s, out ulong temp))
            {
                value = temp;
                return true;
            }
            return false;
        }
    }
}
