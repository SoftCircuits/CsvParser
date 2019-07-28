// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace SoftCircuits.CsvParser.Converters
{
    internal class NullableGuidConverter : CustomConverter<Guid?>
    {
        public override string ConvertToString(Guid? value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out Guid? value)
        {
            value = null;

            if (s == string.Empty)
                return true;
            
            if (Guid.TryParse(s, out Guid temp))
            {
                value = temp;
                return true;
            }
            return false;
        }
    }
}
