// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser
{
    internal class DoubleConverter : DataConverter<double>
    {
        public override string ConvertToString(double value) => value.ToString();

        public override bool TryConvertFromString(string s, out double value) => double.TryParse(s, out value);
    }
}
