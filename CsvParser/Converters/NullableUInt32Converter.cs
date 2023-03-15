// Copyright (c) 2019-2023 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
namespace SoftCircuits.CsvParser
{
    internal class NullableUInt32Converter : DataConverter<Nullable<uint>>
    {
        public override string ConvertToString(Nullable<uint> value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out Nullable<uint> value)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                value = null;
                return true;
            }

            if (uint.TryParse(s, out uint temp))
            {
                value = temp;
                return true;
            }

            value = null;
            return false;
        }
    }
}
