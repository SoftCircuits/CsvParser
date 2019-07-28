// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser.Converters
{
    internal class NullableInt64Converter : CustomConverter<long?>
    {
        public override string ConvertToString(long? value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out long? value)
        {
            value = null;

            if (s == string.Empty)
                return true;
            
            if (long.TryParse(s, out long temp))
            {
                value = temp;
                return true;
            }
            return false;
        }
    }
}
