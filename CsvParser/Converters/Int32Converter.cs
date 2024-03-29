// Copyright (c) 2019-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser
{
    internal class Int32Converter : DataConverter<int>
    {
        public override string ConvertToString(int value) => value.ToString();

        public override bool TryConvertFromString(string s, out int value) => int.TryParse(s, out value);
    }
}
