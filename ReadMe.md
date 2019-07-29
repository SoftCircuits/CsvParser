# CSV Parser

[![NuGet version (SoftCircuits.CsvParser)](https://img.shields.io/nuget/v/SoftCircuits.CsvParser.svg?style=flat-square)](https://www.nuget.org/packages/SoftCircuits.CsvParser/)

```
Install-Package SoftCircuits.CsvParser
```

## Overview

CsvParser is a .NET library that makes it easy to work with comma-separated-values (CSV) files. It includes basic classes to read and write CSV columns and also higher level classes that can automatically map data between columns and class members. The library correctly handles column values that contain embedded commas, quotes or other special characters. It even supports column values that span multiple lines.

In addition, there are a number of option settings. For example, you can change the column delimiter to another character. Additional options include changing the quote character and how blank lines are handled.

## CsvWriter and CsvReader Classes

These classes provide the simplest way to read and write CSV files. The example below writes several rows of data to a CSV file and then reads it back. Note that the `CsvWriter.WriteRow()` method is overloaded to also accept `string[]` and `IEnumerable<string>`.

```cs
// Write some data to disk
using (CsvWriter writer = new CsvWriter(path))
{
    writer.WriteRow("Id", "Name", "Zip", "Birthday");
    writer.WriteRow("1", "Bill Smith", "92869", "10/29/1972 12:00:00 AM");
    writer.WriteRow("2", "Susan Carpenter", "92865", "2/17/1985 12:00:00 AM");
    writer.WriteRow("3", "Jim Windsor", "92862", "4/23/1979 12:00:00 AM");
    writer.WriteRow("4", "Jill Morrison", "92861", "5/2/1969 12:00:00 AM");
    writer.WriteRow("5", "Gary Wright", "92868", "2/18/1974 12:00:00 AM");
}
```

This example reads all rows from a CSV file.

```cs
// Read the data from disk
string[] columns = null;
using (CsvReader reader = new CsvReader(path))
{
    while (reader.ReadRow(ref columns))
        Console.WriteLine(string.Join(", ", columns));
}
```

## CsvDataWriter and CsvDataReader Classes

These higher-level classes will automatically map data between class members and CSV columns. The following example defines a class and a collection with several instances of that class. It then uses `CsvDataWriter` to write the data to a CSV file, and `CsvDataReader` to read it back again.

```cs
// This class will represent the data in the CSV file
class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Zip { get; set; }
    public DateTime Birthday { get; set; }
}

// Define some sample data
List<Person> People = new List<Person>
{
    new Person { Id = 1, Name = "Bill Smith", Zip = "92869", Birthday = new DateTime(1972, 10, 29) },
    new Person { Id = 2, Name = "Susan Carpenter", Zip = "92865", Birthday = new DateTime(1985, 2, 17) },
    new Person { Id = 3, Name = "Jim Windsor", Zip = "92862", Birthday = new DateTime(1979, 4, 23) },
    new Person { Id = 4, Name = "Jill Morrison", Zip = "92861", Birthday = new DateTime(1969, 5, 2) },
    new Person { Id = 5, Name = "Gary Wright", Zip = "92868", Birthday = new DateTime(1974, 2, 18) },
};

// Write the data to disk
using (CsvDataWriter<Person> writer = new CsvDataWriter<Person>(path))
{
    writer.WriteHeaders();
    foreach (Person person in People)
        writer.Write(person);
}

// Read the data from disk
List<Person> people = new List<Person>();
using (CsvDataReader<Person> reader = new CsvDataReader<Person>(path))
{
    // Read header and use to determine column order
    reader.ReadHeaders(true);
    // Read data
    while (reader.Read(out Person person))
        people.Add(person);
}
```

It is important to note in the above example where the code that writes the data calls `CsvDataWriter.WriteHeaders()`. This writes a row with the name of each column. The code that reads the data calls `CsvDataReader.ReadHeaders()` to read that header data. Because the argument to `CsvDataReader.ReadHeaders()` is `true`, this tells the code to use the header data to determine how to map the columns in the rest of the file. For example, maybe the columns are in a different order, or maybe some of the columns are excluded.

Here, the code maps the class members to columns based on the headers. This is critical to making the CSV classes work correctly. The following sections will talk about other ways to map class members to columns.

## ColumnMap Attribute

The `ColumnMapAttribute` can be applied to any class property or field to specify how that member is mapped to a CSV column. This attribute includes the following properties:

**Name:** Specifies a column name, allowing the column name to be different from the class member name.

**Index:** Specifies the column position for this member. Note that if not all indexes assigned are sequential and starting from 0, the actual index numbers can vary from the ones specified.

**Exclude:** Specifies that the class member should be excluded and not written or read to or from any column.

The following example modifies the `Person` class created earlier with `ColumnMap` attributes. The attributes are used to set the columns to be in the opposite order from how the class members are declared, gives the columns completely different names, and excludes the `Id` column.

With the `Person` class defined as follows, the previous example will work correctly without the calls to `WriteHeaders()` and `ReadHeaders()`.

```cs
// Add column mapping attributes to our data class
class Person
{
    [ColumnMap(Exclude = true)]
    public int Id { get; set; }
    [ColumnMap(Index = 2, Name = "nombre")]
    public string Name { get; set; }
    [ColumnMap(Index = 1, Name = "código postal")]
    public string Zip { get; set; }
    [ColumnMap(Index = 0, Name = "cumpleaños")]
    public DateTime Birthday { get; set; }
}
```

## Custom Mapping

Class members can also be mapped to columns using custom mapping. Custom mapping is useful if you cannot directly modify the class you are working with. Custom mapping allows you to do anything you can do with `ColumnMapAttribute` and, in addition, it also allows you to provide custom data converters.

Data converters convert class members to strings, and then back again from strings to class members. The CsvParser library includes converters for all basic types (including Guid and DateTime), nullable basic types, basic type arrays and nullable basic type arrays. But you can override the data convert used for any class member. You might want to write your own data converter to support custom member types, or when you are working with data not formatted as expected by the built-in data converters. For example, parsing `DateTime`s can be problematic as there are many different ways to format dates and time.

The following example starts by defining the `CustomDateTimeConverter` class. This class must implement the `IDataConverter` interface. The easiest way to do this in a type-safe manner is to derive your class from `DataConverter<T>`, where `T` is the type of the property or field you are converting. The `DataConverter<T>` class has two abstract methods that must be implemented, `ConvertToString()` and `TryConvertFromString()`.

Next, the example defines the `PersonMaps` class to define the custom mapping. This class must derive from `ColumnMaps<T>`, where `T` is the type of data class you are writing or reading to or from CSV files. The constructor of this class must call `MapColumn()` for each member it maps. This method supports a fluent interface to set the various mapping properties for each member. The meaning of these properties is described above in the *ColumnMap Attribute* section but supports one additional setting:

**Converter():** Specifies a custom data converter as described above. See the example below.

Finally, the example calls the `CsvDataWriter.MapColumns<T>()` method to register the custom mappings. The code that reads the CSV file also calls the `CsvDataReader.MapColumns<T>()` method in the same way. Both must use the same mapping in order for the data to be interpreted correctly. The easiest way to do this is to pass the same class to both methods.

```cs
// Create a custom data converter for DateTime values
// Stores a date-only value (no time) in a compact format
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

// Create our custom mapping class
class PersonMaps : ColumnMaps<Person>
{
    public PersonMaps()
    {
        MapColumn(m => m.Id).Exclude(true);
        MapColumn(m => m.Name).Index(2).Name("nombre");
        MapColumn(m => m.Zip).Index(1).Name("código postal");
        MapColumn(m => m.Birthday).Index(0).Name("cumpleaños").Converter(new DateTimeConverter());
    }
}

// Write sample data to disk
using (CsvDataWriter<Person> writer = new CsvDataWriter<Person>(path))
{
    // Register our custom mapping
    writer.MapColumns<PersonMaps>();

    writer.WriteHeaders();
    foreach (Person person in People)
        writer.Write(person);
}

// Read data from disk
List<Person> people = new List<Person>();
using (CsvDataReader<Person> reader = new CsvDataReader<Person>(path))
{
    // Register our custom mapping
    reader.MapColumns<PersonMaps>();

    // Read header
    reader.ReadHeaders(false);
    // Read data
    while (reader.Read(out Person person))
        people.Add(person);
}
```

Notice that the example above still calls `CsvDataWriter.WriteHeaders()` and `CsvDataReader.ReadHeaders()`. However, since the code has explicitly mapped all of the columns, this is just for completeness and is completely unnecessary. Also notice that `false` is passed to `CsvDataReader.ReadHeaders()` because we do not need the library to use the headers to determine column order, etc.

## CsvSettings Class

You can customize the way the library behaves by passing your own instance of the `CsvSettings` class to any of the reader or writer constructors, as demonstrated in the following example. This code uses the `CsvSettings` class to read a tab-separated-values (TSV) file. It sets the `ColumnDelimiter` property to a tab. It also sets it to use single quotes instead of double quotes (something you would probably rarely do, but is fully supported).

```cs
// Set custom settings
CsvSettings settings = new CsvSettings();
settings.ColumnDelimiter = '\t';
settings.QuoteCharacter = '\'';

// Apply custom settings to CsvReader
using (CsvReader reader = new CsvReader(path, settings))
{
    string[] columns = null;
    while (reader.ReadRow(ref columns))
        Console.WriteLine(string.Join(", ", columns));
}
```

## Additional Information

This code was originally derived from the article [Reading and Writing CSV Files in C#](http://www.blackbeltcoder.com/Articles/files/reading-and-writing-csv-files-in-c).
