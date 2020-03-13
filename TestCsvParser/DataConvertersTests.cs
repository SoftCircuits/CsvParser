// Copyright (c) 2019-2020 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftCircuits.CsvParser;
using System.Collections.Generic;

namespace CsvParserTests
{
    [TestClass]
    public class DataConvertersTests
    {
        [TestMethod]
        public void DataConvertersTest()
        {
            using (MemoryFile file = new MemoryFile())
            {
                List<DataConvertersTestClass> results = new List<DataConvertersTestClass>();
                using (CsvWriter<DataConvertersTestClass> writer = new CsvWriter<DataConvertersTestClass>(file))
                {
                    writer.WriteHeaders();
                    foreach (var item in DataConvertersTestClass.TestData)
                        writer.Write(item);
                }

                using (CsvReader<DataConvertersTestClass> reader = new CsvReader<DataConvertersTestClass>(file))
                {
                    reader.ReadHeaders(true);
                    while (reader.Read(out DataConvertersTestClass item))
                        results.Add(item);
                }
                CollectionAssert.AreEqual(DataConvertersTestClass.TestData, results);
            }
        }
    }
}
