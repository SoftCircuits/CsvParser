// Copyright (c) 2019-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
namespace SoftCircuits.CsvParser
{
    internal class NullableDoubleConverter : DataConverter<Nullable<double>>
    {
        public override string ConvertToString(Nullable<double> value) => value.HasValue ? value.Value.ToString() : string.Empty;

        public override bool TryConvertFromString(string s, out Nullable<double> value)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                value = null;
                return true;
            }

            if (double.TryParse(s, out double temp))
            {
                value = temp;
                return true;
            }

            value = null;
            return false;
        }
    }
}
