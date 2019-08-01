// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
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
        List<(string, string, string)> RawCsvFile = new List<(string, string, string)>
        {
            ("Abc", "Def", "Ghi"),
            ("@Abc", "D\"e\"f", "G,h'i"),
            ("The quick, brown", "fox\r\n\r\n\r\njumps over", "the \"lazy\" dog."),
            (",,,,", "\t\t\t\t", "\r\n\r\n\r\n\r\n"),
            ("a\tb", "\t\r\n\t", "\t\t\t"),
            ("123", "456", "789"),
            ("\t\r\n", "\r\nx", "\b\a\v"),
            ("    ,    ", "    ,    ", "    ,    "),
            (" \"abc\" ", " \"\" ", "  \" \" "),
            ("alskdfjlaskdfj asldkfjas ldfk", "laksd flkasj flaksjdfl aksdjf", "lkasjlf kalsdkf jalsdkfj alsdf"),
            ("alskdfjlaskdfj asldkfjas ldfk", "laksd flkasj flaksjdfl aksdjf", "lkasjlf kalsdkf jalsdkfj alsdf"),
            ("alskdfjlaskdfj asldkfjas ldfk", "laksd flkasj flaksjdfl aksdjf", "lkasjlf kalsdkf jalsdkfj alsdf"),
            ("alskdfjlaskdfj asldkfjas ldfk", "laksd flkasj flaksjdfl aksdjf", "lkasjlf kalsdkf jalsdkfj alsdf"),
            ("alskdfjlaskdfj asldkfjas ldfk", "laksd flkasj flaksjdfl aksdjf", "lkasjlf kalsdkf jalsdkfj alsdf"),
            ("alskdfjlaskdfj asldkfjas ldfk", "laksd flkasj flaksjdfl aksdjf", "lkasjlf kalsdkf jalsdkfj alsdf"),
            ("alskdfjlaskdfj asldkfjas ldfk", "laksd flkasj flaksjdfl aksdjf", "lkasjlf kalsdkf jalsdkfj alsdf"),
            ("alskdfjlaskdfj asldkfjas ldfk", "laksd flkasj flaksjdfl aksdjf", "lkasjlf kalsdkf jalsdkfj alsdf"),
            ("alskdfjlaskdfj asldkfjas ldfk", "laksd flkasj flaksjdfl aksdjf", "lkasjlf kalsdkf jalsdkfj alsdf"),
            ("alskdfjlaskdfj asldkfjas ldfk", "laksd flkasj flaksjdfl aksdjf", "lkasjlf kalsdkf jalsdkfj alsdf"),
            ("alskdfjlaskdfj asldkfjas ldfk", "laksd flkasj flaksjdfl aksdjf", "lkasjlf kalsdkf jalsdkfj alsdf"),
            ("alskdfjlaskdfj asldkfjas ldfk", "laksd flkasj flaksjdfl aksdjf", "lkasjlf kalsdkf jalsdkfj alsdf"),
            ("alskdfjlaskdfj asldkfjas ldfk", "laksd flkasj flaksjdfl aksdjf", "lkasjlf kalsdkf jalsdkfj alsdf"),
            ("", "", ""),
        };

        [TestMethod]
        public void ParserTest()
        {
            using (MemoryFile file = new MemoryFile())
            {
                List<(string, string, string)> actual = new List<(string, string, string)>();

                using (CsvWriter writer = new CsvWriter(file))
                {
                    foreach (var data in RawCsvFile)
                        writer.WriteRow(data.Item1, data.Item2, data.Item3);
                }

                using (CsvReader reader = new CsvReader(file))
                {
                    string[] columns = null;
                    while (reader.ReadRow(ref columns))
                    {
                        Assert.AreEqual(3, columns.Length);
                        actual.Add((columns[0], columns[1], columns[2]));
                    }
                    CollectionAssert.AreEqual(RawCsvFile, actual);
                }
            }
        }

        [TestMethod]
        public void TabDelimiterTest()
        {
            using (MemoryFile file = new MemoryFile())
            {
                CsvSettings settings = new CsvSettings();
                settings.ColumnDelimiter = '\t';

                List<(string, string, string)> actual = new List<(string, string, string)>();

                using (CsvWriter writer = new CsvWriter(file, settings))
                {
                    foreach (var data in RawCsvFile)
                        writer.WriteRow(data.Item1, data.Item2, data.Item3);
                }

                using (CsvReader reader = new CsvReader(file, settings))
                {
                    string[] columns = null;
                    while (reader.ReadRow(ref columns))
                    {
                        Assert.AreEqual(3, columns.Length);
                        actual.Add((columns[0], columns[1], columns[2]));
                    }
                    CollectionAssert.AreEqual(RawCsvFile, actual);
                }
            }
        }

        [TestMethod]
        public void SingleQuotesTest()
        {
            using (MemoryFile file = new MemoryFile())
            {
                CsvSettings settings = new CsvSettings();
                settings.QuoteCharacter = '\'';

                List<(string, string, string)> actual = new List<(string, string, string)>();

                using (CsvWriter writer = new CsvWriter(file, settings))
                {
                    foreach (var data in RawCsvFile)
                        writer.WriteRow(data.Item1, data.Item2, data.Item3);
                }

                using (CsvReader reader = new CsvReader(file, settings))
                {
                    string[] columns = null;
                    while (reader.ReadRow(ref columns))
                    {
                        Assert.AreEqual(3, columns.Length);
                        actual.Add((columns[0], columns[1], columns[2]));
                    }
                    CollectionAssert.AreEqual(RawCsvFile, actual);
                }
            }
        }

        List<string> EmptyLineTestData = new List<string>
        {
            "abc,def,ghi",
            "abc,def,ghi",
            "",
            "abc,def,ghi",
            "abc,def,ghi",
        };

        List<List<string>>[] EmptyLineTestResults = new List<List<string>>[]
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
