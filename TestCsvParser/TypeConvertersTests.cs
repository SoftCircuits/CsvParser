// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftCircuits.CsvParser.Converters;
using System;
using System.Collections;

namespace CsvParserTests
{
    [TestClass]
    public class TypeConvertersTest
    {
        [TestMethod]
        public void TestDataConverters()
        {
            foreach ((Type Type, object Data) item in TypeConvertersTestData.TestItems)
            {
                IDataConverter converter = DataConverters.GetConverter(item.Type);
                string s = converter.ConvertToString(item.Data);
                Assert.AreEqual(true, converter.TryConvertFromString(s, out object data));
                if (item.Type.IsArray)
                {
                    if (item.Type.GetElementType() == typeof(DateTime) || item.Type.GetElementType() == typeof(DateTime?))
                    {
                        IEnumerator enum1 = ((Array)item.Data).GetEnumerator();
                        IEnumerator enum2 = ((Array)data).GetEnumerator();
                        while (enum1.MoveNext())
                        {
                            Assert.AreEqual(true, enum2.MoveNext());
                            CompareDates((DateTime?)enum1.Current, (DateTime?)enum2.Current);
                        }
                    }
                    else
                    {
                        CollectionAssert.AreEqual((Array)item.Data, (Array)data);
                    }
                }
                else if (item.Type == typeof(DateTime) || item.Type == typeof(DateTime?))
                {
                    CompareDates((DateTime?)item.Data, (DateTime?)data);
                }
                else Assert.AreEqual(item.Data, data);
            }
        }

        /// <summary>
        /// Because DateTime values can differ superficially, such as milliseconds
        /// and time zone, etc. This method just compares the major elements of
        /// two DateTime objects.
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        void CompareDates(DateTime? dt1, DateTime? dt2)
        {
            if (dt1 == null || dt1 == null)
            {
                // If one is null, both must be null
                Assert.AreEqual(dt1, dt2);
            }
            else
            {
                // Test DateTime elements
                Assert.AreEqual(dt1.Value.Year, dt2.Value.Year);
                Assert.AreEqual(dt1.Value.Month, dt2.Value.Month);
                Assert.AreEqual(dt1.Value.Day, dt2.Value.Day);
                Assert.AreEqual(dt1.Value.Hour, dt2.Value.Hour);
                Assert.AreEqual(dt1.Value.Minute, dt2.Value.Minute);
                Assert.AreEqual(dt1.Value.Second, dt2.Value.Second);
                //Assert.AreEqual(dt1.Value.Millisecond, dt2.Value.Millisecond);
            }
        }
    }
}
