// Copyright (c) 2019-2023 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace CsvParser.Helpers
{
    /// <summary>
    /// Simplified string builder class. Provides slightly better performance that <see cref="StringBuilder"/>.
    /// </summary>
    internal class FastStringBuilder
    {
        private const int GrowExtra = 1024;

        private char[] Buffer;
        private int Length;

        public FastStringBuilder()
        {
            Buffer = Array.Empty<char>();
            Length = 0;
        }

        /// <summary>
        /// Clear the line buffer.
        /// </summary>
        public void Clear()
        {
            Length = 0;
        }

        public void Append(char c)
        {
            Debug.Assert(Buffer.Length >= Length);
            if (Buffer.Length < Length + 1)
                Array.Resize(ref Buffer, Length + 1 + GrowExtra);
            Debug.Assert(Buffer.Length >= Length + 1);
            Buffer[Length++] = c;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(string s) => Append(s, 0, s.Length);

        public void Append(string s, int startIndex, int length)
        {
            Debug.Assert(Buffer.Length >= Length);
            if (length > 0)
            {
                if (Buffer.Length < Length + length)
                    Array.Resize(ref Buffer, Length + length + GrowExtra);
                Debug.Assert(Buffer.Length >= Length + length);
                s.CopyTo(startIndex, Buffer, Length, length);
                Length += length;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(LineBuffer line) => Append(line, 0, line.Length);

        public void Append(LineBuffer line, int startIndex, int length)
        {
            Debug.Assert(Buffer.Length >= Length);
            if (length > 0)
            {
                if (Buffer.Length < Length + length)
                    Array.Resize(ref Buffer, Length + length + GrowExtra);
                Debug.Assert(Buffer.Length >= Length + length);
                line.CopyTo(startIndex, Buffer, Length, length);
                Length += length;
            }
        }

        public override string ToString() => new(Buffer, 0, Length);
    }
}
