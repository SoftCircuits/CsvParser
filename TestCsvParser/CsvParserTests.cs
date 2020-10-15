// Copyright (c) 2019-2020 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftCircuits.CsvParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CsvParserTests
{
    [TestClass]
    public class CsvParserTests
    {
        private readonly List<List<string>> CsvTestData = new List<List<string>>
        {
            new List<string> { },
            new List<string> { "Abc", "Def", "Ghi" },
            new List<string> { "Abc,Def,Ghi" },
            new List<string> { "Abc\"Def\"Ghi" },
            new List<string> { "Abc'Def'Ghi" },
            new List<string> { "The quick, brown", "fox\r\n\r\n\r\njumps over", "the \"lazy\" dog." },
            new List<string> { ",,,,", "\t\t\t\t", "\r\n\r\n\r\n\r\n" },
            new List<string> { "a\tb", "\t\r\n\t", "\t\t\t" },
            new List<string> { "123", "456", "789" },
            new List<string> { "\t\r\n", "\r\nx", "\b\a\v" },
            new List<string> { "    ,    ", "    ,    ", "    ,    " },
            new List<string> { " \"abc\" ", " \"\" ", "  \" \" " },
            new List<string> { "abc" },
            new List<string> { "abc", "def" },
            new List<string> { "abc", "def", "ghi" },
            new List<string> { "abc", "def", "ghi", "jkl" },
            new List<string> { "abc", "def", "ghi", "jkl", "mno" },
            new List<string> { "abc", "def", "ghi", "jkl", "mno", "pqr" },
            new List<string> { "abc", "def", "ghi", "jkl", "mno", "pqr", "stu" },
            new List<string> { "abc", "def", "ghi", "jkl", "mno", "pqr", "stu", "vwx" },
            new List<string> { "abc", "def", "ghi", "jkl", "mno", "pqr", "stu", "vwx", "xz" },
            new List<string> { "alskdfjlaskdfj asldkfjas ldfk", "laksd flkasj flaksjdfl aksdjf", "lkasjlf kalsdkf jalsdkfj alsdf" },
            new List<string> { "", "", "" },
            new List<string> { null, null, null },
        };

        private class ListComparer : Comparer<List<string>>
        {
            public override int Compare(List<string> x, List<string> y)
            {
                if (x.Count != y.Count)
                    return x.Count - y.Count;
                for (int i = 0; i < x.Count; i++)
                {
                    if (x[i] == null || y[i] == null)
                    {
                        // null will be empty string when read back
                        if (!string.IsNullOrEmpty(x[i]))
                            return 1;
                        if (!string.IsNullOrEmpty(y[i]))
                            return -1;
                    }
                    else
                    {
                        int result = x[i].CompareTo(y[i]);
                        if (result != 0)
                            return result;
                    }
                }
                return 0;
            }
        }

        [TestMethod]
        public void BasicTests()
        {
            // Defaults
            RunTest(null);

            // Different delimiters and quote characters
            char[] columnDelimiters = { ',', ';', '\t' };
            char[] quoteCharacters = { '"', '\'' };

            foreach (char columnDelimiter in columnDelimiters)
            {
                foreach (char quoteCharacter in quoteCharacters)
                {
                    RunTest(new CsvSettings
                    {
                        ColumnDelimiter = columnDelimiter,
                        QuoteCharacter = quoteCharacter,
                    });
                }
            }
        }

        private void RunTest(CsvSettings settings)
        {
            using (MemoryFile file = new MemoryFile())
            {
                List<List<string>> actual = new List<List<string>>();

                using (CsvWriter writer = new CsvWriter(file, settings))
                {
                    foreach (var data in CsvTestData)
                        writer.WriteRow(data);
                }

                using (CsvReader reader = new CsvReader(file, settings))
                {
                    string[] columns = null;
                    while (reader.ReadRow(ref columns))
                    {
                        //Assert.AreEqual(3, columns.Length);
                        actual.Add(columns.ToList());
                    }
                    CollectionAssert.AreEqual(CsvTestData, actual, new ListComparer());
                }
            }
        }

        private readonly List<string> EmptyLineTestData = new List<string>
        {
            "abc,def,ghi",
            "abc,def,ghi",
            "",
            "abc,def,ghi",
            "abc,def,ghi",
        };

        private readonly List<List<string>>[] EmptyLineTestResults = new List<List<string>>[]
        {
            // EmptyLineBehavior.NoColumns
            new List<List<string>>
            {
                new List<string> { "abc", "def", "ghi" },
                new List<string> { "abc", "def", "ghi" },
                new List<string> { },
                new List<string> { "abc", "def", "ghi" },
                new List<string> { "abc", "def", "ghi" },
            },
            // EmptyLineBehavior.EmptyColumn
            new List<List<string>>
            {
                new List<string> { "abc", "def", "ghi" },
                new List<string> { "abc", "def", "ghi" },
                new List<string> { "" },
                new List<string> { "abc", "def", "ghi" },
                new List<string> { "abc", "def", "ghi" },
            },
            // EmptyLineBehavior.Ignore
            new List<List<string>>
            {
                new List<string> { "abc", "def", "ghi" },
                new List<string> { "abc", "def", "ghi" },
                new List<string> { "abc", "def", "ghi" },
                new List<string> { "abc", "def", "ghi" },
            },
            // EmptyLineBehavior.EndOfFile
            new List<List<string>>
            {
                new List<string> { "abc", "def", "ghi" },
                new List<string> { "abc", "def", "ghi" },
            }
        };

        [TestMethod]
        public void TestEmptyLineBehavior()
        {
            using (MemoryFile file = new MemoryFile())
            {
                CsvSettings settings = new CsvSettings();
                foreach (EmptyLineBehavior emptyLineBehavior in Enum.GetValues(typeof(EmptyLineBehavior)))
                {
                    settings.EmptyLineBehavior = emptyLineBehavior;
                    List<List<string>> actual = new List<List<string>>();

                    using (MemoryStream stream = new MemoryStream())
                    using (StreamWriter writer = new StreamWriter(file))
                    {
                        foreach (string line in EmptyLineTestData)
                            writer.WriteLine(line);
                    }

                    using (CsvReader reader = new CsvReader(file, settings))
                    {
                        string[] columns = null;
                        while (reader.ReadRow(ref columns))
                            actual.Add(columns.ToList());
                    }

                    int resultIndex = (int)emptyLineBehavior;
                    Assert.AreEqual(EmptyLineTestResults[resultIndex].Count, actual.Count);
                    if (EmptyLineTestResults[resultIndex].Count == actual.Count)
                    {
                        for (int i = 0; i < actual.Count; i++)
                            CollectionAssert.AreEqual(EmptyLineTestResults[resultIndex][i], actual[i]);
                    }
                }
            }
        }
    }
}
