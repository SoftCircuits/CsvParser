// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using SoftCircuits.CsvParser.Converters;
using System;

namespace SoftCircuits.CsvParser
{
    public class ColumnMap
    {
        // Properties named as such to prevent conflict with methods.
        internal string PropertyName_ { get; private set; }
        internal string Name_ { get; private set; }
        internal int Index_ { get; private set; }
        internal bool? Exclude_ { get; private set; }
        internal IDataConverter Converter_ { get; private set; }

        internal ColumnMap(string propertyName)
        {
            PropertyName_ = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
            Name_ = null;
            Index_ = ColumnInfo.InvalidIndex;
            Exclude_ = null;
            Converter_ = null;
        }

        /// <summary>
        /// Sets the column name for this property.
        /// </summary>
        /// <param name="name"></param>
        public ColumnMap Name(string name)
        {
            Name_ = name ?? throw new ArgumentNullException(nameof(name));
            return this;
        }

        /// <summary>
        /// Sets the 0-based column position for this property.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ColumnMap Index(int index)
        {
            Index_ = index;
            return this;
        }

        /// <summary>
        /// If true, this property will not be mapped to any column.
        /// </summary>
        /// <param name="exclude"></param>
        /// <returns></returns>
        public ColumnMap Exclude(bool exclude)
        {
            Exclude_ = exclude;
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
            Converter_ = converter ?? throw new ArgumentNullException(nameof(converter));
            return this;
        }
    }
}
