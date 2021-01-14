// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser
{
    internal class BooleanConverter : DataConverter<bool>
    {
        public override string ConvertToString(bool value) => value.ToString();

        public override bool TryConvertFromString(string s, out bool value) => bool.TryParse(s, out value);
    }
}
