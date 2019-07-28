// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace SoftCircuits.CsvParser.Converters
{
    internal class UInt16ArrayConverter : CustomConverter<ushort[]>
    {
        public override string ConvertToString(ushort[] array)
        {
            if (array == null)
                return string.Empty;

            return string.Join(";", array);
        }

        public override bool TryConvertFromString(string s, out ushort[] array)
        {
            try
            {
                array = null;

                if (string.IsNullOrWhiteSpace(s))
                    return false;

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
