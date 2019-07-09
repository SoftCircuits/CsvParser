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
            ("@Abc", "D\"e\"f", "G,hi"),
            ("The quick, brown", "fox\r\n\r\n\r\njumps over", "the \"lazy\" dog."),
            (",,,,", "\t\t\t\t", "\r\n\r\n\r\n\r\n"),
            ("a\tb", "\t\r\n\t", "\t\t\t"),
            ("123", "456", "789"),
            ("\t\r\n", "\r\nx", "\b\a\v"),
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
                    //writer.WriteRow((IEnumerable<string>)new[] { data.Item1, data.Item2, data.Item3 });
                    writer.WriteRow(data.Item1, data.Item2, data.Item3);
                writer.Flush();
                buffer = stream.ToArray();
            }

            using (MemoryStream stream = new MemoryStream(buffer))
            using (CsvReader reader = new CsvReader(stream, settings))
            {
                string[] row;
                while (reader.ReadRow(out row))
                {
                    Assert.AreEqual(3, row.Length);
                    var s = string.Join(", ", row);
                    actual.Add((row[0], row[1], row[2]));
                }
                CollectionAssert.AreEqual(TestData, actual);
            }
        }
    }
}
