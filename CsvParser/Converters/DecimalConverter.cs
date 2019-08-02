// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser
{
    internal class DecimalConverter : DataConverter<decimal>
    {
        public override string ConvertToString(decimal value) => value.ToString();

        public override bool TryConvertFromString(string s, out decimal value) => decimal.TryParse(s, out value);
    }
}
