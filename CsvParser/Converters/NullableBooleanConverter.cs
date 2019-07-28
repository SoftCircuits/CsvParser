// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser.Converters
{
    internal class NullableBooleanConverter : CustomConverter<bool?>
    {
        public override string ConvertToString(bool? value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out bool? value)
        {
            value = null;

            if (s == string.Empty)
                return true;
            
            if (bool.TryParse(s, out bool temp))
            {
                value = temp;
                return true;
            }
            return false;
        }
    }
}
