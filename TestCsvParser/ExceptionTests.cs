// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftCircuits.CsvParser;
using SoftCircuits.CsvParser.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace CsvParserTests
{
    [TestClass]
    public class ExceptionTests
    {
        class UnsupportedType
        {
            public string SSN { get; set; }
            public bool IsCitizen { get; set; }
        }

        class Customer
        {
            // Public properties
            public string Name { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public string Region { get; set; }
            public string Zip { get; set; }
            public int Age { get; set; }
            public double Score { get; set; }
            public bool IsRegistered { get; set; }
            public UnsupportedType UnsupportedType { get; set; }

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
        Customer[] Customers = new Customer[]
        {
            new Customer("Rafael Pitts", "Ap #883-4246 Nunc Avenue", "Maiduguri", "BO", "52319", 19, 123.45, true),
            new Customer("Joel Schmidt", "6768 Dictum Street", "Berlin", "Berlin", "86692", 52, 5.9, true),
            new Customer("Alden Horn", "521 Consequat, Street", "Whithorn", "Wigtownshire", "46603", 42, 1.0, false),
        };

        // Create a custom data converter for DateTime values
        // Stores a date-only value (no time) in a compact format
        class DateTimeConverter : DataConverter<DateTime>
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
        class CustomerMaps : ColumnMaps<Customer>
        {
            public CustomerMaps()
            {
                MapColumn(m => m.UnsupportedType).Converter(new DateTimeConverter());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(DataConverterTypeMismatchException))]
        public void DataConverterTypeMismatchExceptionTest()
        {
            byte[] buffer;

            using (MemoryStream stream = new MemoryStream())
            using (CsvDataWriter<Customer> writer = new CsvDataWriter<Customer>(stream))
            {
                writer.MapColumns<CustomerMaps>();
                writer.WriteHeaders();
                writer.Write(Customers);

                writer.Flush();
                buffer = stream.ToArray();
            }

            //string text = Encoding.UTF8.GetString(buffer);

            using (MemoryStream stream = new MemoryStream(buffer))
            using (CsvDataReader<Customer> reader = new CsvDataReader<Customer>(stream))
            {
                reader.MapColumns<CustomerMaps>();
                reader.ReadHeaders(true);
                while (reader.Read(out Customer person))
                    Debug.WriteLine(person);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(BadDataFormatException))]
        public void InvalidDataExceptionTest()
        {
            byte[] buffer;

            using (MemoryStream stream = new MemoryStream())
            using (CsvWriter writer = new CsvWriter(stream))
            {
                writer.WriteRow("Name", "Street", "City", "Region", "Zip", "Age", "Score", "IsRegistered");
                writer.WriteRow("Rafael Pitts", "Ap #883-4246 Nunc Avenue", "Maiduguri", "BO", "52319", "19", "123.45", "true");
                writer.WriteRow("Joel Schmidt", "6768 Dictum Street", "Berlin", "Berlin", "86692", "52", "5.9", "true");
                writer.WriteRow("Alden Horn", "521 Consequat, Street", "Whithorn", "Wigtownshire", "46603", "abc", "1.0", "false");

                writer.Flush();
                buffer = stream.ToArray();
            }

            //string text = Encoding.UTF8.GetString(buffer);

            using (MemoryStream stream = new MemoryStream(buffer))
            using (CsvDataReader<Customer> reader = new CsvDataReader<Customer>(stream))
            {
                reader.ReadHeaders(true);
                while (reader.Read(out Customer person))
                    Debug.WriteLine(person);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(UnsupportedDataTypeException))]
        public void UnsupportedDataTypeTest()
        {
            byte[] buffer;

            using (MemoryStream stream = new MemoryStream())
            using (CsvDataWriter<Customer> writer = new CsvDataWriter<Customer>(stream))
            {
                writer.WriteHeaders();
                writer.Write(Customers);

                writer.Flush();
                buffer = stream.ToArray();
            }

            //string text = Encoding.UTF8.GetString(buffer);

            using (MemoryStream stream = new MemoryStream(buffer))
            using (CsvDataReader<Customer> reader = new CsvDataReader<Customer>(stream))
            {
                reader.ReadHeaders(true);
                while (reader.Read(out Customer person))
                    Debug.WriteLine(person);
            }
        }
    }
}
