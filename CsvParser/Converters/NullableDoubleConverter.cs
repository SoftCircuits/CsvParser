// Copyright (c) 2019-2020 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser
{
    internal class NullableDoubleConverter : DataConverter<double?>
    {
        public override string ConvertToString(double? value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out double? value)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                value = null;
                return true;
            }

            if (double.TryParse(s, out double temp))
            {
                value = temp;
                return true;
            }

            value = null;
            return false;
        }
    }
}
