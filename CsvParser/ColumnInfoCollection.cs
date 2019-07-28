// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SoftCircuits.CsvParser
{
    /// <summary>
    /// Holds data for all the columns for a given type.
    /// </summary>
    internal class ColumnInfoCollection : List<ColumnInfo>
    {
        /// <summary>
        /// Initializes a new ColumnInfoCollection instance.
        /// </summary>
        public ColumnInfoCollection()
        {
        }

        /// <summary>
        /// Make List&lt;ColumnInfo&gt;.Add() private.
        /// </summary>
        /// <param name="column"></param>
        private new void Add(ColumnInfo column) => base.Add(column);

        /// <summary>
        /// Builds a <see cref="ColumnInfoCollection"></see> for the specified type.
        /// </summary>
        public void BuildColumnInfoCollection<T>() where T : class, new()
        {
            int index = 0;

            Clear();
            foreach (IMember member in GetPropertiesAndFields(typeof(T)))
            {
                ColumnInfo column = new ColumnInfo(member);
                // If we set initial column index values (for columns not already
                // set), we seem to have a few more cases where the code just works
                // than if we don't do this. However, it's not clear if this is
                // best in all cases. For example, if caller has set the indexes
                // for just some of the columns, we are setting the remaining
                // columns with indexes that are not at all coordinated with those
                // already set. In the end, the caller should be explicit with all
                // non-excluded columns. But this logic may need to be revisited.
                if (column.Index == ColumnInfo.InvalidIndex)
                    column.Index = index;
                index++;
                Add(column);
            }
        }

        /// <summary>
        /// Applies sorting and filtering information internally from column headers, and
        /// then returns a sorted and filtered list based on the new information.
        /// </summary>
        /// <param name="headers">Column headers.</param>
        /// <param name="stringComparison">Comparison type used to compare header names
        /// with column names.</param>
        public ColumnInfo[] ApplyHeaders(string[] headers, StringComparison stringComparison)
        {
            if (headers == null)
                throw new ArgumentNullException(nameof(headers));

            for (int i = 0; i < headers.Length; i++)
            {
                string header = headers[i].Trim();
                ColumnInfo column = this.FirstOrDefault(ci => ci.Name.Equals(header, StringComparison.CurrentCultureIgnoreCase));
                if (column != null)
                    column.Index = i;
            }
            return SortAndFilter();
        }

        /// <summary>
        /// Applies column mapping information internally and then returns a sorted and filtered list
        /// based on the new information.
        /// </summary>
        /// <param name="columnMaps">Mapping data.</param>
        public ColumnInfo[] ApplyMapping(IEnumerable<ColumnMap> columnMaps)
        {
            if (columnMaps == null)
                throw new ArgumentNullException(nameof(columnMaps));

            // Validate mapping property references
            foreach (ColumnMap columnMap in columnMaps)
            {
                ColumnInfo column = this.FirstOrDefault(ci => ci.MemberName == columnMap._PropertyName);
                if (column == null)
                    throw new InvalidOperationException($"Custom map for '{columnMap._PropertyName}' references an unknown member.");

                if (!string.IsNullOrWhiteSpace(columnMap._Name))
                    column.Name = columnMap._Name.Trim();
                if (columnMap._Index != ColumnInfo.InvalidIndex)
                    column.Index = columnMap._Index;
                if (columnMap._Exclude.HasValue)
                    column.Exclude = columnMap._Exclude.Value;

                if (columnMap._Converter != null)
                {
                    // NOTE: In the case where we are given an object that inherits from
                    // CustomConverter<T>, it would be nice to confirm that the type 'T'
                    // matches the property type.
                    column.Converter = columnMap._Converter;
                }
            }
            return SortAndFilter();
        }

        /// <summary>
        /// Returns a sorted and filtered shallow copy of the internal list. Results can be empty.
        /// </summary>
        public ColumnInfo[] SortAndFilter()
        {
            return (from ci in this
                    where ci.Exclude == false && ci.Index != ColumnInfo.InvalidIndex
                    orderby ci.Index
                    select ci).ToArray();
        }

        /// <summary>
        /// Returns all the properties and fields of a type.
        /// </summary>
        /// <param name="type">The type from which to return the members.</param>
        private IEnumerable<IMember> GetPropertiesAndFields(Type type)
        {
            foreach (MemberInfo member in type.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (member is PropertyInfo property)
                {
                    yield return new PropertyMember(property);
                }
                else if (member is FieldInfo field)
                {
                    // Ignore compiler-generated fields for backing properties
                    if (!field.IsDefined(typeof(CompilerGeneratedAttribute), false))
                        yield return new FieldMember(field);
                }
            }
        }
    }
}
