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
            value = null;

            if (string.IsNullOrWhiteSpace(s))
                return (s != null);

            if (double.TryParse(s, out double temp))
            {
                value = temp;
                return true;
            }
            return false;
        }
    }
}
