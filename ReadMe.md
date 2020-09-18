<a href="https://www.buymeacoffee.com/jonathanwood" target="_blank"><img src="https://www.buymeacoffee.com/assets/img/custom_images/black_img.png" alt="Buy Me A Coffee" style="height: 37px !important;width: 170px !important;" ></a>

# CSV Parser

[![NuGet version (SoftCircuits.CsvParser)](https://img.shields.io/nuget/v/SoftCircuits.CsvParser.svg?style=flat-square)](https://www.nuget.org/packages/SoftCircuits.CsvParser/)

```
Install-Package SoftCircuits.CsvParser
```

## Overview

CsvParser is a .NET library that makes it easy to work with comma-separated-values (CSV) files. (Since you can customer the delimiter, it can also be used to work with files with any delimiter). CsvParser includes basic classes to read and write CSV data, and also higher-level classes that automatically map class properties to CSV columns. The library correctly handles column values that contain embedded commas, quotes or other special characters. It even supports column values that span multiple lines. CsvParser is very efficient and is designed to handle large data files without loading everything into memory. This library runs up to four times faster than the popular CsvHelper library.

## CsvWriter and CsvReader Classes

These classes provide the simplest way to read and write CSV files. The example below writes several rows of data to a CSV file and then reads it back.

```cs
// Write some data to disk
using (CsvWriter writer = new CsvWriter(path))
{
    // Header row
    writer.WriteRow("Name", "Email", "Phone", "Birthday");
    // Data rows
    writer.WriteRow("Bill Smith", "bsmith@domain.com", "555-1234", "10/29/1982 12:00:00 AM");
    writer.WriteRow("Susan Carpenter", "scarpenter@domain.com", "555-2345", "2/17/1995 12:00:00 AM");
    writer.WriteRow("Jim Windsor", "jwindsor@domain.com", "555-3456", "4/23/1989 12:00:00 AM");
    writer.WriteRow("Jill Morrison", "jmorrison@domain.com", "555-4567", "5/2/1979 12:00:00 AM");
    writer.WriteRow("Gary Wright", "gwright@domain.com", "555-5678", "2/18/1984 12:00:00 AM");
}
```

Note that the `CsvWriter.WriteRow()` method accepts any number of string parameters. It is also overloaded to accept a `string[]` or `IEnumerable<string>` argument. The method correctly handles column values that contain commas, quotes or even newlines.

The next example reads all rows from a CSV file.

```cs
// Read the data from disk
string[] columns = null;
using (CsvReader reader = new CsvReader(path))
{
    while (reader.ReadRow(ref columns))
        Console.WriteLine(string.Join(", ", columns));
}
```

The `CsvReader.ReadRow()` method returns `false` when the end of the file is reached. The `columns` parameter is passed by reference so it can be resized, if needed.

## CsvWriter &lt;T&gt; and CsvReader&lt;T&gt; Classes

These are higher level classes that will automatically map data between class properties and CSV columns. The following example defines a class, declares a collection with several instances of that class, then uses `CsvWriter<T>` to write the data to a CSV file, and `CsvReader<T>` to read it back again.

```cs
// This class will represent the data in the CSV file
class Person
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime Birthday { get; set; }
}

// Define some sample data
List<Person> People = new List<Person>
{
    new Person { Name = "Bill Smith", Email = "bsmith@domain.com", Phone = "555-1234", Birthday = new DateTime(1982, 10, 29) },
    new Person { Name = "Susan Carpenter", Email = "scarpenter@domain.com", Phone = "555-2345", Birthday = new DateTime(1995, 2, 17) },
    new Person { Name = "Jim Windsor", Email = "jwindsor@domain.com", Phone = "555-3456", Birthday = new DateTime(1989, 4, 23) },
    new Person { Name = "Jill Morrison", Email = "jmorrison@domain.com", Phone = "555-4567", Birthday = new DateTime(1979, 5, 2) },
    new Person { Name = "Gary Wright", Email = "gwright@domain.com", Phone = "555-5678", Birthday = new DateTime(1984, 2, 18) },
};

// Write the data to disk
// Note: Since all records are already in memory, you could replace the
// foreach loop with: writer.Write(People)
using (CsvWriter<Person> writer = new CsvWriter<Person>(path))
{
    writer.WriteHeaders();

    foreach (Person person in People)
        writer.Write(person);
}

// Read the data from disk
List<Person> people = new List<Person>();
using (CsvReader<Person> reader = new CsvReader<Person>(path))
{
    // Read header and use to determine column order
    reader.ReadHeaders(true);
    // Read data
    while (reader.Read(out Person person))
        people.Add(person);
}
```

It is important to note in the above example where the code that writes the data calls `CsvWriter<T>.WriteHeaders()`. This writes a row with the name of each column. (The library gets the column names from the properties of the `Person` class.) The code that reads the data calls `CsvReader<T>.ReadHeaders()` to read that header data. Because the argument to `CsvReader<T>.ReadHeaders()` is `true`, this tells the code to use the header data to determine how to map the columns. For example, it can determine the column order and also detect if one or more properties are not mapped to any column.

Correctly mapping the class properties to the CSV columns is critical for these classes to work correctly. Here, the code maps the class properties to columns based on the headers. The following sections will discuss other ways to map class properties to columns.

## ColumnMap Attribute

The `ColumnMapAttribute` can be applied to any class property or field to specify how that property or field is mapped to a CSV column. This attribute includes the following properties:

- **Name:**
  Specifies a column name, allowing the column name to be different from the class property name.

- **Index:**
  Specifies a property's 0-based column position. To ensure expected results, it is generally best to set the Index for all columns when setting this property.

- **Exclude:**
  Specifies that the class property should be excluded, and not written to or read from any column.

- **ConverterType:**
   Specifies the type of a custom class that converts the class property. The type specified will normally derive from `DataConverter<>`, which implements `IDataConverter`. If the type specified does not implement `IDataConverter`, an `ArgumentOutOfRangeException` is thrown. See the *Custom Data Converters* section below for more information about writing data converters.

The following example modifies the `Person` class shown earlier with `ColumnMap` attributes. The attributes are used to set the columns to be in the opposite order from how the class properties are declared, give the columns completely different names, and exclude the `Id` column.

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

With the `Person` class defined as shown above, the previous example will work correctly without the calls to `WriteHeaders()` and `ReadHeaders()`.

## MapColumns() Method

Class properties can also be mapped to columns using the `CsvWriter<T>.MapColumns()` and `CsvReader<T>.MapColumns()` methods. This is useful if you can't directly modify the class you are working with. This approach allows you to do anything you can do with a `ColumnMapAttribute` attribute.

The `MapColumns` method is demonstrated in the following section on *Custom Data Converters*.

## Custom Data Converters

Data converters convert class properties to strings, and then back again from strings to class properties. The CsvParser library includes converters for all basic data types (including `Guid` and `DateTime`), nullable basic data types, basic data type arrays and nullable basic data type arrays. But you can override the data converter used for any class property. For example, you might want to write your own data converter to support custom property types, or when you are working with data not formatted as expected by the built-in data converters. A good example of this are `DateTime` properties because there are so many ways to format date and time values.

The following example starts by defining the `DateTimeConverter` class to convert between `DateTime` values and strings. This class must implement the `IDataConverter` interface. The easiest way to do this in a type-safe manner is to derive your class from `DataConverter<T>`, where `T` is the type of the class property you are converting. The `DataConverter<T>` class has two abstract methods, `ConvertToString()` and `TryConvertFromString()`, which must be overridden in your derived class.

Next, the example defines the `PersonMaps` class to define the column mapping. This class must derive from `ColumnMaps<T>`, where `T` is the type of data class you are writing to or reading from CSV files. The constructor of this class must call `MapColumn()` for each class property that it maps. This method supports a fluent interface to set the various mapping properties for each class property. The meaning of these properties is described above in the *ColumnMap Attribute* section. In addition, it supports setting the `Converter` property, which specifies a custom data converter as described above (also see the example below).

Finally, the example calls the `CsvWriter.MapColumns<T>()` method to register the custom mappings. The code that reads the CSV file also calls the `CsvReader.MapColumns<T>()` method in the same way. Both must use the same mapping in order for the data to be interpreted correctly. The easiest way to do this is to pass the same class to both methods.

```cs
// Create a custom data converter for DateTime values
// Stores a date-only value (no time) in a compact format
class DateTimeConverter : DataConverter<DateTime>
{
    public override string ConvertToString(DateTime value)
    {
        int i = ((value.Day - 1) & 0x1f) |
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
class PersonMaps : ColumnMaps<Person>
{
    public PersonMaps()
    {
        // Note that only those properties set, and only those columns referenced
        // will be modified. All columns and settings not referenced here retain
        // their previous values.
        MapColumn(m => m.Id).Exclude(true);
        MapColumn(m => m.Name).Index(2).Name("nombre");
        MapColumn(m => m.Zip).Index(1).Name("código postal");
        MapColumn(m => m.Birthday).Index(0).Name("cumpleaños").Converter(new DateTimeConverter());
    }
}

// Write data to disk
using (CsvWriter<Person> writer = new CsvWriter<Person>(path))
{
    // Register our custom mapping
    writer.MapColumns<PersonMaps>();

    writer.WriteHeaders();
    foreach (Person person in People)
        writer.Write(person);
}

// Read data from disk
List<Person> people = new List<Person>();
using (CsvReader<Person> reader = new CsvReader<Person>(path))
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

Notice that the example above still calls `CsvWriter.WriteHeaders()` and `CsvReader.ReadHeaders()`. However, since the code has explicitly mapped all of the columns, this is just for the benefit of anyone viewing the file or maybe other software that must read it. It is completely unnecessary in the example above. Also notice that `false` is passed to `CsvReader.ReadHeaders()` because we do not want the library to use the headers to determine column order, etc. (If `true` is passed to `CsvReader.ReadHeaders()` here, it would override any existing `Index` and `Exclude` mapping properties.)

## CsvSettings Class

You can customize the way the library behaves by passing your own instance of the `CsvSettings` class to any of the reader or writer constructors, as demonstrated in the following example. This code uses the `CsvSettings` class to read a tab-separated-values (TSV) file. It sets the `ColumnDelimiter` property to a tab. It also sets it to use single quotes instead of double quotes (something not useful very often, but is fully supported).

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
