// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser.Converters
{
    internal class SByteConverter : CustomConverter<sbyte>
    {
        public override string ConvertToString(sbyte value) => value.ToString();

        public override bool TryConvertFromString(string s, out sbyte value) => sbyte.TryParse(s, out value);
    }
}
