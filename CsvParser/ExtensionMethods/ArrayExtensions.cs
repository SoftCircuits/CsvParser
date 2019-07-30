// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace SoftCircuits.CsvParser
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Searches an array using a lambda expression predicate. Returns the index of
        /// the matching item, or -1 if no items match.
        /// </summary>
        /// <param name="predicate">Expression that returns <c>true</c> </param>
        /// <returns>The index of the matching item, or -1 if no items match.</returns>
        public static int IndexOf<T>(this T[] array, Func<T, bool> predicate)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (predicate(array[i]))
                    return i;
            }
            return -1;
        }
    }
}
