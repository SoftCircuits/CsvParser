// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
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
        private MemoryStream? Stream;

        /// <summary>
        /// Initializes a MemoryFile instance.
        /// </summary>
        public MemoryFile()
        {
            Stream = null;
        }

        /// <summary>
        /// Initializes a MemoryFile with initial content.
        /// </summary>
        /// <param name="content"></param>
        public MemoryFile(string content)
        {
            Stream = new MemoryStream();
            Stream.Write(Encoding.UTF8.GetBytes(content));
            Stream.Seek(0, SeekOrigin.Begin);
        }

        private MemoryStream GetStream()
        {
            MemoryStream? oldStream = Stream;

            Stream = new MemoryStream();
            if (oldStream != null)
            {
                Stream.Write(oldStream.ToArray());
                Stream.Seek(0, SeekOrigin.Begin);
            }
            return Stream;
        }

        public void Reset()
        {
            Stream = null;
        }

        public override string ToString()
        {
            return (Stream != null) ?
                Encoding.UTF8.GetString(Stream.ToArray()) :
                string.Empty;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            if (Stream != null)
            {
                Stream.Dispose();
                Stream = null;
            }
        }

        public static implicit operator Stream(MemoryFile f) => f.GetStream();
    }
}
