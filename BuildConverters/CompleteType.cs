// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System.Collections.Generic;

namespace BuildConverters
{
    public class CompleteType
    {
        public TypeInfo Type { get; }
        public TypeVariation Variation { get; }

        /// <summary>
        /// Type name (e.g. "NullableBooleanArray").
        /// </summary>
        public string TypeName => GetTypeName(Type, Variation);

        /// <summary>
        /// Class name (e.g. "NullableBooleanArrayConverter").
        /// </summary>
        public string ClassName => $"{TypeName}Converter";

        /// <summary>
        /// Base type name (e.g. "Boolean")
        /// </summary>
        public string BaseTypeName => Type.Name;

        /// <summary>
        /// Base type C# name (e.g. "bool")
        /// </summary>
        public string BaseTypeCName => Type.CName;

        /// <summary>
        /// Full type name (e.g. "Boolean?[]").
        /// </summary>
        public string FullTypeName => GetFullTypeName(Type, Variation);

        /// <summary>
        /// Full type C# name (e.g. "bool?[]").
        /// </summary>
        public string FullTypeCName => GetFullTypeCName(Type, Variation);

        /// <summary>
        /// 
        /// </summary>
        public string SampleData => Type.GetSampleData(Variation);

        public CompleteType(TypeInfo type, TypeVariation variation)
        {
            Type = type;
            Variation = variation;
        }

        #region Type Name Lookups

        static Dictionary<TypeVariation, string> TypeNameLookup = new Dictionary<TypeVariation, string>
        {
            [TypeVariation.Standard] = "{@}",
            [TypeVariation.Array] = "{@}Array",
            [TypeVariation.Nullable] = "Nullable{@}",
            [TypeVariation.NullableArray] = "Nullable{@}Array",
        };

        public static string GetTypeName(TypeInfo type, TypeVariation mode) => TypeNameLookup[mode].Replace("{@}", type.Name);

        static Dictionary<TypeVariation, string> FullTypeNameLookup = new Dictionary<TypeVariation, string>
        {
            [TypeVariation.Standard] = "{@}",
            [TypeVariation.Array] = "{@}[]",
            [TypeVariation.Nullable] = "{@}?",
            [TypeVariation.NullableArray] = "{@}?[]",
        };

        public static string GetFullTypeName(TypeInfo type, TypeVariation mode) => FullTypeNameLookup[mode].Replace("{@}", type.Name);

        public static string GetFullTypeCName(TypeInfo type, TypeVariation mode) => FullTypeNameLookup[mode].Replace("{@}", type.CName);

        #endregion

    }
}
