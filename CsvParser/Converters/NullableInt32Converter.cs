// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser.Converters
{
    internal class NullableInt32Converter : CustomConverter<int?>
    {
        public override string ConvertToString(int? value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out int? value)
        {
            value = null;

            if (s == string.Empty)
                return true;
            
            if (int.TryParse(s, out int temp))
            {
                value = temp;
                return true;
            }
            return false;
        }
    }
}
