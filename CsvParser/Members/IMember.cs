// Copyright (c) 2019-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace SoftCircuits.CsvParser.Members
{
    /// <summary>
    /// Interface for property and field members.
    /// </summary>
    internal interface IMember
    {
        Type Type { get; }
        string Name { get; }
        bool CanRead { get; }
        bool CanWrite { get; }
        ColumnMapAttribute? ColumnMapAttribute { get; }
        object? GetValue(object? item);
        void SetValue(object? item, object? value);
    }
}
