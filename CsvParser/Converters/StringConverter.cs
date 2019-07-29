// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace SoftCircuits.CsvParser.Converters
{
    internal class StringConverter : DataConverter<string>
    {
        public override string ConvertToString(string value) => value;

        public override bool TryConvertFromString(string s, out string value)
        {
            value = s;
            return (value != null);
        }
    }
}
