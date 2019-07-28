// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using SoftCircuits.CsvParser.Converters;
using System;

namespace SoftCircuits.CsvParser
{
    public class ColumnMap
    {
        // Properties named as such to prevent conflict with set methods.
        internal string _PropertyName { get; private set; }
        internal string _Name { get; private set; }
        internal int _Index { get; private set; }
        internal bool? _Exclude { get; private set; }
        internal ICustomConverter _Converter { get; private set; }

        internal ColumnMap(string propertyName)
        {
            _PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
            _Name = null;
            _Index = ColumnInfo.InvalidIndex;
            _Exclude = null;
            _Converter = null;
        }

        /// <summary>
        /// Name override for this property.
        /// </summary>
        /// <param name="name"></param>
        public ColumnMap Name(string name)
        {
            _Name = name ?? throw new ArgumentNullException(nameof(name));
            return this;
        }

        /// <summary>
        /// 0-based column position for this property.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ColumnMap Index(int index)
        {
            _Index = index;
            return this;
        }

        /// <summary>
        /// If true, this property will not be serialized to/from file.
        /// </summary>
        /// <param name="exclude"></param>
        /// <returns></returns>
        public ColumnMap Exclude(bool exclude)
        {
            _Exclude = exclude;
            return this;
        }

        /// <summary>
        /// Custom converter class for converting this property to a string
        /// and back to a property value.
        /// </summary>
        /// <param name="converter">Specifies a custom converter for this
        /// property. Must be for the same type as the property type.</param>
        public ColumnMap Converter(ICustomConverter converter)
        {
            _Converter = converter ?? throw new ArgumentNullException(nameof(converter));
            return this;
        }
    }
}
