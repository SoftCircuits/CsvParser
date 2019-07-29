// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftCircuits.CsvParser;
using SoftCircuits.CsvParser.Converters;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CsvParserTests
{
    [TestClass]
    public class CsvDataParserTests
    {
        // Test class
        class Person
        {
            // Public properties
            public string Name { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public string Region { get; set; }
            public string Zip { get; set; }

            // Private fields
            private int Age;
            private double Score;

            public bool IsRegistered { get; set; }

            // Access to private fields
            public int GetAge() => Age;
            public double GetScore() => Score;

            // Must have parameterless constructor
            public Person()
            {
            }

            public Person(string name, string street, string city, string region, string zip, int age, double score, bool isRegistered)
            {
                Name = name;
                Street = street;
                City = city;
                Region = region;
                Zip = zip;
                Age = age;
                Score = score;
                IsRegistered = isRegistered;
            }
        }

        // Class with mapping attributes
        class PersonAttributes
        {
            [ColumnMap(Name = "emaN", Index = 7)]
            public string Name { get; set; }
            [ColumnMap(Name="teertS", Index = 6)]
            public string Street { get; set; }
            [ColumnMap(Name = "ytiC", Index = 5)]
            public string City { get; set; }
            [ColumnMap(Name = "noigeR", Index = 4, Exclude = false)]
            public string Region { get; set; }
            [ColumnMap(Name = "piZ", Index = 3)]
            public string Zip { get; set; }
            [ColumnMap(Name = "egA", Index = 2)]
            public int Age { get; set; }
            [ColumnMap(Name = "erocS", Index = 1)]
            public double Score { get; set; }
            [ColumnMap(Name = "deretsigeRsI", Index = 0)]
            public bool IsRegistered { get; set; }

            // Must have parameterless constructor
            public PersonAttributes()
            {
            }

            public PersonAttributes(Person person)
            {
                Name = person.Name;
                Street = person.Street;
                City = person.City;
                Region = person.Region;
                Zip = person.Zip;
                Age = person.GetAge();
                Score = person.GetScore();
                IsRegistered = person.IsRegistered;
            }
        }

        // Test data
        Person[] People = new Person[]
        {
            new Person("Rafael Pitts", "Ap #883-4246 Nunc Avenue", "Maiduguri", "BO", "52319", 19, 123.45, true),
            new Person("Joel Schmidt", "6768 Dictum Street", "Berlin", "Berlin", "86692", 52, 5.9, true),
            new Person("Alden Horn", "521 Consequat, Street", "Whithorn", "Wigtownshire", "46603", 42, 1.0, false),
            new Person("Ashton Kelley", "Ap #172-4713 Nec Road", "Berlin", "Berlin", "292828", 56, 22.2, true),
            new Person("Price Campos", "918-6468 Nisl Road", "Torun", "KP", "42803", 18, 8.3, true),
            new Person("Burton Figueroa", "Ap #225-2629 At, Road", "Camarones", "Arica y Parinacota", "20805", 38, 71.4, false),
            new Person("Nash Gamble", "2346 Tellus St.", "Cap-de-la-Madeleine", "Quebec", "45300", 66, 0.12345678, false),
            new Person("Hiram Clay", "Ap #763-9136 Et Av.", "Palmerston North", "North Island", "6773", 54, 36.32, true),
            new Person("Lester Larson", "Ap #481-8577 Eleifend. St.", "Sudbury", "Suffolk", "4068 JA", 76, 44.21, false),
            new Person("Hilel Burch", "P.O. Box 598", "2214 Sagittis Street", "Flushing,Zeeland", "K94 5BH", 52, 38.4, true),
            new Person("Kevin Garner", "P.O. Box 677", "7520 Nec, Street", "Hamilton, Victoria", "24326", 76, 16.22, true),
            new Person("Quinn Shepherd", "Ap #366-3229 Eu Rd.", "Castello", "CV", "26624", 40, 9.0003, false),
            new Person("Merrill Vance", "324-1675 Ultricies Avenue", "Galway", "C", "2992", 21, 18.9, true),
            new Person("Raja Hayden", "P.O. Box 431", "1968 Ipsum. St.", "Ilheus,Bahia", "3817", 29, 4.002, true),
            new Person("Dolan Randolph", "Ap #279-3402 Ultrices St.", "Okene", "Kogi", "4902", 52, .0001, true),
            new Person("Dane Rodriguez","3998 Tellus, St.", "Waiheke Island", "NI", "51273", 43, .0023, false),
            new Person("Cole Landry", "655-6242 Felis Avenue", "Patna", "Bihar", "09211", 42, 22.16, true),
            new Person("Joshua Dotson", "P.O. Box 190, 7869 Integer Street", "Gladstone", "QLD", "553684", 73, 80.88, false),
            new Person("Abel Mckay", "Ap #660-9834 Erat Av.", "Etterbeek", "BU", "72-673", 38, 17.5, false),
            new Person("Bruce Crosby", "Ap #411-5503 Nisi. Street", "Trollhattan", "Vastra Gotalands lan", "04656", 60, 4.50, true),
            new Person("Hop Santana", "P.O. Box 908, 1315 Quisque St.", "Jonkoping", "F", "01744", 42, 16.2, true),
            new Person("Eaton Mcmahon", "311 Id, Road", "Badajoz", "EX", "52572", 77, 82.17, false),
            new Person("Fuller Lynch", "7315 Et Street", "Wellington", "NI", "E8H 5M2", 54, 99.0, true),
            new Person("Kelly Valencia", "2823 A St.", "Kisi", "Oyo", "4262 QE", 37, 43.1, false),
            new Person("Rudyard Randolph", "P.O. Box 481, 8001 Bibendum St.", "Motherwell", "Lanarkshire", "3932", 58, 0.127, false),
            new Person("Asher Acevedo", "5793 Parturient Rd.", "Vienna", "Wie", "09408", 59, 23.4, true),
            new Person("Josiah Porter", "Ap #681-1462 Sed St.", "San Jose de Alajuela", "Alajuela", "82006", 46, 16.3, true),
            new Person("Jackson Davidson", "7411 Vivamus Rd.", "Rutigliano", "PUG", "505744", 79, 10.1, false),
            new Person("Plato Mckee", "622-7508 Faucibus Ave", "Ludvika", "W", "R2M 0L2", 43, 82.22, true),
            new Person("Yardley Mcneil", "856-5558 Ligula Street", "Hamilton", "ON", "69-674", 49, 33.4, true),
            new Person("Zahir Drake", "Ap #166-9906 Nulla Av.", "Horsham", "Victoria", "29-317", 25, 27.6, false),
        };

        [TestMethod]
        public void TestDataHeaders()
        {
            byte[] buffer;
            List<Person> people = new List<Person>();

            using (MemoryStream stream = new MemoryStream())
            using (CsvDataWriter<Person> writer = new CsvDataWriter<Person>(stream))
            {
                writer.WriteHeaders();
                writer.Write(People);

                writer.Flush();
                buffer = stream.ToArray();
            }

            //string text = Encoding.UTF8.GetString(buffer);

            using (MemoryStream stream = new MemoryStream(buffer))
            using (CsvDataReader<Person> reader = new CsvDataReader<Person>(stream))
            {
                reader.ReadHeaders(true);
                while (reader.Read(out Person person))
                    people.Add(person);
            }

            Assert.AreEqual(People.Length, people.Count);
            if (People.Length == people.Count)
            {
                for (int i = 0; i < People.Length; i++)
                {
                    Assert.AreEqual(People[i].Name, people[i].Name);
                    Assert.AreEqual(People[i].Street, people[i].Street);
                    Assert.AreEqual(People[i].City, people[i].City);
                    Assert.AreEqual(People[i].Region, people[i].Region);
                    Assert.AreEqual(People[i].Zip, people[i].Zip);
                    Assert.AreEqual(People[i].GetAge(), people[i].GetAge());
                    Assert.AreEqual(People[i].GetScore(), people[i].GetScore());
                    Assert.AreEqual(People[i].IsRegistered, people[i].IsRegistered);
                }
            }
        }

        [TestMethod]
        public void TestDataAttributes()
        {
            byte[] buffer;
            List<PersonAttributes> people = new List<PersonAttributes>(People.Select(p => new PersonAttributes(p)));
            List<PersonAttributes> people2 = new List<PersonAttributes>();

            using (MemoryStream stream = new MemoryStream())
            using (CsvDataWriter<PersonAttributes> writer = new CsvDataWriter<PersonAttributes>(stream))
            {
                writer.WriteHeaders();
                writer.Write(people);

                writer.Flush();
                buffer = stream.ToArray();
            }

            //string text = Encoding.UTF8.GetString(buffer);

            using (MemoryStream stream = new MemoryStream(buffer))
            using (CsvDataReader<PersonAttributes> reader = new CsvDataReader<PersonAttributes>(stream))
            {
                reader.ReadHeaders(false);
                while (reader.Read(out PersonAttributes person))
                    people2.Add(person);
            }

            Assert.AreEqual(people.Count, people2.Count);
            if (people.Count == people2.Count)
            {
                for (int i = 0; i < People.Length; i++)
                {
                    Assert.AreEqual(people[i].Name, people2[i].Name);
                    Assert.AreEqual(people[i].Street, people2[i].Street);
                    Assert.AreEqual(people[i].City, people2[i].City);
                    Assert.AreEqual(people[i].Region, people2[i].Region);
                    Assert.AreEqual(people[i].Zip, people2[i].Zip);
                    Assert.AreEqual(people[i].Age, people2[i].Age);
                    Assert.AreEqual(people[i].Score, people2[i].Score);
                    Assert.AreEqual(people[i].IsRegistered, people2[i].IsRegistered);
                }
            }
        }

        class BoolConverter : DataConverter<bool>
        {
            public override string ConvertToString(bool value)
            {
                return value.ToString();
            }

            public override bool TryConvertFromString(string s, out bool value)
            {
                string lower = s.ToLower();
                value = lower == "true" ||
                    lower == "yes" ||
                    lower == "on" ||
                    lower == "1";
                return true;
            }
        }

        // Class property mapper
        class PersonMap : ColumnMaps<Person>
        {
            public PersonMap()
            {
                MapColumn(m => m.Name).Index(4);
                MapColumn(m => m.Street).Index(3).Name("Address");
                MapColumn(m => m.City).Index(2);
                MapColumn(m => m.Region).Index(1).Name("State");
                MapColumn(m => m.IsRegistered).Index(0).Converter(new BoolConverter());
                MapColumn(m => m.Zip).Exclude(true);
                MapColumn("Age").Exclude(true);
                MapColumn("Score").Exclude(true);
            }
        }

        [TestMethod]
        public void TestDataMapping()
        {
            byte[] buffer;
            List<Person> people = new List<Person>();

            using (MemoryStream stream = new MemoryStream())
            using (CsvDataWriter<Person> writer = new CsvDataWriter<Person>(stream))
            {
                writer.MapColumns<PersonMap>();
                writer.WriteHeaders();
                writer.Write(People);

                writer.Flush();
                buffer = stream.ToArray();
            }

            //string text = Encoding.UTF8.GetString(buffer);

            using (MemoryStream stream = new MemoryStream(buffer))
            using (CsvDataReader<Person> reader = new CsvDataReader<Person>(stream))
            {
                reader.MapColumns<PersonMap>();

                reader.ReadHeaders(true);
                while (reader.Read(out Person person))
                    people.Add(person);
            }

            Assert.AreEqual(People.Length, people.Count);
            if (People.Length == people.Count)
            {
                for (int i = 0; i < People.Length; i++)
                {
                    Assert.AreEqual(People[i].Name, people[i].Name);
                    Assert.AreEqual(People[i].Street, people[i].Street);
                    Assert.AreEqual(People[i].City, people[i].City);
                    Assert.AreEqual(People[i].Region, people[i].Region);
                    Assert.AreEqual(People[i].IsRegistered, people[i].IsRegistered);
                    // Excluded properties
                    Assert.AreEqual(null, people[i].Zip);
                    Assert.AreEqual(0, people[i].GetAge());
                    Assert.AreEqual(0.0, people[i].GetScore());
                }
            }
        }

        [TestMethod]
        public void TestArrayTypes()
        {
            byte[] buffer;
            List<Person> people = new List<Person>();

            using (MemoryStream stream = new MemoryStream())
            using (CsvDataWriter<Person> writer = new CsvDataWriter<Person>(stream))
            {
                writer.MapColumns<PersonMap>();
                writer.WriteHeaders();
                writer.Write(People);

                writer.Flush();
                buffer = stream.ToArray();
            }

            //string text = Encoding.UTF8.GetString(buffer);

            using (MemoryStream stream = new MemoryStream(buffer))
            using (CsvDataReader<Person> reader = new CsvDataReader<Person>(stream))
            {
                reader.MapColumns<PersonMap>();

                reader.ReadHeaders(true);
                while (reader.Read(out Person person))
                    people.Add(person);
            }

            Assert.AreEqual(People.Length, people.Count);
            if (People.Length == people.Count)
            {
                for (int i = 0; i < People.Length; i++)
                {
                    Assert.AreEqual(People[i].Name, people[i].Name);
                    Assert.AreEqual(People[i].Street, people[i].Street);
                    Assert.AreEqual(People[i].City, people[i].City);
                    Assert.AreEqual(People[i].Region, people[i].Region);
                    Assert.AreEqual(People[i].IsRegistered, people[i].IsRegistered);
                    // Excluded properties
                    Assert.AreEqual(null, people[i].Zip);
                    Assert.AreEqual(0, people[i].GetAge());
                    Assert.AreEqual(0.0, people[i].GetScore());
                }
            }
        }
    }
}
