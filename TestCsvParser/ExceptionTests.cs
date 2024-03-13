// Copyright (c) 2019-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftCircuits.CsvParser;
using System;
using System.Diagnostics;

namespace CsvParserTests
{
    [TestClass]
    public class ExceptionTests
    {
        private class UnsupportedType
        {
            public string? SSN { get; set; }
            public bool IsCitizen { get; set; }
        }

        private class Customer
        {
            // Public properties
            public string? Name { get; set; }
            public string? Street { get; set; }
            public string? City { get; set; }
            public string? Region { get; set; }
            public string? Zip { get; set; }
            public int Age { get; set; }
            public double Score { get; set; }
            public bool IsRegistered { get; set; }
            public UnsupportedType? UnsupportedType { get; set; }

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
                UnsupportedType = new UnsupportedType { SSN = "1111", IsCitizen = true };
            }
        }

        // Test data
        private readonly Customer[] Customers =
        [
            new Customer("Rafael Pitts", "Ap #883-4246 Nunc Avenue", "Maiduguri", "BO", "52319", 19, 123.45, true),
            new Customer("Joel Schmidt", "6768 Dictum Street", "Berlin", "Berlin", "86692", 52, 5.9, true),
            new Customer("Alden Horn", "521 Consequat, Street", "Whithorn", "Wigtownshire", "46603", 42, 1.0, false),
        ];

        // Create a custom data converter for DateTime values
        // Stores a date-only value (no time) in a compact format
        private class DateTimeConverter : DataConverter<DateTime>
        {
            public override string ConvertToString(DateTime value)
            {
                int i = ((value.Day - 1) & 0xb1f) |
                    (((value.Month - 1) & 0x0f) << 5) |
                    (value.Year) << 9;
                return i.ToString("x");
            }

            public override bool TryConvertFromString(string s, out DateTime value)
            {
                try
                {
                    int i = Convert.ToInt32(s, 16);
                    value = new DateTime(i >> 9, ((i >> 5) & 0x0f) + 1, (i & 0x1f) + 1);
                    return true;
                }
                catch (Exception)
                {
                    value = DateTime.Now;
                    return false;
                }
            }
        }

        // Create our custom mapping class
        private class CustomerMaps : ColumnMaps<Customer>
        {
            public CustomerMaps()
            {
                MapColumn(m => m.UnsupportedType).Converter(new DateTimeConverter());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(BadDataFormatException))]
        public void BadDataFormatExceptionTest()
        {
            using MemoryFile file = new();

            using (CsvWriter writer = new(file))
            {
                writer.Write("Name", "Street", "City", "Region", "Zip", "Age", "Score", "IsRegistered");
                writer.Write("Rafael Pitts", "Ap #883-4246 Nunc Avenue", "Maiduguri", "BO", "52319", "19", "123.45", "true");
                writer.Write("Joel Schmidt", "6768 Dictum Street", "Berlin", "Berlin", "86692", "52", "5.9", "true");
                writer.Write("Alden Horn", "521 Consequat, Street", "Whithorn", "Wigtownshire", "46603", "abc", "1.0", "false");
            }

            using CsvReader<Customer> reader = new(file);

            reader.ReadHeaders(true);
            Customer? person;
            while ((person = reader.Read()) != null)
                Debug.WriteLine(person);
        }

        [TestMethod]
        public void BadDataFormatExceptionSuppressionTest()
        {
            using MemoryFile file = new();

            using (CsvWriter writer = new(file))
            {
                writer.Write("Name", "Street", "City", "Region", "Zip", "Age", "Score", "IsRegistered");
                writer.Write("Rafael Pitts", "Ap #883-4246 Nunc Avenue", "Maiduguri", "BO", "52319", "19", "123.45", "true");
                writer.Write("Joel Schmidt", "6768 Dictum Street", "Berlin", "Berlin", "86692", "52", "5.9", "true");
                writer.Write("Alden Horn", "521 Consequat, Street", "Whithorn", "Wigtownshire", "46603", "abc", "1.0", "false");
            }

            CsvSettings settings = new() { InvalidDataRaisesException = false };
            using CsvReader<Customer> reader = new(file, settings);

            reader.ReadHeaders(true);
            Customer? person;
            while ((person = reader.Read()) != null)
                Debug.WriteLine(person);
        }

        [TestMethod]
        [ExpectedException(typeof(DataConverterTypeMismatchException))]
        public void DataConverterTypeMismatchExceptionTest()
        {
            using MemoryFile file = new();

            using (CsvWriter<Customer> writer = new(file))
            {
                writer.MapColumns<CustomerMaps>();
                writer.WriteHeaders();
                writer.Write(Customers);
            }

            CsvReader<Customer> reader = new(file);

            reader.MapColumns<CustomerMaps>();
            reader.ReadHeaders(true);
            Customer? person;
            while ((person = reader.Read()) != null)
                Debug.WriteLine(person);
        }

        [TestMethod]
        [ExpectedException(typeof(UnsupportedDataTypeException))]
        public void UnsupportedDataTypeTest()
        {
            using MemoryFile file = new();

            using (CsvWriter<Customer> writer = new(file))
            {
                writer.WriteHeaders();
                writer.Write(Customers);
            }

            using CsvReader<Customer> reader = new(file);

            reader.ReadHeaders(true);
            Customer? person;
            while ((person = reader.Read()) != null)
                Debug.WriteLine(person);
        }
    }
}
