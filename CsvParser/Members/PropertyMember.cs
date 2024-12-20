﻿// Copyright (c) 2019-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Reflection;

namespace SoftCircuits.CsvParser.Members
{
    /// <summary>
    /// Wrapper for PropertyInfo (member with getter and/or setter)
    /// </summary>
    internal class PropertyMember(PropertyInfo property) : IMember
    {
        private readonly PropertyInfo Property = property ?? throw new ArgumentNullException(nameof(property));

        public Type Type => Property.PropertyType;
        public string Name => Property.Name;
        public bool CanRead => Property.CanRead;
        public bool CanWrite => Property.CanWrite;
        public ColumnMapAttribute? ColumnMapAttribute => Property.GetCustomAttribute<ColumnMapAttribute>();
        public object? GetValue(object? item) => Property.GetValue(item);
        public void SetValue(object? item, object? value) => Property.SetValue(item, value);
    }
}
