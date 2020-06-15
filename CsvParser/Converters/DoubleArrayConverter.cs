// Copyright (c) 2019-2020 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace SoftCircuits.CsvParser
{
    internal class DoubleArrayConverter : DataConverter<double[]>
    {
        public override string ConvertToString(double[] array)
        {
            if (array == null || array.Length == 0)
                return string.Empty;

            return string.Join(";", array);
        }

        public override bool TryConvertFromString(string s, out double[] array)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    array = new double[0];
                    return true;
                }

                string[] tokens = s.Split(';');
                array = new double[tokens.Length];
                for (int i = 0; i < tokens.Length; i++)
                    array[i] = double.Parse(tokens[i]);
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
