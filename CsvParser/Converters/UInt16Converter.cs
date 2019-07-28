// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser.Converters
{
    internal class UInt16Converter : CustomConverter<ushort>
    {
        public override string ConvertToString(ushort value) => value.ToString();

        public override bool TryConvertFromString(string s, out ushort value) => ushort.TryParse(s, out value);
    }
}
