// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser.Converters
{
    internal class NullableCharConverter : CustomConverter<char?>
    {
        public override string ConvertToString(char? value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out char? value)
        {
            value = null;

            if (s == string.Empty)
                return true;
            
            if (char.TryParse(s, out char temp))
            {
                value = temp;
                return true;
            }
            return false;
        }
    }
}
