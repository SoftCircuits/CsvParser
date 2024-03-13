// Copyright (c) 2019-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
namespace SoftCircuits.CsvParser
{
    internal class NullableSingleConverter : DataConverter<Nullable<float>>
    {
        public override string ConvertToString(Nullable<float> value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out Nullable<float> value)
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
