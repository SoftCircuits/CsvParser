// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.IO;
using System.Text;

namespace CsvParserTests
{
    /// <summary>
    /// Provides a memory stream for reading and writing files. The first time a stream
    /// is requested (through the <see cref="Stream"/> operator) it returns a stream
    /// not tied to any buffer. The second time a stream is requested, it returns a
    /// stream tied to the buffer created by the first stream. It also overrides
    /// <see cref="ToString()"/> to see the bytes as text for debugging purposes.
    /// </summary>
    public class MemoryFile : IDisposable
    {
        private MemoryStream Stream;

        public MemoryFile()
        {
            Stream = null;
        }

        private MemoryStream GetStream()
        {
            if (Stream != null)
                Stream = new MemoryStream(Stream.ToArray());
            else
                Stream = new MemoryStream();
            return Stream;
        }

        public override string ToString()
        {
            if (Stream != null)
                return Encoding.UTF8.GetString(Stream.ToArray());
            return string.Empty;
        }

        public void Dispose()
        {
            if (Stream != null)
                Stream.Dispose();
        }

        public static implicit operator Stream(MemoryFile f) => f.GetStream();
    }
}
