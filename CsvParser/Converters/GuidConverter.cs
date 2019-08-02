// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace SoftCircuits.CsvParser
{
    internal class GuidConverter : DataConverter<Guid>
    {
        public override string ConvertToString(Guid value) => value.ToString();

        public override bool TryConvertFromString(string s, out Guid value) => Guid.TryParse(s, out value);
    }
}
