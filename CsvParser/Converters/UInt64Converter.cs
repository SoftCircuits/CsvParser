// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser
{
    internal class UInt64Converter : DataConverter<ulong>
    {
        public override string ConvertToString(ulong value) => value.ToString();

        public override bool TryConvertFromString(string s, out ulong value) => ulong.TryParse(s, out value);
    }
}
