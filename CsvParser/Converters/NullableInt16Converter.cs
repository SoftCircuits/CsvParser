// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser
{
    internal class NullableInt16Converter : DataConverter<short?>
    {
        public override string ConvertToString(short? value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out short? value)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                value = null;
                return true;
            }

            if (short.TryParse(s, out short temp))
            {
                value = temp;
                return true;
            }

            value = null;
            return false;
        }
    }
}
