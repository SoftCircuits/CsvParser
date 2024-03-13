// Copyright (c) 2019-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
namespace SoftCircuits.CsvParser
{
    internal class NullableBooleanConverter : DataConverter<Nullable<bool>>
    {
        public override string ConvertToString(Nullable<bool> value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out Nullable<bool> value)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                value = null;
                return true;
            }

            if (bool.TryParse(s, out bool temp))
            {
                value = temp;
                return true;
            }

            value = null;
            return false;
        }
    }
}
