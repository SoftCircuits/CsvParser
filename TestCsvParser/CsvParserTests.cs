// Copyright (c) 2019-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftCircuits.CsvParser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CsvParserTests
{
    [TestClass]
    public class CsvParserTests
    {
        private readonly List<List<string?>> CsvTestData =
        [
            [],
            ["Abc", "Def", "Ghi"],
            ["Abc,Def,Ghi"],
            ["Abc\"Def\"Ghi"],
            ["Abc'Def'Ghi"],
            ["The quick, brown", "fox\r\n\r\n\r\njumps over", "the \"lazy\" dog."],
            [",,,,", "\t\t\t\t", "\r\n\r\n\r\n\r\n"],
            ["a\tb", "\t\r\n\t", "\t\t\t"],
            ["123", "456", "789"],
            ["\t\r\n", "\r\nx", "\b\a\v"],
            ["    ,    ", "    ,    ", "    ,    "],
            [" \"abc\" ", " \"\" ", "  \" \" "],
            ["abc"],
            ["abc", "def"],
            ["abc", "def", "ghi"],
            ["abc", "def", "ghi", "jkl"],
            ["abc", "def", "ghi", "jkl", "mno"],
            ["abc", "def", "ghi", "jkl", "mno", "pqr"],
            ["abc", "def", "ghi", "jkl", "mno", "pqr", "stu"],
            ["abc", "def", "ghi", "jkl", "mno", "pqr", "stu", "vwx"],
            ["abc", "def", "ghi", "jkl", "mno", "pqr", "stu", "vwx", "xz"],
            ["alskdfjlaskdfj asldkfjas ldfk", "laksd flkasj flaksjdfl aksdjf", "lkasjlf kalsdkf jalsdkfj alsdf"],
            ["a", "b", ""],
            ["", "", ""],
            [null, null, null],
        ];

        private class ListComparer : Comparer<List<string>>
        {
            public override int Compare(List<string>? x, List<string>? y)
            {
                Debug.Assert(x != null);
                Debug.Assert(y != null);

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
        public async Task BasicTests()
        {
            // Defaults
            RunTest(null);

            // Different delimiters and quote characters
            char[] columnDelimiters = [ ',', ';', '\t' ];
            char[] quoteCharacters = [ '"', '\'' ];

            foreach (char columnDelimiter in columnDelimiters)
            {
                foreach (char quoteCharacter in quoteCharacters)
                {
                    RunTest(new CsvSettings
                    {
                        ColumnDelimiter = columnDelimiter,
                        QuoteCharacter = quoteCharacter,
                    });

                    await RunTestAsync(new CsvSettings
                    {
                        ColumnDelimiter = columnDelimiter,
                        QuoteCharacter = quoteCharacter,
                    });
                }
            }
        }

        private void RunTest(CsvSettings? settings)
        {
            using MemoryFile file = new();

            List<List<string>> actual = [];

            using (CsvWriter writer = new(file, settings))
            {
                foreach (var data in CsvTestData)
                {
                    if (data != null)
                        writer.Write(data);
                }
            }

            using (CsvReader reader = new(file, settings))
            {
                while (reader.Read())
                {
                    actual.Add(reader.Columns.ToList());
                }
            }

            CollectionAssert.AreEqual(CsvTestData, actual, new ListComparer());
        }

        private async Task RunTestAsync(CsvSettings? settings)
        {
            using MemoryFile file = new();

            List<List<string>> actual = [];

            using (CsvWriter writer = new(file, settings))
            {
                foreach (var data in CsvTestData)
                {
                    if (data != null)
                        await writer.WriteAsync(data);
                }
            }

            using (CsvReader reader = new(file, settings))
            {
                while (await reader.ReadAsync())
                {
                    Debug.Assert(reader.Columns != null);
                    actual.Add(reader.Columns.ToList());
                }
            }

            CollectionAssert.AreEqual(CsvTestData, actual, new ListComparer());
        }

        private readonly List<string> EmptyLineTestData =
        [
            "abc,def,ghi",
            "abc,def,ghi",
            "",
            "abc,def,ghi",
            "abc,def,ghi",
        ];

        private readonly List<List<string>>[] EmptyLineTestResults =
        [
            // EmptyLineBehavior.NoColumns
            [
                [ "abc", "def", "ghi" ],
                [ "abc", "def", "ghi" ],
                [ ],
                [ "abc", "def", "ghi" ],
                [ "abc", "def", "ghi" ],
            ],
            // EmptyLineBehavior.EmptyColumn
            [
                [ "abc", "def", "ghi" ],
                [ "abc", "def", "ghi" ],
                [ "" ],
                [ "abc", "def", "ghi" ],
                [ "abc", "def", "ghi" ],
            ],
            // EmptyLineBehavior.Ignore
            [
                [ "abc", "def", "ghi" ],
                [ "abc", "def", "ghi" ],
                [ "abc", "def", "ghi" ],
                [ "abc", "def", "ghi" ],
            ],
            // EmptyLineBehavior.EndOfFile
            [
                [ "abc", "def", "ghi" ],
                [ "abc", "def", "ghi" ],
            ]
        ];

        [TestMethod]
        public void TestEmptyLineBehavior()
        {
            using MemoryFile file = new();

            CsvSettings settings = new();
            foreach (EmptyLineBehavior emptyLineBehavior in Enum.GetValues<EmptyLineBehavior>())
            {
                settings.EmptyLineBehavior = emptyLineBehavior;
                List<List<string>> actual = [];

                using (MemoryStream stream = new())
                using (StreamWriter writer = new(file))
                {
                    foreach (string line in EmptyLineTestData)
                        writer.WriteLine(line);
                }

                using (CsvReader reader = new(file, settings))
                {
                    while (reader.Read())
                        actual.Add(reader.Columns.ToList());
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
