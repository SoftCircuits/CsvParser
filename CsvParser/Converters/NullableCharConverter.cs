// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
namespace SoftCircuits.CsvParser
{
    internal class NullableCharConverter : DataConverter<Nullable<char>>
    {
        public override string ConvertToString(Nullable<char> value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out Nullable<char> value)
        {
            value = string.IsNullOrEmpty(s) ? (Nullable<char>)null : s[0];
            return true;
        }
    }
}
