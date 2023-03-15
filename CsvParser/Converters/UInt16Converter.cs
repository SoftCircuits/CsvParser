// Copyright (c) 2019-2023 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser
{
    internal class UInt16Converter : DataConverter<ushort>
    {
        public override string ConvertToString(ushort value) => value.ToString();

        public override bool TryConvertFromString(string s, out ushort value) => ushort.TryParse(s, out value);
    }
}
