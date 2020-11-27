// Copyright (c) 2019-2020 Jonathan Wood (www.softcircuits.com)
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
        /// <param name="items">Initial array array data.</param>
        public GrowableArray(T[] items)
        {
            Items = items ?? new T[GrowBy];
            Count = 0;
        }

        /// <summary>
        /// Appends an item to the array.
        /// </summary>
        /// <param name="item">Item to add.</param>
        public void Append(T item)
        {
            if (Items.Length <= Count)
                Array.Resize(ref Items, Count + GrowBy);
            Debug.Assert(Items.Length > Count);
            Items[Count++] = item;
        }

        /// <summary>
        /// Returns the underlying array, with any unused elements trimmed.
        /// </summary>
        /// <param name="array">The <see cref="GrowableArray{T}"/> to convert.</param>
        public static implicit operator T[](GrowableArray<T> array)
        {
            Debug.Assert(array.Items.Length >= array.Count);
            if (array.Items.Length > array.Count)
                Array.Resize(ref array.Items, array.Count);
            Debug.Assert(array.Items.Length == array.Count);
            return array.Items;
        }
    }
}
