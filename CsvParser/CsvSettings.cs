// Copyright (c) 2019-2023 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using CsvParser.Helpers;
using System;

namespace SoftCircuits.CsvParser
{
    /// <summary>
    /// Settings class for CSV reader and writer classes.
    /// </summary>
    public class CsvSettings
    {

        #region Special Characters

        /// <summary>
        /// These are special characters in CSV files. If a column contains any
        /// of these characters, the entire column is wrapped in quotes.
        /// </summary>
        private readonly char[] SpecialCharacters;

        // Indexes into SpecialCharacters
        private const int ColumnDelimiterIndex = 0;
        private const int QuoteCharacterIndex = 1;

        /// <summary>
        /// Gets or sets the character used to delimit columns. Default value is a comma
        /// (,).
        /// </summary>
        public char ColumnDelimiter
        {
            get => SpecialCharacters[ColumnDelimiterIndex];
            set => SpecialCharacters[ColumnDelimiterIndex] = value;
        }

        /// <summary>
        /// Gets or sets the character used to quote values that contain special characters.
        /// Default value is the double quote character (&quot;).
        /// </summary>
        public char QuoteCharacter
        {
            get => SpecialCharacters[QuoteCharacterIndex];
            set => SpecialCharacters[QuoteCharacterIndex] = value;
        }

        /// <summary>
        /// Returns <c>true</c> is the given string contains any special characters.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        internal bool HasSpecialCharacter(string s) => s.IndexOfAny(SpecialCharacters) >= 0;

        #endregion

        /// <summary>
        /// Specifies how blank lines are interpreted by CsvReader. Default value is
        /// <see cref="EmptyLineBehavior.NoColumns"></see>.
        /// </summary>
        public EmptyLineBehavior EmptyLineBehavior;

        /// <summary>
        /// If true, <see cref="CsvReader{T}"></see> will raise a
        /// <see cref="BadDataFormatException"></see> exception when it encounters
        /// data that cannot be converted to the corresponding data type. Default
        /// value is <c>true</c>.
        /// </summary>
        public bool InvalidDataRaisesException { get; set; }

        /// <summary>
        /// Specifies the string comparison type used by
        /// <see cref="CsvReader.ReadHeaders(bool)"></see> to compare column
        /// names against column headers when it's passed a <c>true</c> argument.
        /// Default value is <see cref="StringComparison.InvariantCultureIgnoreCase"></see>.
        /// </summary>
        public StringComparison ColumnHeaderStringComparison { get; set; }

        /// <summary>
        /// Gets or sets the size of the internal read buffer used by <see cref="CsvReader"/>. Default value is 4096.
        /// </summary>
        public int BufferSize { get; set; }

        /// <summary>
        /// Initializes a new <c>CsvSettings</c> instance.
        /// </summary>
        public CsvSettings()
        {
            SpecialCharacters = new char[]
            {
                ',',    // ColumnDelimiter
                '"',    // QuoteCharacter
                '\r',   // New line characters
                '\n'
            };

            EmptyLineBehavior = EmptyLineBehavior.NoColumns;
            InvalidDataRaisesException = true;
            ColumnHeaderStringComparison = StringComparison.InvariantCultureIgnoreCase;
            BufferSize = CharBuffer.DefaultBufferSize;
        }
    }
}
