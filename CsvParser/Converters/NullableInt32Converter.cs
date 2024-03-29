// Copyright (c) 2019-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
namespace SoftCircuits.CsvParser
{
    internal class NullableInt32Converter : DataConverter<Nullable<int>>
    {
        public override string ConvertToString(Nullable<int> value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out Nullable<int> value)
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
