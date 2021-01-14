// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace SoftCircuits.CsvParser
{
    internal class UInt32ArrayConverter : DataConverter<uint[]>
    {
        public override string ConvertToString(uint[] array)
        {
            if (array == null || array.Length == 0)
                return string.Empty;

            return string.Join(";", array);
        }

        public override bool TryConvertFromString(string s, out uint[] array)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    array = new uint[0];
                    return true;
                }

                string[] tokens = s.Split(';');
                array = new uint[tokens.Length];
                for (int i = 0; i < tokens.Length; i++)
                    array[i] = uint.Parse(tokens[i]);
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
