// Copyright (c) 2019-2020 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser
{
    internal class ByteConverter : DataConverter<byte>
    {
        public override string ConvertToString(byte value) => value.ToString();

        public override bool TryConvertFromString(string s, out byte value) => byte.TryParse(s, out value);
    }
}
