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
        /// Make <see cref="List{ColumnInfo}"></see> private.
        /// </summary>
        /// <param name="column"></param>
        private new void Add(ColumnInfo column) => base.Add(column);

        /// <summary>
        /// Builds a <see cref="ColumnInfoCollection"></see> for the specified type.
        /// </summary>
        public void BuildColumnInfoCollection<T>() where T : class, new()
        {
            // If we set an initial column index value (for columns not already
            // set), we seem to have more cases where the code just works than
            // if we don't do this. However, it's not clear if this is best in
            // all cases. For example, if caller has set the indexes for just
            // some of the columns, we are setting the remaining columns with
            // indexes that are not at all coordinated with those already set.
            // In the end, the caller should be explicit with all non-excluded
            // columns. But this logic may need to be revisited.
            //
            // Start our indexes much higher than anything the caller is
            // likely to use.
            int index = 1000;

            Clear();
            foreach (IMember member in GetPropertiesAndFields(typeof(T)))
            {
                ColumnInfo column = new ColumnInfo(member);
                if (column.Index == ColumnInfo.InvalidIndex)
                {
                    column.Index = index;
                    index++;
                }
                Add(column);
            }
        }

        /// <summary>
        /// Applies sorting and filtering information internally from column headers, and
        /// then returns a sorted and filtered list based on the new information. Overrides
        /// the mapping index for any column found in <paramref name="headers"/>, and
        /// overrides the mapping exclude flag for every column.
        /// </summary>
        /// <param name="headers">Column headers.</param>
        /// <param name="stringComparison">Comparison type used to compare header names
        /// against column names.</param>
        public ColumnInfo[] ApplyHeaders(string[] headers, StringComparison stringComparison)
        {
            if (headers == null)
                throw new ArgumentNullException(nameof(headers));

            foreach (ColumnInfo column in this)
            {
                int i = headers.IndexOf(h => h.Equals(column.Name, stringComparison));
                if (i >= 0)
                {
                    column.Index = i;
                    column.Exclude = false;
                }
                else column.Exclude = true;
            }
            return SortAndFilter();
        }

        /// <summary>
        /// Applies column mapping information internally and then returns a sorted and filtered list
        /// based on the new information. Overrides only the mapping properties explicitly set for
        /// only the columns explicitly specified.
        /// </summary>
        /// <param name="columnMaps">Mapping data.</param>
        public ColumnInfo[] ApplyColumnMaps(IEnumerable<ColumnMap> columnMaps)
        {
            if (columnMaps == null)
                throw new ArgumentNullException(nameof(columnMaps));

            // Validate mapping property references
            foreach (ColumnMap columnMap in columnMaps)
            {
                ColumnInfo column = this.FirstOrDefault(ci => ci.MemberName == columnMap.PropertyName_);
                if (column == null)
                    throw new InvalidOperationException($"Custom map for '{columnMap.PropertyName_}' references an unknown member.");

                if (!string.IsNullOrWhiteSpace(columnMap.Name_))
                    column.Name = columnMap.Name_.Trim();
                if (columnMap.Index_ != ColumnInfo.InvalidIndex)
                    column.Index = columnMap.Index_;
                if (columnMap.Exclude_.HasValue)
                    column.Exclude = columnMap.Exclude_.Value;

                if (columnMap.Converter_ != null)
                {
                    // Confirm converter handles the correct data type
                    if (columnMap.Converter_.GetDataType() != column.Member.Type)
                        throw new DataConverterTypeMismatchException(column.MemberName, column.Member.Type, columnMap.Converter_.GetDataType());
                    column.Converter = columnMap.Converter_;
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
        /// <param name="type">The type for which to return the members.</param>
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
