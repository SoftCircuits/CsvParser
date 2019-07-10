# CSV Parser

[![NuGet version (SoftCircuits.CsvParser)](https://img.shields.io/nuget/v/SoftCircuits.CsvParser.svg?style=flat-square)](https://www.nuget.org/packages/SoftCircuits.CsvParser/)

```
Install-Package SoftCircuits.CsvParser
```

## Overview

CsvParser makes it easy to work with comma-separated-values (CSV) files. It supports column values that contain commas, quotes or other special characters. It even supports column values that span multiple lines. In addition, you can optionally change settings such as the column delimiter character, the quote character or how blank lines are handled.

Use the `CsvWriter` class to write a CSV file. Use the `CsvReader` class to read a CSV file.

## Examples

This example creates and writes a few rows to a CSV file.

```cs
using (CsvWriter writer = new CsvWriter(path))
{
    // Note: WriteRow also accepts string[] or IEnumerable<string>
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
    string[] columns = null;
    while (reader.ReadRow(ref columns))
        Console.WriteLine(string.Join(", ", columns));
}
```

The next example uses the `CsvSettings` class to read a tab-separated-values (TSV) file. It sets the `ColumnDelimiter` property to a tab. It also sets it to use single quotes instead of double quotes, something you would likely never to but is supported).

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
