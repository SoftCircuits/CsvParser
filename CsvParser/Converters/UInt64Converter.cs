// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser.Converters
{
    internal class UInt64Converter : CustomConverter<ulong>
    {
        public override string ConvertToString(ulong value) => value.ToString();

        public override bool TryConvertFromString(string s, out ulong value) => ulong.TryParse(s, out value);
    }
}
