// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftCircuits.CsvParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TestCsvParser
{
    [TestClass]
    public class UnitTest1
    {
        private List<(string, string, string)> TestData = new List<(string, string, string)>
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
        public void TestCsv()
        {
            // Default settings
            CsvSettings settings = new CsvSettings();
            RunStreamTest(settings);
            RunFileTests(settings);
            // Vary settings
            settings.ColumnDelimiter = '\t';
            settings.QuoteCharacter = '\'';
            RunStreamTest(settings);
            RunFileTests(settings);
        }

        public void RunStreamTest(CsvSettings settings)
        {
            byte[] buffer;
            List<(string, string, string)> actual = new List<(string, string, string)>();

            using (MemoryStream stream = new MemoryStream())
            using (CsvWriter writer = new CsvWriter(stream, settings))
            {
                foreach (var data in TestData)
                    writer.WriteRow(data.Item1, data.Item2, data.Item3);
                writer.Flush();
                buffer = stream.ToArray();
            }

            using (MemoryStream stream = new MemoryStream(buffer))
            using (CsvReader reader = new CsvReader(stream, settings))
            {
                string[] columns = null;
                while (reader.ReadRow(ref columns))
                {
                    Assert.AreEqual(3, columns.Length);
                    actual.Add((columns[0], columns[1], columns[2]));
                }
                CollectionAssert.AreEqual(TestData, actual);
            }
        }

        public void RunFileTests(CsvSettings settings)
        {
            string path = @"C:\TEMP\TEMP.CSV";
            List<(string, string, string)> actual = new List<(string, string, string)>();

            using (CsvWriter writer = new CsvWriter(path, settings))
            {
                foreach (var data in TestData)
                    writer.WriteRow((IEnumerable<string>)new[] { data.Item1, data.Item2, data.Item3 });
                writer.Close();
            }

            using (CsvReader reader = new CsvReader(path, settings))
            {
                string[] columns = null;
                while (reader.ReadRow(ref columns))
                {
                    Assert.AreEqual(3, columns.Length);
                    actual.Add((columns[0], columns[1], columns[2]));
                }
                CollectionAssert.AreEqual(TestData, actual);
            }
        }

        private List<string> EmptyLineBehaviorTestData = new List<string>
        {
            "\"abc\",\"def\",\"ghi\"",
            "\"abc\",\"def\",\"ghi\"",
            "",
            "\"abc\",\"def\",\"ghi\"",
            "\"abc\",\"def\",\"ghi\"",
        };

        private List<List<string>>[] EmptyLineBehaviorTestResults = new List<List<string>>[]
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
            CsvSettings settings = new CsvSettings();
            foreach (EmptyLineBehavior emptyLineBehavior in Enum.GetValues(typeof(EmptyLineBehavior)))
            {
                settings.EmptyLineBehavior = emptyLineBehavior;
                byte[] buffer;
                List<List<string>> actual = new List<List<string>>();

                using (MemoryStream stream = new MemoryStream())
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    foreach (string line in EmptyLineBehaviorTestData)
                        writer.WriteLine(line);
                    writer.Flush();
                    buffer = stream.ToArray();
                }

                using (MemoryStream stream = new MemoryStream(buffer))
                using (CsvReader reader = new CsvReader(stream, settings))
                {
                    string[] columns = null;
                    while (reader.ReadRow(ref columns))
                        actual.Add(columns.ToList());
                }

                int resultIndex = (int)emptyLineBehavior;
                Assert.AreEqual(EmptyLineBehaviorTestResults[resultIndex].Count, actual.Count);
                if (EmptyLineBehaviorTestResults[resultIndex].Count == actual.Count)
                {
                    for (int i = 0; i < actual.Count; i++)
                        CollectionAssert.AreEqual(EmptyLineBehaviorTestResults[resultIndex][i], actual[i]);
                }
            }
        }
    }
}
