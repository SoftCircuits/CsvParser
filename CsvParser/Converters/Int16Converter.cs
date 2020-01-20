// Copyright (c) 2019-2020 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser
{
    internal class Int16Converter : DataConverter<short>
    {
        public override string ConvertToString(short value) => value.ToString();

        public override bool TryConvertFromString(string s, out short value) => short.TryParse(s, out value);
    }
}
