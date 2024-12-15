// Copyright (c) 2019-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace SoftCircuits.CsvParser
{
    internal class CharArrayConverter : DataConverter<char[]>
    {
        public override string ConvertToString(char[]? array) => (array != null) ? new string(array) : string.Empty;

        public override bool TryConvertFromString(string s, out char[]? array)
        {
            array = string.IsNullOrEmpty(s) ? [] : s.ToCharArray();
            return true;
        }
    }
}
