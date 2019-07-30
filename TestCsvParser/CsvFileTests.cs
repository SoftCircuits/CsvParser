// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftCircuits.CsvParser;
using SoftCircuits.CsvParser.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace CsvParserTests
{
    [TestClass]
    public class CsvFileTests
    {
        private static readonly string Folder;

        static CsvFileTests()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            Folder = Path.Combine(path, "CsvParserTests");
            if (!Directory.Exists(Folder))
                Directory.CreateDirectory(Folder);
        }

        private class Person : IEquatable<Person>
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Zip { get; set; }
            public DateTime Birthday { get; set; }

            #region Equality

            public override int GetHashCode()
            {
                return HashCode.Combine(Id, Name, Zip, Birthday);
            }

            public override bool Equals(object obj) => Equals(obj as Person);

            public bool Equals(Person other)
            {
                return other != null &&
                       Id == other.Id &&
                       Name == other.Name &&
                       Zip == other.Zip &&
                       Birthday == other.Birthday;
            }

            #endregion
        }

        List<Person> People = new List<Person>
        {
            new Person { Id = 1, Name = "Bill Smith", Zip = "92869", Birthday = new DateTime(1972, 10, 29) },
            new Person { Id = 2, Name = "Susan Carpenter", Zip = "92865", Birthday = new DateTime(1985, 2, 17) },
            new Person { Id = 3, Name = "Jim Windsor", Zip = "92862", Birthday = new DateTime(1979, 4, 23) },
            new Person { Id = 4, Name = "Jill Morrison", Zip = "92861", Birthday = new DateTime(1969, 5, 2) },
            new Person { Id = 5, Name = "Gary Wright", Zip = "92868", Birthday = new DateTime(1974, 2, 18) },
        };

        [TestMethod]
        public void WriteAndRead()
        {
            string path = Path.Combine(Folder, "Example1.csv");

            using (CsvWriter writer = new CsvWriter(path))
            {
                writer.WriteRow("Id", "Name", "Zip", "Birthday");
                writer.WriteRow("1", "Bill Smith", "92869", "10/29/1972 12:00:00 AM");
                writer.WriteRow("2", "Susan Carpenter", "92865", "2/17/1985 12:00:00 AM");
                writer.WriteRow("3", "Jim Windsor", "92862", "4/23/1979 12:00:00 AM");
                writer.WriteRow("4", "Jill Morrison", "92861", "5/2/1969 12:00:00 AM");
                writer.WriteRow("5", "Gary Wright", "92868", "2/18/1974 12:00:00 AM");
            }

            string[] columns = null;
            using (CsvReader reader = new CsvReader(path))
            {
                // Read header row
                reader.ReadRow(ref columns);
                // Read data rows
                int i = 0;
                while (reader.ReadRow(ref columns))
                {
                    Assert.AreEqual(People[i].Id.ToString(), columns[0]);
                    Assert.AreEqual(People[i].Name, columns[1]);
                    Assert.AreEqual(People[i].Zip, columns[2]);
                    Assert.AreEqual(People[i].Birthday.ToString(), columns[3]);
                    i++;
                }
                Assert.AreEqual(People.Count, i);
            }
        }

        [TestMethod]
        public void WriteAndReadTabDelimited()
        {
            string path = Path.Combine(Folder, "Example2.tsv");

            CsvSettings settings = new CsvSettings();
            settings.ColumnDelimiter = '\t';

            using (CsvWriter writer = new CsvWriter(path, settings))
            {
                writer.WriteRow("Id", "Name", "Zip", "Birthday");
                writer.WriteRow("1", "Bill Smith", "92869", "10/29/1972 12:00:00 AM");
                writer.WriteRow("2", "Susan Carpenter", "92865", "2/17/1985 12:00:00 AM");
                writer.WriteRow("3", "Jim Windsor", "92862", "4/23/1979 12:00:00 AM");
                writer.WriteRow("4", "Jill Morrison", "92861", "5/2/1969 12:00:00 AM");
                writer.WriteRow("5", "Gary Wright", "92868", "2/18/1974 12:00:00 AM");
            }

            string[] columns = null;
            using (CsvReader reader = new CsvReader(path, settings))
            {
                // Read header row
                reader.ReadRow(ref columns);
                // Read data rows
                int i = 0;
                while (reader.ReadRow(ref columns))
                {
                    Assert.AreEqual(People[i].Id.ToString(), columns[0]);
                    Assert.AreEqual(People[i].Name, columns[1]);
                    Assert.AreEqual(People[i].Zip, columns[2]);
                    Assert.AreEqual(People[i].Birthday.ToString(), columns[3]);
                    i++;
                }
                Assert.AreEqual(People.Count, i);
            }
        }

        [TestMethod]
        public void WriteAndReadObjects()
        {
            string path = Path.Combine(Folder, "Example3.csv");

            using (CsvDataWriter<Person> writer = new CsvDataWriter<Person>(path))
            {
                writer.WriteHeaders();
                foreach (Person person in People)
                    writer.Write(person);
            }

            List<Person> people = new List<Person>();
            using (CsvDataReader<Person> reader = new CsvDataReader<Person>(path))
            {
                // Read header and use to determine column order
                reader.ReadHeaders(true);
                // Read data
                while (reader.Read(out Person person))
                    people.Add(person);
            }
            CollectionAssert.AreEqual(People, people);
        }

        private class Person2
        {
            [ColumnMap(Exclude = true)]
            public int Id { get; set; }
            [ColumnMap(Index = 2, Name = "abc")]
            public string Name { get; set; }
            [ColumnMap(Index = 1, Name = "def")]
            public string Zip { get; set; }
            [ColumnMap(Index = 0, Name = "ghi")]
            public DateTime Birthday { get; set; }

            [ColumnMap(Index = 0)]
            private int NotUsed;
        }

        [TestMethod]
        public void WriteAndReadObjectsWithAttributes()
        {
            string path = Path.Combine(Folder, "Example4.csv");

            // Populate data
            List<Person2> people2 = new List<Person2>();
            foreach (Person person in People)
                people2.Add(new Person2
                {
                    Id = person.Id,
                    Name = person.Name,
                    Zip = person.Zip,
                    Birthday = person.Birthday
                });

            using (CsvDataWriter<Person2> writer = new CsvDataWriter<Person2>(path))
            {
                writer.WriteHeaders();
                foreach (Person2 person in people2)
                    writer.Write(person);
            }

            List<Person2> people = new List<Person2>();
            using (CsvDataReader<Person2> reader = new CsvDataReader<Person2>(path))
            {
                // Read header and use to determine column order
                reader.ReadHeaders(true);
                // Read data
                while (reader.Read(out Person2 person))
                    people.Add(person);
            }
            Assert.AreEqual(people2.Count, people.Count);
            for (int i = 0; i < people2.Count; i++)
            {
                Assert.AreEqual(people2[i].Name, people[i].Name);
                Assert.AreEqual(people2[i].Zip, people[i].Zip);
                Assert.AreEqual(people2[i].Birthday, people[i].Birthday);
                // Excluded properties
                Assert.AreEqual(0, people[i].Id);
            }
        }

        /// <summary>
        /// Custom DateTime converter. Stores a date-only value (no time) in a
        /// compact format.
        /// </summary>
        class DateTimeConverter : DataConverter<DateTime>
        {
            public override string ConvertToString(DateTime value)
            {
                int i = ((value.Day - 1) & 0b00011111) |
                    (((value.Month - 1) & 0b00001111) << 5) |
                    (value.Year) << 9;
                return i.ToString("x");
            }

            public override bool TryConvertFromString(string s, out DateTime value)
            {
                try
                {
                    int i = Convert.ToInt32(s, 16);
                    value = new DateTime(i >> 9, ((i >> 5) & 0b00001111) + 1, (i & 0b11111) + 1);
                    return true;
                }
                catch (Exception)
                {
                    value = DateTime.Now;
                    return false;
                }
            }
        }

        class PersonMaps : ColumnMaps<Person>
        {
            public PersonMaps()
            {
                MapColumn(m => m.Id).Exclude(true);
                MapColumn(m => m.Name).Index(2).Name("abc");
                MapColumn(m => m.Zip).Index(1).Name("def");
                MapColumn(m => m.Birthday).Index(0).Name("ghi").Converter(new DateTimeConverter());
            }
        }

        [TestMethod]
        public void WriteAndReadObjectsWithColumnMapping()
        {
            string path = Path.Combine(Folder, "Example5.csv");

            using (CsvDataWriter<Person> writer = new CsvDataWriter<Person>(path))
            {
                writer.MapColumns<PersonMaps>();

                writer.WriteHeaders();
                foreach (Person person in People)
                    writer.Write(person);
            }

            List<Person> people = new List<Person>();
            using (CsvDataReader<Person> reader = new CsvDataReader<Person>(path))
            {
                reader.MapColumns<PersonMaps>();

                // Read header
                reader.ReadHeaders(false);
                // Read data
                while (reader.Read(out Person person))
                    people.Add(person);
            }
            Assert.AreEqual(people.Count, people.Count);
            for (int i = 0; i < people.Count; i++)
            {
                Assert.AreEqual(people[i].Name, people[i].Name);
                Assert.AreEqual(people[i].Zip, people[i].Zip);
                Assert.AreEqual(people[i].Birthday, people[i].Birthday);
                // Excluded properties
                Assert.AreEqual(0, people[i].Id);
            }
        }
    }
}
