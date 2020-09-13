// Copyright (c) 2019-2020 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftCircuits.CsvParser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
                using (CsvWriter<DataConvertersTestClass> writer = new CsvWriter<DataConvertersTestClass>(file))
                {
                    writer.WriteHeaders();
                    foreach (var item in DataConvertersTestClass.TestData)
                        writer.Write(item);
                }

                List<DataConvertersTestClass> results = new List<DataConvertersTestClass>();
                using (CsvReader<DataConvertersTestClass> reader = new CsvReader<DataConvertersTestClass>(file))
                {
                    reader.ReadHeaders(true);
                    while (reader.Read(out DataConvertersTestClass item))
                        results.Add(item);
                }
                CollectionAssert.AreEqual(DataConvertersTestClass.TestData, results);
            }
        }

        private class BooleanDataConverter : DataConverter<bool?>
        {
            private readonly Random Random;

            private readonly static HashSet<string> TrueStrings = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "True",
                "Yes",
                "T",
                "Y",
                "On",
                "1",
            };

            private readonly static HashSet<string> FalseStrings = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "False",
                "No",
                "F",
                "N",
                "Off",
                "0",
            };

            public BooleanDataConverter()
            {
                Random = new Random();
            }

            public override string ConvertToString(bool? value)
            {
                if (value.HasValue)
                {
                    // Randomly choose a format for testing purposes
                    Debug.Assert(TrueStrings.Count == FalseStrings.Count);
                    int index = Random.Next(TrueStrings.Count);
                    return value.Value ? TrueStrings.ToArray()[index] : FalseStrings.ToArray()[index];
                }
                return null;
            }

            public override bool TryConvertFromString(string s, out bool? value)
            {
                s = string.IsNullOrWhiteSpace(s) ? string.Empty : s.Trim();
                if (s.Length == 0)
                {
                    value = null;
                    return true;
                }
                if (TrueStrings.Contains(s))
                {
                    value = true;
                    return true;
                }
                if (FalseStrings.Contains(s))
                {
                    value = false;
                    return true;
                }
                value = null;
                return false;
            }
        }

        private class CurrencyDataConverter : DataConverter<decimal?>
        {
            public override string ConvertToString(decimal? value) => value.HasValue ? value.Value.ToString("C") : string.Empty;

            public override bool TryConvertFromString(string s, out decimal? value)
            {
                s = string.IsNullOrWhiteSpace(s) ? null : s.Trim(' ', '$');
                if (s == null ||
                    s.Equals("N/A", StringComparison.OrdinalIgnoreCase) ||
                    s.Equals("NA", StringComparison.OrdinalIgnoreCase))
                {
                    value = null;
                    return true;
                }
                if (decimal.TryParse(s, out decimal v))
                {
                    value = v;
                    return true;
                }
                value = null;
                return false;
            }
        }

        private class Entry
        {
            public string String { get; set; }
            [ColumnMap(ConverterType = typeof(BooleanDataConverter))]
            public bool? Boolean { get; set; }
            [ColumnMap(ConverterType = typeof(CurrencyDataConverter))]
            public decimal? Currency { get; set; }

            #region IEquatable

            public override int GetHashCode() => HashCode.Combine(String, Boolean, Currency);

            public override bool Equals(object obj) => Equals(obj as Entry);

            public bool Equals(Entry other)
            {
                if (other == null)
                    return false;
                return String == other.String &&
                    Boolean == other.Boolean &&
                    Currency == other.Currency;
            }

            #endregion

        }

        private readonly List<Entry> EntryData = new List<Entry>
        {
            new Entry { String = "Abc", Boolean = false, Currency = 123.45m },
            new Entry { String = "Def", Boolean = true, Currency = 678.90m },
            new Entry { String = "Ghi", Boolean = true, Currency = 34.18m },
            new Entry { String = "Jkl", Boolean = false, Currency = 87.22m },
            new Entry { String = "Mno", Boolean = true, Currency = 107.15m },
            new Entry { String = "Pqr", Boolean = false, Currency = 99.98m },
            new Entry { String = "Stu", Boolean = false, Currency = 14.27m },
            new Entry { String = "Vwx", Boolean = true, Currency = 386.21m },
            new Entry { String = "Yz", Boolean = true, Currency = 543.21m },
        };

        [TestMethod]
        public void DataConverterAttributeTest()
        {
            using (MemoryFile file = new MemoryFile())
            {
                using (CsvWriter<Entry> writer = new CsvWriter<Entry>(file))
                {
                    writer.WriteHeaders();
                    foreach (var item in EntryData)
                        writer.Write(item);
                }

                List<Entry> results = new List<Entry>();
                using (CsvReader<Entry> reader = new CsvReader<Entry>(file))
                {
                    reader.ReadHeaders(true);
                    while (reader.Read(out Entry item))
                        results.Add(item);
                }
                CollectionAssert.AreEqual(EntryData, results);
            }
        }

        private class InvalidEntry
        {
            public string String { get; set; }
            [ColumnMap(ConverterType = typeof(string))] // Convert type cannot be type string
            public bool? Boolean { get; set; }
        }

        private readonly List<InvalidEntry> InvalidEntryData = new List<InvalidEntry>();

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DataConvertAttributeInvalidTypeTest()
        {
            using (MemoryFile file = new MemoryFile())
            {
                using (CsvWriter<InvalidEntry> writer = new CsvWriter<InvalidEntry>(file))
                {
                    writer.WriteHeaders();
                    foreach (var item in InvalidEntryData)
                        writer.Write(item);
                }
            }
        }
    }
}
