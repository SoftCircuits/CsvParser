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
    /// Specialized buffer for efficiently storing line data.
    /// </summary>
    /// <remarks>
    /// This class has an internal buffer, which can be used for storig line data. However, when the
    /// entire line exists within a single block, it can simply refer to part of an external array.
    /// This allows the line data to be returned by this class without actually copying any bytes.
    /// </remarks>
    internal class LineBuffer
    {
        public const int GrowExtra = 1024;

        // Internal buffer
        private char[] InternalBuffer;

        // Current buffer. References either InternalBuffer or a caller-supplied buffer
        private char[] Buffer;

        // Index offset into external buffer. Zero when using internal buffer
        private int IndexOffset;

        /// <summary>
        /// Gets the length of the line buffer.
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// Tracks the current position within the line
        /// </summary>
        public int Position { get; set; }

        public LineBuffer()
        {
            InternalBuffer = new char[GrowExtra];
            Reset();
        }

        /// <summary>
        /// Clears the buffer.
        /// </summary>
#if !NETSTANDARD2_0
        [MemberNotNull(nameof(Buffer))]
#endif
        public void Reset()
        {
            Buffer = InternalBuffer;
            IndexOffset = 0;
            Length = 0;
            Position = 0;
        }

        /// <summary>
        /// Appends a character to the line.
        /// </summary>
        public void Append(char c)
        {
            Debug.Assert(Buffer == InternalBuffer);

            if (InternalBuffer.Length < Length + 1)
            {
                Array.Resize(ref InternalBuffer, Length + 1 + GrowExtra);
                Buffer = InternalBuffer;
            }
            Buffer[Length++] = c;
        }

        /// <summary>
        /// Appends an array to the line.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(char[] array) => Append(array, 0, array.Length);

        /// <summary>
        /// Appends part of an array to the line.
        /// </summary>
        public void Append(char[] array, int startIndex, int length)
        {
            Debug.Assert(Buffer == InternalBuffer);

            if (length > 0)
            {
                Debug.Assert(Buffer.Length >= Length);
                if (Buffer.Length < Length + length)
                {
                    Array.Resize(ref InternalBuffer, Length + length + GrowExtra);
                    Buffer = InternalBuffer;
                }
                Debug.Assert(Buffer.Length >= Length + length);
                Array.Copy(array, startIndex, Buffer, Length, length);
                Length += length;
            }
        }

        /// <summary>
        /// Configures this <see cref="LineBuffer"/> to reference an external buffer.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetExternalBuffer(char[] array) => SetExternalBuffer(array, 0, array.Length);

        /// <summary>
        /// Configures this <see cref="LineBuffer"/> to reference an external buffer.
        /// </summary>
        public void SetExternalBuffer(char[] array, int startIndex, int length)
        {
            Debug.Assert(Length == 0);

            Buffer = array;
            IndexOffset = startIndex;
            Length = length;
        }

        /// <summary>
        /// Gets the character at the specified index.
        /// </summary>
        public char this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Buffer[IndexOffset + index];
        }

        /// <summary>
        /// Returns a string from the specified segment of this line.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string Substring(int startIndex, int length) => new(Buffer, IndexOffset + startIndex, length);

        /// <summary>
        /// Returns the index of the specified character or -1 if the character was not found.
        /// </summary>
        public int IndexOf(char c, int startIndex)
        {
            if (startIndex >= Length)
                return -1;
            int index = Array.IndexOf(Buffer, c, IndexOffset + startIndex, Length - startIndex);
            if (index >= 0)
                index -= IndexOffset;
            return index;
        }

        /// <summary>
        /// Copies all or part of the buffer to the specified array.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(int sourceIndex, char[] targetArray, int targetIndex, int length) =>
            Array.Copy(Buffer, IndexOffset + sourceIndex, targetArray, targetIndex, length);

        /// <summary>
        /// If this line references an external buffer, this method will copy the data
        /// to the internal buffer.
        /// </summary>
        public void InternalizeBuffer()
        {
            if (Buffer != InternalBuffer)
            {
                // Save old values
                char[] oldBuffer = Buffer;
                int oldLength = Length;
                int oldOffset = IndexOffset;

                // Set new values
                Buffer = InternalBuffer;
                Length = 0;
                IndexOffset = 0;

                // Copy data
                Append(oldBuffer, oldOffset, oldLength);
            }
        }

        //public override string ToString()
        //{
        //    return string.Format("{0} (Len={1}, Pos={2}, Offset={3}, ExternBuff={4})",
        //        new string(Buffer, IndexOffset + Position, Length - Position),
        //        Length,
        //        Position,
        //        IndexOffset,
        //        Buffer != InternalBuffer);
        //}
    }
}
