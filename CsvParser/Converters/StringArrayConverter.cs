// Copyright (c) 2019-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System.Collections.Generic;
using System.Text;

namespace SoftCircuits.CsvParser
{
    internal class StringArrayConverter : DataConverter<string[]>
    {
        public override string ConvertToString(string[]? array)
        {
            if (array == null || array.Length == 0)
                return string.Empty;

            StringBuilder builder = new();
            for (int i = 0; i < array.Length; i++)
            {
                if (i > 0)
                    builder.Append(';');
                if (array[i].IndexOfAny(new[] { ';', '"', '\r', '\n' }) >= 0)
                    builder.Append($"\"{array[i].Replace("\"", "\"\"")}\"");
                else
                    builder.Append(array[i]);
            }
            return builder.ToString();
        }

        public override bool TryConvertFromString(string s, out string[] array)
        {
            s ??= string.Empty;

            List<string> list = new();
            int pos = 0;

            while (pos < s.Length)
            {
                if (s[pos] == '"')
                {
                    // Parse quoted value
                    StringBuilder builder = new();
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
#if !NETSTANDARD2_0
                    list.Add(s[start..pos]);
#else
                    list.Add(s.Substring(start, pos - start));
#endif
                    // Skip delimiter
                    pos++;
                }
            }
            array = list.ToArray();
            return true;
        }
    }
}
