// Copyright (c) 2019-2020 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

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

        #region Special Characters

        /// <summary>
        /// These are special characters in CSV files. If a column contains any
        /// of these characters, the entire column is wrapped in quotes.
        /// </summary>
        private char[] SpecialCharacters;

        // Indexes into SpecialCharacters
        private const int ColumnDelimiterIndex = 0;
        private const int QuoteCharacterIndex = 1;

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
        /// Returns <c>true</c> is the given string contains any special characters.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        internal bool HasSpecialCharacter(string s) => s.IndexOfAny(SpecialCharacters) >= 0;

        #endregion

        /// <summary>
        /// Specifies how blank lines are interpreted by CsvReader. This setting is set
        /// to <see cref="EmptyLineBehavior.NoColumns"></see> by default.
        /// </summary>
        public EmptyLineBehavior EmptyLineBehavior;

        /// <summary>
        /// If true, <see cref="CsvDataReader{T}"></see> will raise a
        /// <see cref="BadDataFormatException"></see> exception when it encounters
        /// data that cannot be converted to the corresponding data type. This
        /// setting is <c>true</c> by default.
        /// </summary>
        public bool InvalidDataRaisesException { get; set; }

        /// <summary>
        /// Specifies the string comparison type used by
        /// <see cref="CsvDataReader.ReadHeaders(bool)"></see> to compare column
        /// names against column headers when it's passed a <c>true</c> argument.
        /// This setting is set to
        /// <see cref="StringComparison.InvariantCultureIgnoreCase"></see> by
        /// default.
        /// </summary>
        public StringComparison ColumnHeaderStringComparison { get; set; }

        /// <summary>
        /// Initializes a new <c>CsvSettings</c> instance.
        /// </summary>
        public CsvSettings()
        {
            SpecialCharacters = new char[] { ',', '"', '\r', '\n' };
            EmptyLineBehavior = EmptyLineBehavior.NoColumns;
            InvalidDataRaisesException = true;
            ColumnHeaderStringComparison = StringComparison.InvariantCultureIgnoreCase;
        }
    }
}
