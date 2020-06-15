// Copyright (c) 2019-2020 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser
{
    internal class NullableSByteConverter : DataConverter<sbyte?>
    {
        public override string ConvertToString(sbyte? value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out sbyte? value)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                value = null;
                return true;
            }

            if (sbyte.TryParse(s, out sbyte temp))
            {
                value = temp;
                return true;
            }

            value = null;
            return false;
        }
    }
}
