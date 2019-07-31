// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser.Converters
{
    internal class NullableSByteConverter : DataConverter<sbyte?>
    {
        public override string ConvertToString(sbyte? value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out sbyte? value)
        {
            value = null;

            if (string.IsNullOrWhiteSpace(s))
                return (s != null);

            if (sbyte.TryParse(s, out sbyte temp))
            {
                value = temp;
                return true;
            }
            return false;
        }
    }
}
