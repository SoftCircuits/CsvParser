// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Diagnostics.CodeAnalysis;

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
            set
            {
                SpecialCharacters[QuoteCharacterIndex] = value;
                InitializeQuoteStrings();
            }
        }

        /// <summary>
        /// Returns <c>true</c> is the given string contains any special characters.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        internal bool HasSpecialCharacter(string s) => s.IndexOfAny(SpecialCharacters) >= 0;

        /// <summary>
        /// Gets a string with the current quote character. Used for formatting columns.
        /// </summary>
        internal string OneQuoteString { get; private set; }

        /// <summary>
        /// Gets a string with two quote characters. Used for formatting columns.
        /// </summary>
        internal string TwoQuoteString { get; private set; }

        /// <summary>
        /// Initializes <see cref="OneQuoteString"/> and <see cref="TwoQuoteString"/>
        /// based on the value of <see cref="QuoteCharacter"/>.
        /// </summary>
#if !NETSTANDARD2_0
        [MemberNotNull(nameof(OneQuoteString))]
        [MemberNotNull(nameof(TwoQuoteString))]
#endif
        private void InitializeQuoteStrings()
        {
            OneQuoteString = new string(QuoteCharacter, 1);
            TwoQuoteString = new string(QuoteCharacter, 2);
        }

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

            InitializeQuoteStrings();
            EmptyLineBehavior = EmptyLineBehavior.NoColumns;
            InvalidDataRaisesException = true;
            ColumnHeaderStringComparison = StringComparison.InvariantCultureIgnoreCase;
        }

        /// <summary>
        /// Encodes a CSV field if needed by doubling any quote characters and then
        /// wrapping the field in quotes.
        /// </summary>
        /// <param name="s">Field to encode.</param>
        /// <returns>The encoded string.</returns>
        public string CsvEncode(string s)
        {
            if (HasSpecialCharacter(s))
            {
                s = s.Replace(OneQuoteString, TwoQuoteString);
                s = $"{QuoteCharacter}{s}{QuoteCharacter}";
            }
            return s;
        }
    }
}
