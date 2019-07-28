// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System.Collections.Generic;
using System.Text;

namespace SoftCircuits.CsvParser.Converters
{
    internal class StringArrayConverter : CustomConverter<string[]>
    {
        public override string ConvertToString(string[] array)
        {
            StringBuilder builder = new StringBuilder();

            if (array == null)
                return string.Empty;

            for (int i = 0; i < array.Length; i++)
            {
                if (i > 0)
                    builder.Append(';');
                if (array[i].IndexOfAny(new[] { ';', '"', '\r', '\n' }) >= 0)
                    builder.Append(string.Format("\"{0}\"", array[i].Replace("\"", "\"\"")));
                else
                    builder.Append(array[i]);
            }
            return builder.ToString();
        }

        public override bool TryConvertFromString(string s, out string[] array)
        {
            List<string> list = new List<string>();
            int pos = 0;

            if (s == null)
            {
                array = null;
                return false;
            }

            while (pos < s.Length)
            {
                if (s[pos] == '"')
                {
                    // Parse quoted value
                    StringBuilder builder = new StringBuilder();

                    // Skip starting quote
                    pos++;
                    while (pos < s.Length)
                    {
                        if (s[pos] == '"')
                        {
                            // Skip quote
                            pos++;
                            // One quote signifies end of value
                            // Two quote signifies single quote literal
                            if (pos >= s.Length || s[pos] != '"')
                                break;
                        }
                        builder.Append(s[pos++]);
                    }
                    list.Add(builder.ToString());
                    // Skip past next delimiter
                    pos = s.IndexOf(';', pos);
                    if (pos == -1)
                        pos = s.Length;
                    pos++;
                }
                else
                {
                    // Parse value
                    int start = pos;
                    pos = s.IndexOf(';', pos);
                    if (pos == -1)
                        pos = s.Length;
                    list.Add(s.Substring(start, pos - start));
                    // Skip delimiter
                    pos++;
                }
            }
            array = list.ToArray();
            return true;
        }
    }
}
