// Copyright (c) 2019-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace SoftCircuits.CsvParser
{
    internal class GuidArrayConverter : DataConverter<Guid[]>
    {
        public override string ConvertToString(Guid[]? array)
        {
            if (array == null || array.Length == 0)
                return string.Empty;

            return string.Join(";", array);
        }

        public override bool TryConvertFromString(string s, out Guid[]? array)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    array = Array.Empty<Guid>();
                }
                else
                {
                    string[] tokens = s.Split(';');
                    array = new Guid[tokens.Length];
                    for (int i = 0; i < tokens.Length; i++)
                        array[i] = Guid.Parse(tokens[i]);
                }
                return true;
            }
            catch (Exception)
            {
                array = null;
                return false;
            }
        }
    }
}
