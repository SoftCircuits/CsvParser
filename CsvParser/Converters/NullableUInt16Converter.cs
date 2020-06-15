// Copyright (c) 2019-2020 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser
{
    internal class NullableUInt16Converter : DataConverter<ushort?>
    {
        public override string ConvertToString(ushort? value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out ushort? value)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                value = null;
                return true;
            }

            if (ushort.TryParse(s, out ushort temp))
            {
                value = temp;
                return true;
            }

            value = null;
            return false;
        }
    }
}
