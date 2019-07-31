// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace SoftCircuits.CsvParser.Converters
{
    internal class UInt64ArrayConverter : DataConverter<ulong[]>
    {
        public override string ConvertToString(ulong[] array)
        {
            if (array == null || array.Length == 0)
                return string.Empty;

            return string.Join(";", array);
        }

        public override bool TryConvertFromString(string s, out ulong[] array)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    array = null;
                    return (s != null);
                }

                string[] tokens = s.Split(';');
                array = new ulong[tokens.Length];
                for (int i = 0; i < tokens.Length; i++)
                    array[i] = ulong.Parse(tokens[i]);
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
