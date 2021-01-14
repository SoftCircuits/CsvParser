// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser
{
    internal class Int64Converter : DataConverter<long>
    {
        public override string ConvertToString(long value) => value.ToString();

        public override bool TryConvertFromString(string s, out long value) => long.TryParse(s, out value);
    }
}
