// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser.Converters
{
    internal class NullableUInt16Converter : DataConverter<ushort?>
    {
        public override string ConvertToString(ushort? value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out ushort? value)
        {
            value = null;

            if (string.IsNullOrWhiteSpace(s))
                return (s != null);

            if (ushort.TryParse(s, out ushort temp))
            {
                value = temp;
                return true;
            }
            return false;
        }
    }
}
