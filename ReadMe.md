<a href="https://www.buymeacoffee.com/jonathanwood" target="_blank"><img src="https://www.buymeacoffee.com/assets/img/custom_images/black_img.png" alt="Buy Me A Coffee" style="height: 37px !important;width: 170px !important;" ></a>

# CSV Parser

[![NuGet version (SoftCircuits.CsvParser)](https://img.shields.io/nuget/v/SoftCircuits.CsvParser.svg?style=flat-square)](https://www.nuget.org/packages/SoftCircuits.CsvParser/)

```
Install-Package SoftCircuits.CsvParser
```

## Overview

CsvParser is a .NET library that makes it easy to work with comma-separated-values (CSV) files. (Since you can customize the delimiter, it can be used to work with files with any delimiter). CsvParser includes basic classes to read and write CSV data, and also higher-level classes that automatically map class properties to CSV columns. The library correctly handles column values that contain embedded commas, quotes or other special characters. It even supports column values that span multiple lines. CsvParser is very efficient and is designed to handle large data files without loading everything into memory. This library runs up to four times faster than the popular CsvHelper library.

## CsvWriter and CsvReader Classes

These classes provide the simplest way to read and write CSV files. The example below writes several rows of data to a CSV file.

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

Note that the `CsvWriter.WriteRow()` method accepts any number of string parameters. It is also overloaded to handle a `string[]` or `IEnumerable<string>` argument.

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

The `CsvReader.ReadRow()` method returns `false` when the end of the file has been reached and no more rows can be read. The `columns` parameter is passed by reference so it can be resized, if needed.

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

If you can be sure the CSV file being read was created using the code above, the argument to `CsvReader<T>.ReadHeaders()` could be false because you could be confident that the columns would be in the order expected, etc. But if someone else is supplying the CSV file, setting the `CsvReader<T>.ReadHeaders()` argument to `true` would allow it to work if the supplier put the columns in a different order.

Correctly mapping the class properties to the CSV columns is critical for these classes to work correctly. Here, the code maps the class properties to columns based on the column headers. The following sections will discuss other ways to map class properties to columns.

## ColumnMap Attribute

The `ColumnMapAttribute` can be applied to any class property or field to specify how that property or field is mapped to a CSV column. This attribute accepts any of the following arguments:

- **Name:**
  Specifies a column name, allowing the column name to be different from the class property name.

- **Index:**
  Specifies a property's 0-based column position. To ensure expected results, it is generally best to set the column Index for all properties when setting this property.

- **Exclude:**
  Specifies whether or not the class property should be excluded, and not written to or read from any column.

- **ConverterType:**
   Data converters convert individual class properties to strings and back again from strings to class properties. The CsvParser library includes converters for all basic data types (including `Guid` and `DateTime`), basic nullable data types, basic data type arrays and nullable nullable data type arrays. But you can override the data converter used for any class property. For example, you might want to write your own data converter to support custom property types, or when you are working with data not formatted as expected by the built-in data converters. A good example of this are `DateTime` properties because there are so many ways to format date and time values.

   To override a data converter, create a class that implements the `IDataConverter` interface. The easiest way to do this in a type-safe manner is to derive your class from `DataConverter<T>`, where `T` is the type of the property you are converting. The `DataConverter<T>` class has two abstract methods, `ConvertToString()` and `TryConvertFromString()`, which must be overridden in your derived class.
   
   Finally, set the `ConverterType` argument of the property's `ColumnMap` attribute to your custom convert class type. Note that if you set it to a type that does not  implements `IDataConverter`, an `ArgumentOutOfRangeException` is thrown at runtime.

The example below uses the `ColumnMap` attribute to customize the `Person` class. It sets the `Index` properties such that the CSV columns appear in the reverse order from how the properties are declared in the class, it excludes the `Phone` property, and it causes the `Birthday` header to use the name *DOB*. It also specifies a custom converter for the `Birthday` property that stores the date (no time) in a very compact format.

```cs
// Add column mapping attributes to our data class
class Person
{
    [ColumnMap(Index = 2)]
    public string Name { get; set; }

    [ColumnMap(Index = 1)]
    public string Email { get; set; }

    [ColumnMap(Exclude = true)]
    public string Phone { get; set; }

    [ColumnMap(Index = 0, Name = "DOB", ConverterType = typeof(DateTimeConverter))]
    public DateTime Birthday { get; set; }
}

// Create a custom data converter for DateTime values
// Stores a date-only value (no time) in a very compact format
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
    reader.ReadHeaders(false);
    // Read data
    while (reader.Read(out Person person))
        people.Add(person);
}
```

Note that the code above will work correctly without the calls to the `CsvWriter<T>.WriteHeaders()` and `CsvReader<T>.ReadHeaders()` methods. We still included them for the benefit of anyone looking at the contents of the CSV file. But they are optional here because we used the `ColumnMap` attribute to provide enough information about column order, etc. You can also see that we passed `false` to the `CsvReader<T>.ReadHeaders()` method. Again, this is because we have all the column information we need. However, if the file is being supplied by someone else, as described earlier, you might want to change this argument to `true`, so that it can handle unexpected cases such as where columns are in a different order or are omitted. (If `true` is passed to `CsvReader<T>.ReadHeaders()` here, it would override any existing `Index` and `Exclude` mapping properties.)

Also note that, in the collection read back from disk, the `Phone` property will always be `null` because we excluded that property and it was not written to the CSV file.

## MapColumns() Method

In some cases, you may want to set `ColumnMap` attributes for a class you cannot directly modify. For example, the class might be part of a library you are using and you don't have the source code. In these cases, you can use the `CsvWriter<T>.MapColumns()` and `CsvReader<T>.MapColumns()` methods.

The example below creates a custom class that derives from `ColumnMaps<T>`, where `T` is the type of class being written or read. The constructor of this class must call `MapColumn()` for each class property that it maps. This method supports a fluent interface to set the various mapping properties for each class property. 

The code that writes the CSV file calls the `CsvWriter<T>.MapColumns<T>()` method to register the custom mappings before any data is written. The code that reads the CSV file calls the `CsvReader<T>.MapColumns<T>()` method in the same way. Both must use the same mapping in order for the data to be interpreted correctly. The easiest way to do this is to pass the same class to both methods.

```cs
// Create our custom mapping class
class PersonMaps : ColumnMaps<Person>
{
    public PersonMaps()
    {
        // Note that only those properties set, and only those columns referenced
        // will be modified. All columns and settings not referenced here retain
        // their previous values.
        MapColumn(m => m.Name).Index(2);
        MapColumn(m => m.Email).Index(1);
        MapColumn(m => m.Phone).Exclude(true);
        MapColumn(m => m.Birthday).Index(0).Name("DOB").Converter(new DateTimeConverter());
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

This example does exactly the same thing as the previous example but without modifying the `Person` class.

## CsvSettings Class

You can customize the way the library behaves by passing your own instance of the `CsvSettings` class to any of the reader or writer constructors, as demonstrated in the following example. This code uses the `CsvSettings` class to read a tab-separated-values (TSV) file. It sets the `ColumnDelimiter` property to a tab. It also sets it to use single quotes instead of double quotes (something not useful very often, but is fully supported).

```cs
// Set custom settings
CsvSettings settings = new CsvSettings
{
    ColumnDelimiter = '\t',
    QuoteCharacter = '\''
};

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
