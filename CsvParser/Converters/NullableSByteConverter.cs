// Copyright (c) 2019-2023 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
namespace SoftCircuits.CsvParser
{
    internal class NullableSByteConverter : DataConverter<Nullable<sbyte>>
    {
        public override string ConvertToString(Nullable<sbyte> value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out Nullable<sbyte> value)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                value = null;
                return true;
            }

            if (sbyte.TryParse(s, out sbyte temp))
            {
                value = temp;
                return true;
            }

            value = null;
            return false;
        }
    }
}
