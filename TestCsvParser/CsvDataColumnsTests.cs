// Copyright (c) 2019-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftCircuits.CsvParser;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsvParserTests
{
    #region Data classes

    class CsvDataA
    {
        public int One { get; set; }
        public string? Two { get; set; }
        public int Three { get; set; }
        public string? Four { get; set; }
        public int Five { get; set; }
    }

    class CsvDataB
    {
        public int One { get; set; }
        [ColumnMap(Exclude = true)]
        public string? Two { get; set; }
        [ColumnMap(Exclude = true)]
        public int Three { get; set; }
        public string? Four { get; set; }
        public int Five { get; set; }
    }

    class CsvDataC
    {
        [ColumnMap(Index = 4)]
        public int Id { get; set; }
        [ColumnMap(Index = 3, Exclude = true)]
        public string? Name { get; set; }
        [ColumnMap(Index = 2)]
        public string? Address1 { get; set; }
        [ColumnMap(Index = 1)]
        public string? Address2 { get; set; }
        [ColumnMap(Index = 0)]
        public string? Email { get; set; }
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
        public async Task TestCsvDataColumns()
        {
            string input, output, results;

            // No mapping
            input = @"One,Two,Three,Four,Five
1,2,3,4,5";
            output = input;
            results = RunTest<CsvDataA, CsvDataMapNone<CsvDataA>>(input, false);
            Assert.AreEqual(output, results);
            results = await RunTestAsync<CsvDataA, CsvDataMapNone<CsvDataA>>(input, false);
            Assert.AreEqual(output, results);

            // Misspelled column
            input = @"One,TwoX,Three,Four,Five
1,2,3,4,5";
            output = @"One,Two,Three,Four,Five
1,,3,4,5";
            results = RunTest<CsvDataA, CsvDataMapNone<CsvDataA>>(input, true);
            Assert.AreEqual(output, results);
            results = await RunTestAsync<CsvDataA, CsvDataMapNone<CsvDataA>>(input, true);
            Assert.AreEqual(output, results);

            // Reverse columns via mapping headers
            input = @"Five,Four,Three,Two,One
5,4,3,2,1";
            output = @"One,Two,Three,Four,Five
1,2,3,4,5";
            results = RunTest<CsvDataA, CsvDataMapNone<CsvDataA>>(input, true);
            Assert.AreEqual(output, results);
            results = await RunTestAsync<CsvDataA, CsvDataMapNone<CsvDataA>>(input, true);
            Assert.AreEqual(output, results);

            // Reverse columns via column mapping
            input = @"Five,Four,Three,Two,One
5,4,3,2,1";
            output = input;
            results = RunTest<CsvDataA, CsvDataAMapReverse>(input, false);
            Assert.AreEqual(output, results);
            results = await RunTestAsync<CsvDataA, CsvDataAMapReverse>(input, false);
            Assert.AreEqual(output, results);

            // Include excluded column via column mapping
            input = @"One,Two,Three,Four,Five
1,2,3,4,5";
            output = input;
            results = RunTest<CsvDataB, CsvDataBMapInclude>(input, false);
            Assert.AreEqual(output, results);
            results = await RunTestAsync<CsvDataB, CsvDataBMapInclude>(input, false);
            Assert.AreEqual(output, results);

            // Include excluded column and reverse columns via column mapping
            input = @"Five,Four,Three,Two,One
5,4,3,2,1";
            output = @"Five,Four,Three,Two,One
5,4,3,2,1";
            results = RunTest<CsvDataB, CsvDataBMapIncludeAndReverse>(input, false);
            Assert.AreEqual(output, results);
            results = await RunTestAsync<CsvDataB, CsvDataBMapIncludeAndReverse>(input, false);
            Assert.AreEqual(output, results);

            // No additional mapping
            input = @"Email,Address2,Address1,Name,Id
name@company.com,,1241 Willow Lane,Bob Smith,12345";
            output = @"Email,Address2,Address1,Id
name@company.com,,1241 Willow Lane,12345";
            results = RunTest<CsvDataC, CsvDataMapNone<CsvDataC>>(input, false);
            Assert.AreEqual(output, results);
            results = await RunTestAsync<CsvDataC, CsvDataMapNone<CsvDataC>>(input, false);
            Assert.AreEqual(output, results);

            // Header mapping
            input = @"Id,Name,Address1,Address2,Email
12345,Bob Smith,1241 Willow Lane,,name@company.com";
            output = @"Email,Address2,Address1,Name,Id
name@company.com,,1241 Willow Lane,Bob Smith,12345";
            results = RunTest<CsvDataC, CsvDataCMapInclude>(input, true);
            Assert.AreEqual(output, results);
            results = await RunTestAsync<CsvDataC, CsvDataCMapInclude>(input, true);
            Assert.AreEqual(output, results);

            // Misspelled column
            input = @"Id,NameX,Address1,Address2,Email
12345,Bob Smith,1241 Willow Lane,,name@company.com";
            output = @"Email,Address2,Address1,Name,Id
name@company.com,,1241 Willow Lane,,12345";
            results = RunTest<CsvDataC, CsvDataCMapInclude>(input, true);
            Assert.AreEqual(output, results);
            results = await RunTestAsync<CsvDataC, CsvDataCMapInclude>(input, true);
            Assert.AreEqual(output, results);
        }

        private static string RunTest<DATA_TYPE, MAPPING_TYPE>(string fileContents, bool mapColumnsFromHeaders)
            where DATA_TYPE : class, new()
            where MAPPING_TYPE : ColumnMaps<DATA_TYPE>, new()
        {
            using MemoryFile file = new(fileContents);

            List<DATA_TYPE> items = [];

            using (CsvReader<DATA_TYPE> reader = new(file))
            {
                reader.MapColumns<MAPPING_TYPE>();
                Assert.IsTrue(reader.ReadHeaders(mapColumnsFromHeaders));
                DATA_TYPE? item;
                while ((item = reader.Read()) != null)
                    items.Add(item);
            }

            file.Reset();
            using (CsvWriter<DATA_TYPE> writer = new(file))
            {
                writer.MapColumns<MAPPING_TYPE>();
                writer.WriteHeaders();
                writer.Write(items);
            }

            return file.ToString().Trim();
        }

        private static async Task<string> RunTestAsync<DATA_TYPE, MAPPING_TYPE>(string fileContents, bool mapColumnsFromHeaders)
            where DATA_TYPE : class, new()
            where MAPPING_TYPE : ColumnMaps<DATA_TYPE>, new()
        {
            using MemoryFile file = new(fileContents);

            List<DATA_TYPE> items = [];

            using (CsvReader<DATA_TYPE> reader = new(file))
            {
                reader.MapColumns<MAPPING_TYPE>();
                Assert.IsTrue(await reader.ReadHeadersAsync(mapColumnsFromHeaders));
                DATA_TYPE? item;
                while ((item = await reader.ReadAsync()) != null)
                    items.Add(item);
            }

            file.Reset();
            using (CsvWriter<DATA_TYPE> writer = new(file))
            {
                writer.MapColumns<MAPPING_TYPE>();
                await writer.WriteHeadersAsync();
                await writer.WriteAsync(items);
            }

            return file.ToString().Trim();
        }
    }
}
