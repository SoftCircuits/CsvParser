// Copyright (c) 2019-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using SoftCircuits.CsvParser.Members;
using System;
using System.Diagnostics;
using System.Text;

namespace SoftCircuits.CsvParser
{
    internal class ColumnInfo
    {
        /// <summary>
        /// Represents the default, invalid column index value.
        /// When sorted, invalid indexes always come after valid ones.
        /// </summary>
        public const int InvalidIndex = int.MaxValue;

        /// <summary>
        /// The column name, which can be different from the property name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The original property name.
        /// </summary>
        public string MemberName => Member.Name;

        /// <summary>
        /// The index of this column. Works as a sort order rather than always
        /// indicating the true column index.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// If true, the <see cref="Index"/> property was set explicitly
        /// and shouldn't change unless changed explicitly.
        /// </summary>
        public bool ExplicitIndex { get; set; }

        /// <summary>
        /// If true, this column is not serialized.
        /// </summary>
        public bool Exclude { get; set; }

        /// <summary>
        /// Object that converts this property to a string, and back again.
        /// </summary>
        public IDataConverter? Converter { get; set; }

        /// <summary>
        /// Reflection data for this property.
        /// </summary>
        public IMember Member { get; set; }

        /// <summary>
        /// Initializes a ColumnInfo instance.
        /// </summary>
        /// <param name="member">Identifies the type member this column is associated
        /// with.</param>
        public ColumnInfo(IMember member)
        {
#if NETSTANDARD2_0
            if (member == null)
                throw new ArgumentNullException(nameof(member));
#else
            ArgumentNullException.ThrowIfNull(member);
#endif

            ColumnMapAttribute? attribute = member.ColumnMapAttribute;
            if (attribute != null)
            {
                // Initialize data from attribute
                Name = string.IsNullOrWhiteSpace(attribute.Name) ? member.Name : attribute.Name;
                Index = attribute.Index;
                ExplicitIndex = Index != InvalidIndex;
                Exclude = attribute.Exclude;
                if (attribute.ConverterType != null)
                {
                    // Verify converter type
                    if (!typeof(IDataConverter).IsAssignableFrom(attribute.ConverterType))
                        throw new ArgumentOutOfRangeException(nameof(member), "Converter type must derive from IDataConverter");
                    Converter = Activator.CreateInstance(attribute.ConverterType) as IDataConverter;
                    Debug.Assert(Converter != null);
                }
            }
            else
            {
                // Initialize data
                Name = member.Name;
                Index = InvalidIndex;
                ExplicitIndex = false;
                Exclude = false;
                Converter = null;
            }
            // Get standard converter if custom converter not supplied
            Converter ??= DataConverters.GetConverter(member.Type);
            Member = member;
        }

        /// <summary>
        /// Returns the value of this property on <paramref name="item"/> as a string.
        /// </summary>
        /// <param name="item">The object whose property value will be returned.</param>
        /// <returns>A string representation of <paramref name="item"/>.</returns>
        public string GetValue(object item)
        {
            Debug.Assert(Member != null);
            Debug.Assert(Converter != null);
            return Member.CanRead ?
                Converter.ConvertToString(Member.GetValue(item)) :
                string.Empty;
        }

        /// <summary>
        /// Sets the value of this property on <paramref name="item"/> from a
        /// string.
        /// </summary>
        /// <param name="item">The object whose property value will be set.</param>
        /// <param name="s">The new property value as a string.</param>
        /// <param name="invalidDataRaisesException">If true, raises an exception if
        /// the string is not in a supported format.</param>
        /// <returns>False if the string was not in the correct format.</returns>
        public void SetValue(object item, string s, bool invalidDataRaisesException)
        {
            Debug.Assert(Member != null);
            Debug.Assert(Converter != null);
            if (Member.CanWrite)
            {
                if (!Converter.TryConvertFromString(s, out object? value))
                {
                    // String could not be converted to property value. Throw exception
                    // if requested. Otherwise, just assign the default value returned
                    // from TryConvertFromString().
                    if (invalidDataRaisesException)
                        throw new BadDataFormatException(s, Member.Name, Member.Type);
                }
                Member.SetValue(item, value);
            }
        }

        /// <summary>
        /// For debugging.
        /// </summary>
        public override string ToString()
        {
            StringBuilder builder = new();

            builder.Append(Name);
            builder.AppendFormat(", Index: {0}", Index);
            if (Exclude)
                builder.Append(" (Exclude)");
            return builder.ToString();
        }
    }
}
