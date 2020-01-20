﻿// Copyright (c) 2019-2020 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Text;

namespace SoftCircuits.CsvParser
{
    public class ColumnMap
    {
        // Properties named as such to prevent conflict with methods.
        internal string _PropertyName { get; private set; }
        internal string _Name { get; private set; }
        internal int _Index { get; private set; }
        internal bool? _Exclude { get; private set; }
        internal IDataConverter _Converter { get; private set; }

        internal ColumnMap(string propertyName)
        {
            _PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
            _Name = null;
            _Index = ColumnInfo.InvalidIndex;
            _Exclude = null;
            _Converter = null;
        }

        /// <summary>
        /// Sets the column name for this property.
        /// </summary>
        /// <param name="name"></param>
        public ColumnMap Name(string name)
        {
            _Name = name ?? throw new ArgumentNullException(nameof(name));
            return this;
        }

        /// <summary>
        /// Sets the 0-based column position for this property.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ColumnMap Index(int index)
        {
            _Index = index;
            return this;
        }

        /// <summary>
        /// If true, this property will not be mapped to any column.
        /// </summary>
        /// <param name="exclude"></param>
        /// <returns></returns>
        public ColumnMap Exclude(bool exclude)
        {
            _Exclude = exclude;
            return this;
        }

        /// <summary>
        /// Data converter class for converting this property to a string
        /// and back to a property value.
        /// </summary>
        /// <param name="converter">Specifies a data converter for this
        /// property. Must be for the same type as the property type.</param>
        public ColumnMap Converter(IDataConverter converter)
        {
            _Converter = converter ?? throw new ArgumentNullException(nameof(converter));
            return this;
        }

        /// <summary>
        /// For debugging help.
        /// </summary>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(_PropertyName ?? "(null)");
            builder.AppendFormat(", Index: {0}",
                (_Index != ColumnInfo.InvalidIndex) ?
                _Index.ToString() :
                "(null)");
            if (_Exclude.HasValue && _Exclude.Value)
                builder.Append(" (Exclude)");
            return builder.ToString();
        }
    }
}
