// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
namespace SoftCircuits.CsvParser
{
    internal class NullableByteConverter : DataConverter<Nullable<byte>>
    {
        public override string ConvertToString(Nullable<byte> value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out Nullable<byte> value)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                value = null;
                return true;
            }

            if (byte.TryParse(s, out byte temp))
            {
                value = temp;
                return true;
            }

            value = null;
            return false;
        }
    }
}
