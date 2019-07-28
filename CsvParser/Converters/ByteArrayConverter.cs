// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace SoftCircuits.CsvParser.Converters
{
    internal class ByteArrayConverter : CustomConverter<byte[]>
    {
        public override string ConvertToString(byte[] array)
        {
            if (array == null)
                return string.Empty;

            return string.Join(";", array);
        }

        public override bool TryConvertFromString(string s, out byte[] array)
        {
            try
            {
                array = null;

                if (string.IsNullOrWhiteSpace(s))
                    return false;

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
