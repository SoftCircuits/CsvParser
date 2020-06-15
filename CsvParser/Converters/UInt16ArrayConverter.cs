// Copyright (c) 2019-2020 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace SoftCircuits.CsvParser
{
    internal class UInt16ArrayConverter : DataConverter<ushort[]>
    {
        public override string ConvertToString(ushort[] array)
        {
            if (array == null || array.Length == 0)
                return string.Empty;

            return string.Join(";", array);
        }

        public override bool TryConvertFromString(string s, out ushort[] array)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    array = new ushort[0];
                    return true;
                }

                string[] tokens = s.Split(';');
                array = new ushort[tokens.Length];
                for (int i = 0; i < tokens.Length; i++)
                    array[i] = ushort.Parse(tokens[i]);
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
