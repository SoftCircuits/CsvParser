using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftCircuits.CsvParser;
using System.Collections.Generic;
using System.IO;

namespace TestCsvParser
{
    [TestClass]
    public class UnitTest1
    {
        List<(string, string, string)> TestData = new List<(string, string, string)>
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
            RunTests(settings);
            // Vary settings
            settings.ColumnDelimiter = '\t';
            settings.QuoteCharacter = '\'';
            RunTests(settings);
        }

        public void RunTests(CsvSettings settings)
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
                    var s = string.Join(", ", columns);
                    actual.Add((columns[0], columns[1], columns[2]));
                }
                CollectionAssert.AreEqual(TestData, actual);
            }
        }

        [TestMethod]
        public void RunFileTests()
        {
            string path = @"C:\TEMP\TEMP.CSV";

            CsvSettings settings = new CsvSettings();
            settings.ColumnDelimiter = '\t';
            settings.QuoteCharacter = '\'';

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
                    var s = string.Join(", ", columns);
                    actual.Add((columns[0], columns[1], columns[2]));
                }
                CollectionAssert.AreEqual(TestData, actual);
            }
        }
    }
}
