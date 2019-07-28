// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser.Converters
{
    internal class NullableByteConverter : CustomConverter<byte?>
    {
        public override string ConvertToString(byte? value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out byte? value)
        {
            value = null;

            if (s == string.Empty)
                return true;
            
            if (byte.TryParse(s, out byte temp))
            {
                value = temp;
                return true;
            }
            return false;
        }
    }
}
