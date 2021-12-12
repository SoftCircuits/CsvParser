// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Diagnostics;

namespace SoftCircuits.CsvParser
{
    /// <summary>
    /// Because the number of columns in a CSV file tends to be the same on each line,
    /// this helper class attempts to reuse an existing array with a minimum number
    /// of resizing or new allocations. If the number of items added to the array
    /// equals the initial size of the array, no resizing or allocations occur.
    /// </summary>
    /// <typeparam name="T">The array type.</typeparam>
    internal class GrowableArray<T>
    {
        private const int GrowBy = 10;

        private T[] Items;
        private int Count;

        /// <summary>
        /// Initializes a new <see cref="GrowableArray{T}"/> instance.
        /// </summary>
        public GrowableArray()
        {
            Items = new T[GrowBy];
            Count = 0;
        }

        /// <summary>
        /// Initializes a new <see cref="GrowableArray{T}"/> instance.
        /// </summary>
        /// <param name="items">Initial array array data.</param>
        public GrowableArray(T[] items)
        {
            Items = items ?? throw new ArgumentNullException(nameof(items));
            Count = 0;
        }

        /// <summary>
        /// Resets the number of items in this array without changing the
        /// current array's capacity.
        /// </summary>
        public void Clear()
        {
            Count = 0;
        }

        /// <summary>
        /// Appends an item to the array.
        /// </summary>
        /// <param name="item">Item to add.</param>
        public void Append(T item)
        {
            Debug.Assert(Items.Length >= Count);
            if (Items.Length <= Count)
                Array.Resize(ref Items, Count + GrowBy);

            Debug.Assert(Items.Length > Count);
            Items[Count++] = item;
        }

        /// <summary>
        /// Reduces the capacity to match the number of items it contains.
        /// </summary>
        public void Compact()
        {
            Debug.Assert(Items.Length >= Count);

            if (Items.Length > Count)
                Array.Resize(ref Items, Count);
        }

        /// <summary>
        /// Returns the underlying array, with any unused elements trimmed.
        /// </summary>
        /// <param name="array">The <see cref="GrowableArray{T}"/> to convert.</param>
        public static implicit operator T[](GrowableArray<T> array)
        {
            array.Compact();
            return array.Items;
        }
    }
}
