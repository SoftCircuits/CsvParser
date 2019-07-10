# CSV Parser

[![NuGet version (SoftCircuits.CsvParser)](https://img.shields.io/nuget/v/SoftCircuits.CsvParser.svg?style=flat-square)](https://www.nuget.org/packages/SoftCircuits.CsvParser/)

```
Install-Package SoftCircuits.CsvParser
```

## Overview

CsvParser is a class library to aid in working with comma-separated-values (CSV) files. The `CsvWriter` class can be used to write a CSV file. The `CsvReader` class can be used to read a CSV file.

These classes have support for column values that contain special characters, such as commas. Such values are wrapped in quotes. And the classes even support column values that span multiple lines.

An instance of the `CsvSettings` can optionally be passed to the constructors of the other classes, allowing you to override default settings such as which character is used to delimit columns, which character is used as a quote, and how empty lines are handled.

## Examples

This example creates and writes a few rows to a CSV file.

```cs
using (CsvWriter writer = new CsvWriter(path))
{
    // Note: WriteRow also accepts an string[] or IEnumerable<string>
    writer.WriteRow("First Name", "Last Name", "Address");
    writer.WriteRow("Jack", "Smith", "1215 Oak St");
    writer.WriteRow("Bryon", "Wilson", "18 Main St");
    writer.WriteRow("Janet", "Carpenter", "1922 W Sycamore Ln");
    writer.WriteRow("Bill", "Reynolds", "210 Redhill Ave");
}
```

This example reads all rows from a CSV file.

```cs
using (CsvReader reader = new CsvReader(path))
{
    string[] columns;
    while (reader.ReadRow(out columns))
        Console.WriteLine(string.Join(", ", columns));
}
```

The next example reads a tab-separated-values (TSV) file by creating a custom `CsvSettings` object, changing the `ColumnDelimiter` property to a tab, and passing the settings object to the `CsvReader`'s constructor.

```cs
CsvSettings settings = new CsvSettings();
settings.ColumnDelimiter = '\t';

using (CsvReader reader = new CsvReader(path, settings))
{
    string[] columns;
    while (reader.ReadRow(out columns))
        Console.WriteLine(string.Join(", ", columns));
}
```

## Additional Information

This code was derived from the article [Reading and Writing CSV Files in C#](http://www.blackbeltcoder.com/Articles/files/reading-and-writing-csv-files-in-c).
