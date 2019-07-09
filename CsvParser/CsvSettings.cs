// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.CsvParser
{
    /// <summary>
    /// Determines how empty lines are interpreted when reading CSV files.
    /// These values do not affect empty lines that occur within quoted fields
    /// or empty lines that appear at the end of the input file.
    /// </summary>
    public enum EmptyLineBehavior
    {
        /// <summary>
        /// Empty lines are interpreted as a line with zero columns.
        /// </summary>
        NoColumns,
        /// <summary>
        /// Empty lines are interpreted as a line with a single empty column.
        /// </summary>
        EmptyColumn,
        /// <summary>
        /// Empty lines are skipped over as though they did not exist.
        /// </summary>
        Ignore,
        /// <summary>
        /// An empty line is interpreted as the end of the input file.
        /// </summary>
        EndOfFile,
    }

    /// <summary>
    /// Common base class for CSV reader and writer classes.
    /// </summary>
    public class CsvSettings
    {
        private const int ColumnDelimiterIndex = 0;
        private const int QuoteCharacterIndex = 1;

        // Used for formatting quoted columns
        private string OneQuoteString = null;
        private string TwoQuoteString = null;

        /// <summary>
        /// These are special characters in CSV files. If a column contains any
        /// of these characters, the entire column is wrapped in quotes.
        /// </summary>
        private char[] SpecialCharacters;

        /// <summary>
        /// Gets/sets the character used to delimit columns.
        /// </summary>
        public char ColumnDelimiter
        {
            get => SpecialCharacters[ColumnDelimiterIndex];
            set => SpecialCharacters[ColumnDelimiterIndex] = value;
        }

        /// <summary>
        /// Gets/sets the character used to quote values that contain special characters.
        /// </summary>
        public char QuoteCharacter
        {
            get => SpecialCharacters[QuoteCharacterIndex];
            set => SpecialCharacters[QuoteCharacterIndex] = value;
        }

        /// <summary>
        /// Specifies how blank lines are interpreted by CsvReader.
        /// </summary>
        public EmptyLineBehavior EmptyLineBehavior;

        public CsvSettings()
        {
            SpecialCharacters = new char[] { ',', '"', '\r', '\n' };
            EmptyLineBehavior = EmptyLineBehavior.NoColumns;
        }

        internal string CsvEncode(string s)
        {
            if (s.IndexOfAny(SpecialCharacters) == -1)
                return s;
            // Ensure we're using current quote character
            if (OneQuoteString == null || OneQuoteString[0] != QuoteCharacter)
            {
                OneQuoteString = QuoteCharacter.ToString();
                TwoQuoteString = string.Format("{0}{0}", QuoteCharacter);
            }
            return string.Format("{0}{1}{0}", QuoteCharacter, s.Replace(OneQuoteString, TwoQuoteString));
        }
    }
}
