// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser.Converters
{
    internal class UInt32Converter : DataConverter<uint>
    {
        public override string ConvertToString(uint value) => value.ToString();

        public override bool TryConvertFromString(string s, out uint value) => uint.TryParse(s, out value);
    }
}