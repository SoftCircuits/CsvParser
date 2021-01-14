// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftCircuits.CsvParser;
using System.Collections.Generic;

namespace CsvParserTests
{
    #region Data classes

    class CsvDataA
    {
        public int One { get; set; }
        public string Two { get; set; }
        public int Three { get; set; }
        public string Four { get; set; }
        public int Five { get; set; }
    }

    class CsvDataB
    {
        public int One { get; set; }
        [ColumnMap(Exclude = true)]
        public string Two { get; set; }
        [ColumnMap(Exclude = true)]
        public int Three { get; set; }
        public string Four { get; set; }
        public int Five { get; set; }
    }

    class CsvDataC
    {
        [ColumnMap(Index = 4)]
        public int Id { get; set; }
        [ColumnMap(Index = 3, Exclude = true)]
        public string Name { get; set; }
        [ColumnMap(Index = 2)]
        public string Address1 { get; set; }
        [ColumnMap(Index = 1)]
        public string Address2 { get; set; }
        [ColumnMap(Index = 0)]
        public string Email { get; set; }
    }

    #endregion

    #region Column mapping classes

    class CsvDataMapNone<T> : ColumnMaps<T> where T : class, new()
    {
        public CsvDataMapNone()
        { }
    }

    class CsvDataAMapReverse : ColumnMaps<CsvDataA>
    {
        public CsvDataAMapReverse()
        {
            MapColumn(m => m.One).Index(4);
            MapColumn(m => m.Two).Index(3);
            MapColumn(m => m.Three).Index(2);
            MapColumn(m => m.Four).Index(1);
            MapColumn(m => m.Five).Index(0);
        }
    }

    class CsvDataBMapInclude : ColumnMaps<CsvDataB>
    {
        public CsvDataBMapInclude()
        {
            MapColumn(m => m.Two).Exclude(false);
            MapColumn(m => m.Three).Exclude(false);
        }
    }

    class CsvDataBMapIncludeAndReverse : ColumnMaps<CsvDataB>
    {
        public CsvDataBMapIncludeAndReverse()
        {
            MapColumn(m => m.One).Index(4);
            MapColumn(m => m.Two).Index(3).Exclude(false);
            MapColumn(m => m.Three).Index(2).Exclude(false);
            MapColumn(m => m.Four).Index(1);
            MapColumn(m => m.Five).Index(0);
        }
    }

    class CsvDataCMapInclude : ColumnMaps<CsvDataC>
    {
        public CsvDataCMapInclude()
        {
            MapColumn(m => m.Name).Exclude(false);
        }
    }

    #endregion

    /// <summary>
    /// Test column mapping.
    /// </summary>
    [TestClass]
    public class CsvDataColumnsTests
    {
        /// <summary>
        /// Tests correct column ordering using column attributes, mapping and header mapping.
        /// </summary>
        [TestMethod]
        public void TestCsvDataColumns()
        {
            string input, output, results;

            // No mapping
            input = @"One,Two,Three,Four,Five
1,2,3,4,5";
            output = input;
            results = RunTest<CsvDataA, CsvDataMapNone<CsvDataA>>(input, false);
            Assert.AreEqual(output, results);

            // Misspelled column
            input = @"One,TwoX,Three,Four,Five
1,2,3,4,5";
            output = @"One,Two,Three,Four,Five
1,,3,4,5";
            results = RunTest<CsvDataA, CsvDataMapNone<CsvDataA>>(input, true);
            Assert.AreEqual(output, results);

            // Reverse columns via mapping headers
            input = @"Five,Four,Three,Two,One
5,4,3,2,1";
            output = @"One,Two,Three,Four,Five
1,2,3,4,5";
            results = RunTest<CsvDataA, CsvDataMapNone<CsvDataA>>(input, true);
            Assert.AreEqual(output, results);

            // Reverse columns via column mapping
            input = @"Five,Four,Three,Two,One
5,4,3,2,1";
            output = input;
            results = RunTest<CsvDataA, CsvDataAMapReverse>(input, false);
            Assert.AreEqual(output, results);

            // Include excluded column via column mapping
            input = @"One,Two,Three,Four,Five
1,2,3,4,5";
            output = input;
            results = RunTest<CsvDataB, CsvDataBMapInclude>(input, false);
            Assert.AreEqual(output, results);

            // Include excluded column and reverse columns via column mapping
            input = @"Five,Four,Three,Two,One
5,4,3,2,1";
            output = @"Five,Four,Three,Two,One
5,4,3,2,1";
            results = RunTest<CsvDataB, CsvDataBMapIncludeAndReverse>(input, false);
            Assert.AreEqual(output, results);

            // No additional mapping
            input = @"Email,Address2,Address1,Name,Id
name@company.com,,1241 Willow Lane,Bob Smith,12345";
            output = @"Email,Address2,Address1,Id
name@company.com,,1241 Willow Lane,12345";
            results = RunTest<CsvDataC, CsvDataMapNone<CsvDataC>>(input, false);
            Assert.AreEqual(output, results);

            // Header mapping
            input = @"Id,Name,Address1,Address2,Email
12345,Bob Smith,1241 Willow Lane,,name@company.com";
            output = @"Email,Address2,Address1,Name,Id
name@company.com,,1241 Willow Lane,Bob Smith,12345";
            results = RunTest<CsvDataC, CsvDataCMapInclude>(input, true);
            Assert.AreEqual(output, results);

            // Misspelled column
            input = @"Id,NameX,Address1,Address2,Email
12345,Bob Smith,1241 Willow Lane,,name@company.com";
            output = @"Email,Address2,Address1,Name,Id
name@company.com,,1241 Willow Lane,,12345";
            results = RunTest<CsvDataC, CsvDataCMapInclude>(input, true);
            Assert.AreEqual(output, results);
        }

        private string RunTest<DATA_TYPE, MAPPING_TYPE>(string fileContents, bool mapColumnsFromHeaders)
            where DATA_TYPE : class, new()
            where MAPPING_TYPE : ColumnMaps<DATA_TYPE>, new()
        {
            using (MemoryFile file = new MemoryFile(fileContents))
            {
                List<DATA_TYPE> items = new List<DATA_TYPE>();

                using (CsvReader<DATA_TYPE> reader = new CsvReader<DATA_TYPE>(file))
                {
                    reader.MapColumns<MAPPING_TYPE>();
                    Assert.IsTrue(reader.ReadHeaders(mapColumnsFromHeaders));
                    while (reader.Read(out DATA_TYPE item))
                        items.Add(item);
                }

                file.Reset();
                using (CsvWriter<DATA_TYPE> writer = new CsvWriter<DATA_TYPE>(file))
                {
                    writer.MapColumns<MAPPING_TYPE>();
                    writer.WriteHeaders();
                    writer.Write(items);
                }

                return file.ToString().Trim();
            }
        }
    }
}
