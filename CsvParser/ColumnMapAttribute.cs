// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace SoftCircuits.CsvParser
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ColumnMapAttribute : Attribute
    {
        /// <summary>
        /// Name override for this property.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 0-based column position for this property.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// If true, property will not be serialized to or from CSV files.
        /// </summary>
        public bool Exclude { get; set; }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public ColumnMapAttribute()
        {
            Name = null;
            Index = ColumnInfo.InvalidIndex;
            Exclude = false;
        }
    }
}
