// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace SoftCircuits.CsvParser
{
    internal class ByteArrayConverter : DataConverter<byte[]>
    {
        public override string ConvertToString(byte[] array)
        {
            if (array == null || array.Length == 0)
                return string.Empty;

            return string.Join(";", array);
        }

        public override bool TryConvertFromString(string s, out byte[] array)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    array = new byte[0];
                    return true;
                }

                string[] tokens = s.Split(';');
                array = new byte[tokens.Length];
                for (int i = 0; i < tokens.Length; i++)
                    array[i] = byte.Parse(tokens[i]);
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
