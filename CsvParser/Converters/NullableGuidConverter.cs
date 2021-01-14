// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace SoftCircuits.CsvParser
{
    internal class NullableGuidConverter : DataConverter<Guid?>
    {
        public override string ConvertToString(Guid? value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out Guid? value)
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
