// Copyright (c) 2019-2020 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser
{
    internal class SingleConverter : DataConverter<float>
    {
        public override string ConvertToString(float value) => value.ToString();

        public override bool TryConvertFromString(string s, out float value) => float.TryParse(s, out value);
    }
}
