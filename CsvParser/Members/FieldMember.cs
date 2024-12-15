// Copyright (c) 2019-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Reflection;

namespace SoftCircuits.CsvParser.Members
{
    /// <summary>
    /// Wrapper for FieldInfo (no getter or setter).
    /// </summary>
    internal class FieldMember(FieldInfo field) : IMember
    {
        private readonly FieldInfo Field = field ?? throw new ArgumentNullException(nameof(field));

        public Type Type => Field.FieldType;
        public string Name => Field.Name;
        public bool CanRead => true;
        public bool CanWrite => true;
        public ColumnMapAttribute? ColumnMapAttribute => Field.GetCustomAttribute<ColumnMapAttribute>();
        public object? GetValue(object? item) => Field.GetValue(item);
        public void SetValue(object? item, object? value) => Field.SetValue(item, value);
    }
}
