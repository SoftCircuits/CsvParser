// Copyright (c) 2019-2020 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser
{
    internal class NullableSingleConverter : DataConverter<float?>
    {
        public override string ConvertToString(float? value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out float? value)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                value = null;
                return true;
            }

            if (float.TryParse(s, out float temp))
            {
                value = temp;
                return true;
            }

            value = null;
            return false;
        }
    }
}
