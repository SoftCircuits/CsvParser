// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace SoftCircuits.CsvParser.Converters
{
    internal class SingleArrayConverter : DataConverter<float[]>
    {
        public override string ConvertToString(float[] array)
        {
            if (array == null)
                return string.Empty;

            return string.Join(";", array);
        }

        public override bool TryConvertFromString(string s, out float[] array)
        {
            try
            {
                array = null;

                if (string.IsNullOrWhiteSpace(s))
                    return false;

                string[] tokens = s.Split(';');
                array = new float[tokens.Length];
                for (int i = 0; i < tokens.Length; i++)
                    array[i] = float.Parse(tokens[i]);
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