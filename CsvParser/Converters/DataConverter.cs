// Copyright (c) 2019-2023 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;

namespace SoftCircuits.CsvParser
{
    /// <summary>
    /// Interface for all data converters.
    /// </summary>
    public interface IDataConverter
    {
        /// <summary>
        /// Returns the data type this object converts.
        /// </summary>
        /// <returns>Returns the data type this object converts.</returns>
        Type GetDataType();

        /// <summary>
        /// Converts the object to a string.
        /// </summary>
        /// <param name="value">The object to be converted to a string.</param>
        /// <returns>A string representation of <paramref name="value"/>.</returns>
        string ConvertToString(object? value);

        /// <summary>
        /// Converts a string back to an object. Returns <c>true</c> if
        /// successful or <c>false</c> if the string could not be
        /// converted. If the string cannot be converted, <paramref name="value"/>
        /// should be set to its default value.
        /// </summary>
        /// <param name="s">The string to convert to an object.</param>
        /// <param name="value">Receives the resulting value that was
        /// parsed from the string.</param>
        /// <returns>True if successful, false if the string could not
        /// be converted.</returns>
        bool TryConvertFromString(string s, out object? value);
    }

    /// <summary>
    /// Base class for strongly typed data converters. The easiest way
    /// to implement <see cref="IDataConverter"/> in a type-safe way is
    /// to derive your data converter class from this class with the
    /// appropriate data type.
    /// </summary>
    /// <typeparam name="T">The data type that is being converted to
    /// a string, and back again.</typeparam>
    public abstract class DataConverter<T> : IDataConverter
    {
        /// <summary>
        /// Returns the type this converter supports.
        /// </summary>
        /// <returns>Returns the type this converter supports.</returns>
        public Type GetDataType() => typeof(T);

        /// <summary>
        /// Converts the variable to a string.
        /// </summary>
        /// <param name="value">The variable to be converted to a string.</param>
        /// <returns>A string representation of <paramref name="value"/>.</returns>
        public string ConvertToString(object? value) => ConvertToString((T?)value);

        /// <summary>
        /// Converts a string back to a value. Returns <c>true</c> if
        /// successful or <c>false</c> if the string could not be
        /// converted.
        /// </summary>
        /// <param name="s">The string to convert to a value.</param>
        /// <param name="value">Receives the resulting value that was
        /// parsed from the string.</param>
        /// <returns>True if successful, false if the string could not
        /// be converted.</returns>
        public bool TryConvertFromString(string s, out object? value)
        {
            if (TryConvertFromString(s, out T? temp))
            {
                value = temp;
                return true;
            }
            value = default;
            return false;
        }

        #region Abstract methods

        /// <summary>
        /// Override this abstract method to implement your own logic to convert
        /// a value of type <typeparamref name="T"/> to a string.
        /// </summary>
        /// <param name="value">The value to be converted to a string.</param>
        /// <returns>Returns a string representation of
        /// <typeparamref name="T"/></returns>
        public abstract string ConvertToString(T? value);

        /// <summary>
        /// Override this abstract method to implement your own logic to convert
        /// a string back to a value of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="s">The string to convert to a value.</param>
        /// <param name="value">Receives the resulting value that was
        /// parsed from the string.</param>
        /// <returns>True if successful, false if the string could not
        /// be converted.</returns>
//#if !NETSTANDARD2_0
//        public abstract bool TryConvertFromString(string s, [NotNullWhen(true)] out T? value);
//#else
        public abstract bool TryConvertFromString(string s, out T? value);
//#endif

        #endregion

    }

    /// <summary>
    /// Converter for unsupported types. Throws an exception if called to convert
    /// from a string.
    /// </summary>
    internal class UnsupportedTypeConverter : IDataConverter
    {
        private readonly Type Type;

        /// <summary>
        /// Returns the type this converter supports.
        /// </summary>
        /// <returns>Returns the type this converter supports.</returns>
        public Type GetDataType() => Type;

        public UnsupportedTypeConverter(Type type)
        {
            Type = type;
        }

        public string ConvertToString(object? value) => value?.ToString() ?? string.Empty;

        public bool TryConvertFromString(string s, out object? value)
        {
            throw new UnsupportedDataTypeException(Type);
        }
    }
}
