// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser
{
    internal class NullableBooleanConverter : DataConverter<bool?>
    {
        public override string ConvertToString(bool? value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out bool? value)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                value = null;
                return true;
            }

            if (bool.TryParse(s, out bool temp))
            {
                value = temp;
                return true;
            }

            value = null;
            return false;
        }
    }
}
