// Copyright (c) 2019-2020 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Text;

namespace SoftCircuits.CsvParser
{
    public class ColumnMap
    {
        internal string InternalMemberName { get; private set; }
        internal string InternalName { get; private set; }
        internal int InternalIndex { get; private set; }
        internal bool? InternalExclude { get; private set; }
        internal IDataConverter InternalConverter { get; private set; }

        internal ColumnMap(string memberName)
        {
            InternalMemberName = memberName ?? throw new ArgumentNullException(nameof(memberName));
            InternalName = null;
            InternalIndex = ColumnInfo.InvalidIndex;
            InternalExclude = null;
            InternalConverter = null;
        }

        /// <summary>
        /// Sets the column name that corresponds to this member.
        /// </summary>
        /// <param name="name">The column name that corresponds to this member.</param>
        /// <returns>This <see cref="ColumnMap"></see> to allow for fluent interface syntax.</returns>
        public ColumnMap Name(string name)
        {
            InternalName = name ?? throw new ArgumentNullException(nameof(name));
            return this;
        }

        /// <summary>
        /// Sets the 0-based column position for this member.
        /// </summary>
        /// <param name="index">The 0-based column position for this member.</param>
        /// <returns>This <see cref="ColumnMap"></see> to allow for fluent interface syntax.</returns>
        public ColumnMap Index(int index)
        {
            InternalIndex = index;
            return this;
        }

        /// <summary>
        /// If true, this member will not be mapped to any column.
        /// </summary>
        /// <param name="exclude">If true, this member will not be mapped to any column.</param>
        /// <returns>This <see cref="ColumnMap"></see> to allow for fluent interface syntax.</returns>
        public ColumnMap Exclude(bool exclude)
        {
            InternalExclude = exclude;
            return this;
        }

        /// <summary>
        /// Data converter class for converting this member to a string
        /// and back to a property value.
        /// </summary>
        /// <param name="converter">Specifies a data converter for this
        /// property. Must be for the same type as the member type.</param>
        /// <returns>This <see cref="ColumnMap"></see> to allow for fluent interface syntax.</returns>
        public ColumnMap Converter(IDataConverter converter)
        {
            InternalConverter = converter ?? throw new ArgumentNullException(nameof(converter));
            return this;
        }

        /// <summary>
        /// For debugging help.
        /// </summary>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(InternalMemberName ?? "(null)");
            builder.AppendFormat(", Index: {0}",
                (InternalIndex != ColumnInfo.InvalidIndex) ?
                InternalIndex.ToString() :
                "(null)");
            if (InternalExclude.HasValue && InternalExclude.Value)
                builder.Append(" (Exclude)");
            return builder.ToString();
        }
    }
}
