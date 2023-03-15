// Copyright (c) 2019-2023 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
namespace SoftCircuits.CsvParser
{
    internal class NullableUInt16Converter : DataConverter<Nullable<ushort>>
    {
        public override string ConvertToString(Nullable<ushort> value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out Nullable<ushort> value)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                value = null;
                return true;
            }

            if (ushort.TryParse(s, out ushort temp))
            {
                value = temp;
                return true;
            }

            value = null;
            return false;
        }
    }
}
