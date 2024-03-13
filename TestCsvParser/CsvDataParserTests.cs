// Copyright (c) 2019-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftCircuits.CsvParser;
using System.Collections.Generic;
using System.Linq;

namespace CsvParserTests
{
    [TestClass]
    public class CsvDataParserTests
    {
        // Test class
        class Customer
        {
            // Public properties
            public string? Name { get; set; }
            public string? Street { get; set; }
            public string? City { get; set; }
            public string? Region { get; set; }
            public string? Zip { get; set; }

            // Private fields
            private readonly int Age;
            private readonly double Score;

            public bool IsRegistered { get; set; }

            // Access to private fields
            public int GetAge() => Age;
            public double GetScore() => Score;

            // Must have parameterless constructor
            public Customer()
            {
            }

            public Customer(string name, string street, string city, string region, string zip, int age, double score, bool isRegistered)
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
        class CustomerAttributes
        {
            [ColumnMap(Name = "emaN", Index = 7)]
            public string? Name { get; set; }
            [ColumnMap(Name="teertS", Index = 6)]
            public string? Street { get; set; }
            [ColumnMap(Name = "ytiC", Index = 5)]
            public string? City { get; set; }
            [ColumnMap(Name = "noigeR", Index = 4, Exclude = false)]
            public string? Region { get; set; }
            [ColumnMap(Name = "piZ", Index = 3)]
            public string? Zip { get; set; }
            [ColumnMap(Name = "egA", Index = 2)]
            public int Age { get; set; }
            [ColumnMap(Name = "erocS", Index = 1)]
            public double Score { get; set; }
            [ColumnMap(Name = "deretsigeRsI", Index = 0)]
            public bool IsRegistered { get; set; }

            // Must have parameterless constructor
            public CustomerAttributes()
            {
            }

            public CustomerAttributes(Customer customer)
            {
                Name = customer.Name;
                Street = customer.Street;
                City = customer.City;
                Region = customer.Region;
                Zip = customer.Zip;
                Age = customer.GetAge();
                Score = customer.GetScore();
                IsRegistered = customer.IsRegistered;
            }
        }

        // Test data
        readonly Customer[] Customers =
        [
            new Customer("Rafael Pitts", "Ap #883-4246 Nunc Avenue", "Maiduguri", "BO", "52319", 19, 123.45, true),
            new Customer("Joel Schmidt", "6768 Dictum Street", "Berlin", "Berlin", "86692", 52, 5.9, true),
            new Customer("Alden Horn", "521 Consequat, Street", "Whithorn", "Wigtownshire", "46603", 42, 1.0, false),
            new Customer("Ashton Kelley", "Ap #172-4713 Nec Road", "Berlin", "Berlin", "292828", 56, 22.2, true),
            new Customer("Price Campos", "918-6468 Nisl Road", "Torun", "KP", "42803", 18, 8.3, true),
            new Customer("Burton Figueroa", "Ap #225-2629 At, Road", "Camarones", "Arica y Parinacota", "20805", 38, 71.4, false),
            new Customer("Nash Gamble", "2346 Tellus St.", "Cap-de-la-Madeleine", "Quebec", "45300", 66, 0.12345678, false),
            new Customer("Hiram Clay", "Ap #763-9136 Et Av.", "Palmerston North", "North Island", "6773", 54, 36.32, true),
            new Customer("Lester Larson", "Ap #481-8577 Eleifend. St.", "Sudbury", "Suffolk", "4068 JA", 76, 44.21, false),
            new Customer("Hilel Burch", "P.O. Box 598", "2214 Sagittis Street", "Flushing,Zeeland", "K94 5BH", 52, 38.4, true),
            new Customer("Kevin Garner", "P.O. Box 677", "7520 Nec, Street", "Hamilton, Victoria", "24326", 76, 16.22, true),
            new Customer("Quinn Shepherd", "Ap #366-3229 Eu Rd.", "Castello", "CV", "26624", 40, 9.0003, false),
            new Customer("Merrill Vance", "324-1675 Ultricies Avenue", "Galway", "C", "2992", 21, 18.9, true),
            new Customer("Raja Hayden", "P.O. Box 431", "1968 Ipsum. St.", "Ilheus,Bahia", "3817", 29, 4.002, true),
            new Customer("Dolan Randolph", "Ap #279-3402 Ultrices St.", "Okene", "Kogi", "4902", 52, .0001, true),
            new Customer("Dane Rodriguez","3998 Tellus, St.", "Waiheke Island", "NI", "51273", 43, .0023, false),
            new Customer("Cole Landry", "655-6242 Felis Avenue", "Patna", "Bihar", "09211", 42, 22.16, true),
            new Customer("Joshua Dotson", "P.O. Box 190, 7869 Integer Street", "Gladstone", "QLD", "553684", 73, 80.88, false),
            new Customer("Abel Mckay", "Ap #660-9834 Erat Av.", "Etterbeek", "BU", "72-673", 38, 17.5, false),
            new Customer("Bruce Crosby", "Ap #411-5503 Nisi. Street", "Trollhattan", "Vastra Gotalands lan", "04656", 60, 4.50, true),
            new Customer("Hop Santana", "P.O. Box 908, 1315 Quisque St.", "Jonkoping", "F", "01744", 42, 16.2, true),
            new Customer("Eaton Mcmahon", "311 Id, Road", "Badajoz", "EX", "52572", 77, 82.17, false),
            new Customer("Fuller Lynch", "7315 Et Street", "Wellington", "NI", "E8H 5M2", 54, 99.0, true),
            new Customer("Kelly Valencia", "2823 A St.", "Kisi", "Oyo", "4262 QE", 37, 43.1, false),
            new Customer("Rudyard Randolph", "P.O. Box 481, 8001 Bibendum St.", "Motherwell", "Lanarkshire", "3932", 58, 0.127, false),
            new Customer("Asher Acevedo", "5793 Parturient Rd.", "Vienna", "Wie", "09408", 59, 23.4, true),
            new Customer("Josiah Porter", "Ap #681-1462 Sed St.", "San Jose de Alajuela", "Alajuela", "82006", 46, 16.3, true),
            new Customer("Jackson Davidson", "7411 Vivamus Rd.", "Rutigliano", "PUG", "505744", 79, 10.1, false),
            new Customer("Plato Mckee", "622-7508 Faucibus Ave", "Ludvika", "W", "R2M 0L2", 43, 82.22, true),
            new Customer("Yardley Mcneil", "856-5558 Ligula Street", "Hamilton", "ON", "69-674", 49, 33.4, true),
            new Customer("Zahir Drake", "Ap #166-9906 Nulla Av.", "Horsham", "Victoria", "29-317", 25, 27.6, false),
        ];

        [TestMethod]
        public void TestHeaderMapping()
        {
            using MemoryFile file = new();

            List<Customer> results = [];

            using (CsvWriter<Customer> writer = new(file))
            {
                writer.WriteHeaders();
                writer.Write(Customers);
            }

            using (CsvReader<Customer> reader = new(file))
            {
                reader.ReadHeaders(true);
                Customer? customer;
                while ((customer = reader.Read()) != null)
                    results.Add(customer);
            }

            Assert.AreEqual(Customers.Length, results.Count);
            if (Customers.Length == results.Count)
            {
                for (int i = 0; i < Customers.Length; i++)
                {
                    Assert.AreEqual(Customers[i].Name, results[i].Name);
                    Assert.AreEqual(Customers[i].Street, results[i].Street);
                    Assert.AreEqual(Customers[i].City, results[i].City);
                    Assert.AreEqual(Customers[i].Region, results[i].Region);
                    Assert.AreEqual(Customers[i].Zip, results[i].Zip);
                    Assert.AreEqual(Customers[i].GetAge(), results[i].GetAge());
                    Assert.AreEqual(Customers[i].GetScore(), results[i].GetScore());
                    Assert.AreEqual(Customers[i].IsRegistered, results[i].IsRegistered);
                }
            }
        }

        [TestMethod]
        public void TestAttributeMapping()
        {
            using MemoryFile file = new();

            List<CustomerAttributes> customers = new(Customers.Select(p => new CustomerAttributes(p)));
            List<CustomerAttributes> result = [];

            using (CsvWriter<CustomerAttributes> writer = new(file))
            {
                writer.WriteHeaders();
                writer.Write(customers);
            }

            using (CsvReader<CustomerAttributes> reader = new(file))
            {
                reader.ReadHeaders(false);
                CustomerAttributes? customer;
                while ((customer = reader.Read()) != null)
                    result.Add(customer);
            }

            Assert.AreEqual(customers.Count, result.Count);
            if (customers.Count == result.Count)
            {
                for (int i = 0; i < Customers.Length; i++)
                {
                    Assert.AreEqual(customers[i].Name, result[i].Name);
                    Assert.AreEqual(customers[i].Street, result[i].Street);
                    Assert.AreEqual(customers[i].City, result[i].City);
                    Assert.AreEqual(customers[i].Region, result[i].Region);
                    Assert.AreEqual(customers[i].Zip, result[i].Zip);
                    Assert.AreEqual(customers[i].Age, result[i].Age);
                    Assert.AreEqual(customers[i].Score, result[i].Score);
                    Assert.AreEqual(customers[i].IsRegistered, result[i].IsRegistered);
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
        class CustomerMap : ColumnMaps<Customer>
        {
            public CustomerMap()
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
        public void TestMapColumnMapping()
        {
            using MemoryFile file = new();

            List<Customer> results = [];

            using (CsvWriter<Customer> writer = new(file))
            {
                writer.MapColumns<CustomerMap>();
                writer.WriteHeaders();
                writer.Write(Customers);
            }

            using (CsvReader<Customer> reader = new(file))
            {
                reader.MapColumns<CustomerMap>();

                reader.ReadHeaders(true);
                Customer? customer;
                while ((customer = reader.Read()) != null)
                    results.Add(customer);
            }

            Assert.AreEqual(Customers.Length, results.Count);
            if (Customers.Length == results.Count)
            {
                for (int i = 0; i < Customers.Length; i++)
                {
                    Assert.AreEqual(Customers[i].Name, results[i].Name);
                    Assert.AreEqual(Customers[i].Street, results[i].Street);
                    Assert.AreEqual(Customers[i].City, results[i].City);
                    Assert.AreEqual(Customers[i].Region, results[i].Region);
                    Assert.AreEqual(Customers[i].IsRegistered, results[i].IsRegistered);
                    // Excluded properties
                    Assert.AreEqual(null, results[i].Zip);
                    Assert.AreEqual(0, results[i].GetAge());
                    Assert.AreEqual(0.0, results[i].GetScore());
                }
            }
        }
    }
}
