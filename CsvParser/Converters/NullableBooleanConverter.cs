// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser.Converters
{
    internal class NullableBooleanConverter : DataConverter<bool?>
    {
        public override string ConvertToString(bool? value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out bool? value)
        {
            value = null;

            if (string.IsNullOrWhiteSpace(s))
                return (s != null);

            if (bool.TryParse(s, out bool temp))
            {
                value = temp;
                return true;
            }
            return false;
        }
    }
}
