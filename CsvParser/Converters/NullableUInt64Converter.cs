// Copyright (c) 2019-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
namespace SoftCircuits.CsvParser
{
    internal class NullableUInt64Converter : DataConverter<Nullable<ulong>>
    {
        public override string ConvertToString(Nullable<ulong> value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out Nullable<ulong> value)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                value = null;
                return true;
            }

            if (ulong.TryParse(s, out ulong temp))
            {
                value = temp;
                return true;
            }

            value = null;
            return false;
        }
    }
}
