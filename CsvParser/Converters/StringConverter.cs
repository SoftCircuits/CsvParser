// Copyright (c) 2019-2020 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace SoftCircuits.CsvParser
{
    internal class StringConverter : DataConverter<string>
    {
        public override string ConvertToString(string value) => value ?? string.Empty;

        public override bool TryConvertFromString(string s, out string value)
        {
            value = s;
            return (value != null);
        }
    }
}
