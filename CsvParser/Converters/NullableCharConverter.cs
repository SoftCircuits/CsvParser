// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser
{
    internal class NullableCharConverter : DataConverter<char?>
    {
        public override string ConvertToString(char? value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out char? value)
        {
            value = string.IsNullOrEmpty(s) ? (char?)null : s[0];
            return true;
        }
    }
}
