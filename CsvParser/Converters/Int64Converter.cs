// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser.Converters
{
    internal class Int64Converter : CustomConverter<long>
    {
        public override string ConvertToString(long value) => value.ToString();

        public override bool TryConvertFromString(string s, out long value) => long.TryParse(s, out value);
    }
}
