// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser
{
    internal class NullableInt32Converter : DataConverter<int?>
    {
        public override string ConvertToString(int? value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out int? value)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                value = null;
                return true;
            }

            if (int.TryParse(s, out int temp))
            {
                value = temp;
                return true;
            }

            value = null;
            return false;
        }
    }
}
