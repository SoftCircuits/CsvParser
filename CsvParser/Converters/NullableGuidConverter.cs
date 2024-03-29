// Copyright (c) 2019-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace SoftCircuits.CsvParser
{
    internal class NullableGuidConverter : DataConverter<Nullable<Guid>>
    {
        public override string ConvertToString(Nullable<Guid> value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out Nullable<Guid> value)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                value = null;
                return true;
            }

            if (Guid.TryParse(s, out Guid temp))
            {
                value = temp;
                return true;
            }

            value = null;
            return false;
        }
    }
}
