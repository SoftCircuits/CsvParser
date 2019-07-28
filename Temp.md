# CSV Parser

[![NuGet version (SoftCircuits.CsvParser)](https://img.shields.io/nuget/v/SoftCircuits.CsvParser.svg?style=flat-square)](https://www.nuget.org/packages/SoftCircuits.CsvParser/)

```
Install-Package SoftCircuits.CsvParser
```

## Overview

CsvParser is a .NET library that makes it easy to work with comma-separated-values (CSV) files. It includes basic classes to read and write CSV columns and also higher level classes that will automatically map data between class members and columns. The library supports column values that contain embedded commas, quotes or other special characters. It even supports column values that span multiple lines.

In addition, there are a number of option settings. For example, you can change the column delimiter to another character. Additional options include changing the quote character and how blank lines are handled.

## CsvWriter and CsvReader Classes

These classes provide the simplest way to read and write CSV files. The example below writes several rows of data to a CSV file and then reads it back. Note that the `CsvWriter.WriteRow()` method is overloaded to also accept `string[]` and `IEnumerable<string>`.

```cs
using (CsvWriter writer = new CsvWriter(path))
{
    // Note that WriteRow() also accepts string[] and IEnumerable<string>
    writer.WriteRow("Id", "Name", "Zip", "Birthday");
    writer.WriteRow("1", "Bill Smith", "92869", "10/29/1972 12:00:00 AM");
    writer.WriteRow("2", "Susan Carpenter", "92865", "2/17/1985 12:00:00 AM");
    writer.WriteRow("3", "Jim Winsor", "92862", "4/23/1979 12:00:00 AM");
    writer.WriteRow("4", "Jill Morrison", "92861", "5/2/1969 12:00:00 AM");
    writer.WriteRow("5", "Gary Wright", "92868", "2/18/1974 12:00:00 AM");
}
```

This example reads all rows from a CSV file.

```cs
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
private class Person : IEquatable<Person>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Zip { get; set; }
    public DateTime Birthday { get; set; }
}

List<Person> People = new List<Person>
{
    new Person { Id = 1, Name = "Bill Smith", Zip = "92869", Birthday = new DateTime(1972, 10, 29) },
    new Person { Id = 2, Name = "Susan Carpenter", Zip = "92865", Birthday = new DateTime(1985, 2, 17) },
    new Person { Id = 3, Name = "Jim Winsor", Zip = "92862", Birthday = new DateTime(1979, 4, 23) },
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

It is important to note in the above example where the code that writes the data calls `CsvDataWriter.WriteHeaders()`. This writes a row with the name of each column. The code that reads the data calls `CsvDataReader.ReadHeaders()` to read that header data. Because the argument to `CsvDataReader.ReadHeaders()` is `true`, this tells the code to use the header data to figure out how to map the columns in the rest of the file. For example, maybe the columns are in a different order, or maybe one of the columns is excluded. Interpreting the column headers allows the code above correctly.



In addition




The next example uses the `CsvSettings` class to read a tab-separated-values (TSV) file. It sets the `ColumnDelimiter` property to a tab. It also sets it to use single quotes instead of double quotes (something you would likely never to but is fully supported).

```cs
CsvSettings settings = new CsvSettings();
settings.ColumnDelimiter = '\t';
settings.QuoteCharacter = '\'';

using (CsvReader reader = new CsvReader(path, settings))
{
    string[] columns = null;
    while (reader.ReadRow(ref columns))
        Console.WriteLine(string.Join(", ", columns));
}
```

## Additional Information

This code was originally derived from the article [Reading and Writing CSV Files in C#](http://www.blackbeltcoder.com/Articles/files/reading-and-writing-csv-files-in-c).