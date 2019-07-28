# CSV Parser

[![NuGet version (SoftCircuits.CsvParser)](https://img.shields.io/nuget/v/SoftCircuits.CsvParser.svg?style=flat-square)](https://www.nuget.org/packages/SoftCircuits.CsvParser/)

```
Install-Package SoftCircuits.CsvParser
```

## Overview

CsvParser is a .NET library that makes it easy to work with comma-separated-values (CSV) files. It includes basic classes to read and write CSV columns and also higher level classes that will automatically map data between class members and columns. The library supports column values that contain embedded commas, quotes or other special characters. It even supports column values that span multiple lines.

In addition, a number of options can be modified. For example, you can change the column delimiter to be a character other than a comma.

you can optionally change settings such as the column delimiter character, the quote character or how blank lines are handled.

## Basic Classes

The following code writes several rows of data to a CSV file. Note that the `CsvWriter.WriteRow()` method is overloaded to also accept `string[]` and `IEnumerable<string>`.

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

This code was derived from the article [Reading and Writing CSV Files in C#](http://www.blackbeltcoder.com/Articles/files/reading-and-writing-csv-files-in-c).
