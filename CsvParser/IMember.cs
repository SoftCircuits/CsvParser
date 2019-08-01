// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Reflection;

namespace SoftCircuits.CsvParser
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
        ColumnMapAttribute ColumnMapAttribute { get; }
        object GetValue(object item);
        void SetValue(object item, object value);
    }

    /// <summary>
    /// Wrapper for PropertyInfo (member with getter and/or setter)
    /// </summary>
    internal class PropertyMember : IMember
    {
        private readonly PropertyInfo Property;

        public PropertyMember(PropertyInfo property)
        {
            Property = property;
        }

        public Type Type => Property.PropertyType;
        public string Name => Property.Name;
        public bool CanRead => Property.CanRead;
        public bool CanWrite => Property.CanWrite;
        public ColumnMapAttribute ColumnMapAttribute => Property.GetCustomAttribute<ColumnMapAttribute>();
        public object GetValue(object item) => Property.GetValue(item);
        public void SetValue(object item, object value) => Property.SetValue(item, value);
    }

    /// <summary>
    /// Wrapper for FieldInfo (no getter or setter).
    /// </summary>
    internal class FieldMember : IMember
    {
        private readonly FieldInfo Field;

        public FieldMember(FieldInfo field)
        {
            Field = field;
        }

        public Type Type => Field.FieldType;
        public string Name => Field.Name;
        public bool CanRead => true;
        public bool CanWrite => true;
        public ColumnMapAttribute ColumnMapAttribute => Field.GetCustomAttribute<ColumnMapAttribute>();
        public object GetValue(object item) => Field.GetValue(item);
        public void SetValue(object item, object value) => Field.SetValue(item, value);
    }
}
