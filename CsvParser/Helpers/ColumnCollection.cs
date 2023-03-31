// Copyright (c) 2019-2023 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace CsvParser.Helpers
{
    /// <summary>
    /// This helper class attempts to reuse an existing array with minimum resizing. It
    /// optimizes for the case where all records in a file have the same number of columns.
    /// </summary>
    internal class ColumnCollection
    {
        private const int GrowExtra = 10;

        private string[] Items;

        public int Length { get; private set; }

        /// <summary>
        /// Initializes a new <see cref="ColumnCollection"/> instance.
        /// </summary>
        public ColumnCollection(string[]? items = null)
        {
            Reset(items);
        }

#if !NETSTANDARD2_0
        [MemberNotNull(nameof(Items))]
#endif
        public void Reset(string[]? items = null)
        {
            if (items != null)
                Items = items;
            else
                Items ??= new string[GrowExtra];
            Length = 0;
        }

        /// <summary>
        /// Resets the number of items in this array without changing the
        /// current array's capacity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            Length = 0;
        }

        /// <summary>
        /// Appends an item to the array.
        /// </summary>
        /// <param name="item">Item to add.</param>
        public void Append(string item)
        {
            Debug.Assert(Items.Length >= Length);
            if (Items.Length < Length + 1)
                Array.Resize(ref Items, Length + 1 + GrowExtra);
            Debug.Assert(Items.Length >= Length + 1);
            Items[Length++] = item;
        }

        /// <summary>
        /// Resizes the array, if needed, to match the number of used elements.
        /// Returs a reference to the resized array.
        /// </summary>
        public string[] TrimUnused()
        {
            Debug.Assert(Items.Length >= Length);
            if (Items.Length > Length)
                Array.Resize(ref Items, Length);
            return Items;
        }

        public static implicit operator string[](ColumnCollection columns) => columns.Items;

        //public override string ToString() => string.Join(", ", Items.Take(Length));
    }
}
