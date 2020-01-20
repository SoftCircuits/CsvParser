// Copyright (c) 2019-2020 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser
{
    internal class NullableCharConverter : DataConverter<char?>
    {
        public override string ConvertToString(char? value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out char? value)
        {
            value = null;

            if (string.IsNullOrEmpty(s))
                return (s != null);

            if (s.Length != 1)
                return false;

            value = s[0];
            return true;
        }
    }
}
