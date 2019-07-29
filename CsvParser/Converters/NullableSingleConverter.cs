// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser.Converters
{
    internal class NullableSingleConverter : DataConverter<float?>
    {
        public override string ConvertToString(float? value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out float? value)
        {
            value = null;

            if (s == string.Empty)
                return true;
            
            if (float.TryParse(s, out float temp))
            {
                value = temp;
                return true;
            }
            return false;
        }
    }
}
