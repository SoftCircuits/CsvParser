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
        /// <summary>
        /// These are special characters in CSV files. If a column contains any
        /// of these characters, the entire column is wrapped in quotes.
        /// </summary>
        private char[] SpecialCharacters;

        // Indices into SpecialCharacters
        private const int ColumnDelimiterIndex = 0;
        private const int QuoteCharacterIndex = 1;

        /// <summary>
        /// Specifies how blank lines are interpreted by CsvReader.
        /// </summary>
        public EmptyLineBehavior EmptyLineBehavior;

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

        public CsvSettings()
        {
            SpecialCharacters = new char[] { ',', '"', '\r', '\n' };
            EmptyLineBehavior = EmptyLineBehavior.NoColumns;
        }

        // Returns true if string contains any special characters.
        internal bool HasSpecialCharacter(string s) => s.IndexOfAny(SpecialCharacters) >= 0;
    }
}
