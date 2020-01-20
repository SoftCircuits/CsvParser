// Copyright (c) 2019-2020 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser
{
    internal class CharArrayConverter : DataConverter<char[]>
    {
        public override string ConvertToString(char[] array) => (array != null) ? new string(array) : string.Empty;

        public override bool TryConvertFromString(string s, out char[] array)
        {
            if (s == null || s.Length == 0)
            {
                array = null;
                return (s != null);
            }
            array = s.ToCharArray();
            return true;
        }
    }
}
