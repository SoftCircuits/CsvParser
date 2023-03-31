// Copyright (c) 2019-2023 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CsvParser.Helpers
{
    internal class CharBuffer
    {
        internal const int DefaultBufferSize = 4096;
        internal const int MinBufferSize = 128;

        // Interal character buffer
        private readonly char[] Buffer;

        // Gets the current length of the data in this buffer.
        public int Length { get; private set; }

        // Get or sets the current position within this buffer.
        public int Position { get; set; }

        public CharBuffer(int bufferSize = DefaultBufferSize)
        {
            Buffer = new char[Math.Max(bufferSize, MinBufferSize)];
            Length = 0;
            Position = 0;
        }

        /// <summary>
        /// Loads another block of data from <paramref name="reader"/>.
        /// </summary>
        /// <param name="line">If the specified line references a <see cref="CharBuffer"/>'s buffer, data from that buffer
        /// will be copied to the <see cref="LineBuffer"/>'s internal buffer. This prevents overwriting data referenced in
        /// this <see cref="CharBuffer"/>.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Read(StreamReader reader, LineBuffer line)
        {
            line.InternalizeBuffer();
            return Read(reader);
        }

        /// <summary>
        /// Loads another block of data from <paramref name="reader"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Read(StreamReader reader)
        {
#if NETSTANDARD2_0
            Length = reader.Read(Buffer, 0, Buffer.Length);
#else
            Length = reader.Read(new Span<char>(Buffer, 0, Buffer.Length));
#endif
            Position = 0;
            return Length > 0;
        }

        /// <summary>
        /// Asynchronously loads another block of data from <paramref name="reader"/>.
        /// </summary>
        /// <param name="line">If the specified line references a <see cref="CharBuffer"/>'s buffer, data from that buffer
        /// will be copied to the <see cref="LineBuffer"/>'s internnal buffer. This prevents overwriting data referenced in
        /// this <see cref="CharBuffer"/>.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<bool> ReadAsync(StreamReader reader, LineBuffer line)
        {
            line.InternalizeBuffer();
            return await ReadAsync(reader);
        }

        /// <summary>
        /// Asynchronously loads another block of data from <paramref name="reader"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<bool> ReadAsync(StreamReader reader)
        {
            Length = await reader.ReadAsync(Buffer, 0, Buffer.Length);
            Position = 0;
            return Length > 0;
        }

        /// <summary>
        /// Returns the index of the next newline character.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int IndexOfNewLine(int startIndex = 0)
        {

#if NETSTANDARD2_0

            // Slow version for .NET Standard
            for (int i = startIndex; i < Length; i++)
            {
                if (Buffer[i] == '\r' || Buffer[i] == '\n')
                    return i;
            }
            return -1;

#else

            int i = Buffer.AsSpan(startIndex, Length - startIndex).IndexOfAny('\r', '\n');
            if (i >= 0)
                i += startIndex;
            return i;

#endif

        }

        public char this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Buffer [index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator char[](CharBuffer buffer) => buffer.Buffer;

        //public override string ToString()
        //{
        //    const int MaxStringLen = 120;

        //    string s = new(Buffer, Position, Math.Min(Length, MaxStringLen));
        //    if (Length > MaxStringLen)
        //        s = string.Concat(s, "...");
        //    return $"\"{s}\" (Len={Length}, Pos={Position})";
        //}
    }
}
