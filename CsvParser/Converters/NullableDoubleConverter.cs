// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser.Converters
{
    internal class NullableDoubleConverter : CustomConverter<double?>
    {
        public override string ConvertToString(double? value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out double? value)
        {
            value = null;

            if (s == string.Empty)
                return true;
            
            if (double.TryParse(s, out double temp))
            {
                value = temp;
                return true;
            }
            return false;
        }
    }
}
