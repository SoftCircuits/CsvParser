// Copyright (c) 2019-2020 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser
{
    internal class NullableByteConverter : DataConverter<byte?>
    {
        public override string ConvertToString(byte? value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out byte? value)
        {
            value = null;

            if (string.IsNullOrWhiteSpace(s))
                return (s != null);

            if (byte.TryParse(s, out byte temp))
            {
                value = temp;
                return true;
            }
            return false;
        }
    }
}
