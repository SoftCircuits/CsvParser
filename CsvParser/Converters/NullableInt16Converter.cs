// Copyright (c) 2019-2023 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
namespace SoftCircuits.CsvParser
{
    internal class NullableInt16Converter : DataConverter<Nullable<short>>
    {
        public override string ConvertToString(Nullable<short> value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out Nullable<short> value)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                value = null;
                return true;
            }

            if (short.TryParse(s, out short temp))
            {
                value = temp;
                return true;
            }

            value = null;
            return false;
        }
    }
}
