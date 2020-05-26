// Copyright (c) 2019-2020 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SoftCircuits.CsvParser
{
    /// <summary>
    /// Holds column descriptors for the columns in the given type.
    /// </summary>
    internal class ColumnInfoCollection<T> : List<ColumnInfo>
    {
        /// <summary>
        /// Initializes a new <see cref="ColumnInfoCollection{T}"></see> instance.
        /// </summary>
        public ColumnInfoCollection()
        {
            // Build the column collection for the specified type
            int index = 0;
            foreach (IMember member in GetMembers(typeof(T)))
            {
                ColumnInfo column = new ColumnInfo(member);

                // Set index
                if (column.Index != ColumnInfo.InvalidIndex)
                    index = column.Index;
                else
                    column.Index = index;
                if (!column.Exclude)
                    index++;
                Add(column);
            }
            _filteredColumns = null;
        }

        /// <summary>
        /// Applies column mapping information. Overrides only the mapping properties explicitly
        /// set for only the columns explicitly specified.
        /// </summary>
        /// <param name="columnMaps">Mapping data.</param>
        public void ApplyColumnMaps(IEnumerable<ColumnMap> columnMaps)
        {
            if (columnMaps == null)
                throw new ArgumentNullException(nameof(columnMaps));

            // Validate mapping property references
            foreach (ColumnMap columnMap in columnMaps)
            {
                int currIndex = FindIndex(ci => ci.MemberName == columnMap.InternalMemberName);
                if (currIndex < 0)
                    throw new InvalidOperationException($"Custom map for '{columnMap.InternalMemberName}' references an unknown member.");

                ColumnInfo column = this[currIndex];
                if (!string.IsNullOrWhiteSpace(columnMap.InternalName))
                    column.Name = columnMap.InternalName.Trim();
                if (columnMap.InternalIndex != ColumnInfo.InvalidIndex)
                    column.Index = columnMap.InternalIndex;
                if (columnMap.InternalExclude.HasValue)
                {
                    // Has setting changed?
                    if (column.Exclude != columnMap.InternalExclude.Value)
                    {
                        // Renumber non-explicit indexes
                        int index = column.Index;
                        if (!columnMap.InternalExclude.Value)
                            index++;
                        for (int i = currIndex + 1; i < Count; i++)
                        {
                            if (this[i].ExplicitIndex)
                                break;
                            this[i].Index = index;
                            if (!this[i].Exclude)
                                index++;
                        }
                    }
                    column.Exclude = columnMap.InternalExclude.Value;
                    column.ExplicitIndex = true;
                }
                if (columnMap.InternalConverter != null)
                {
                    // Confirm converter handles the correct data type
                    if (columnMap.InternalConverter.GetDataType() != column.Member.Type)
                        throw new DataConverterTypeMismatchException(column.MemberName, column.Member.Type, columnMap.InternalConverter.GetDataType());
                    column.Converter = columnMap.InternalConverter;
                }
            }
            _filteredColumns = null;
        }

        /// <summary>
        /// Applies sorting and filtering information internally from column headers. Overrides
        /// the index of any column found in <paramref name="headers"/>, and excludes those
        /// columns that do not appear in the headers. The exclude setting for columns found in
        /// the headers is not changed.
        /// </summary>
        /// <param name="headers">Column headers.</param>
        /// <param name="stringComparison">Comparison type used to compare header names
        /// against column names.</param>
        public void ApplyHeaders(string[] headers, StringComparison stringComparison)
        {
            if (headers == null)
                throw new ArgumentNullException(nameof(headers));

            // Override all Index values (whether explicit or not)
            for (int i = 0; i < Count; i++)
            {
                int j = Array.FindIndex(headers, h => h.Equals(this[i].Name, stringComparison));
                if (j >= 0)
                    this[i].Index = j;
                else
                    this[i].Exclude = true;
            }
            _filteredColumns = null;
        }

        /// <summary>
        /// Returns the <see cref="ColumnInfo"></see> for each column sorted by index and
        /// filtered by exclude flag.
        /// </summary>
        public IEnumerable<ColumnInfo> FilteredColumns
        {
            get
            {
                if (_filteredColumns == null)
                {
                    _filteredColumns = this
                        .Where(ci => ci.Exclude == false)
                        .OrderBy(ci => ci.Index);
                }
                Debug.Assert(_filteredColumns != null);
                return _filteredColumns;
            }
        }
        private IEnumerable<ColumnInfo> _filteredColumns = null;

        /// <summary>
        /// Returns all the properties and fields of a type.
        /// </summary>
        /// <param name="type">The type for which to return the members.</param>
        private IEnumerable<IMember> GetMembers(Type type)
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
