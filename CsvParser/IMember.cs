using System;

namespace SoftCircuits.CsvParser
{
    /// <summary>
    /// Interface for property and field members.
    /// </summary>
    internal interface IMember
    {
        Type Type { get; }
        string Name { get; }
        bool CanRead { get; }
        bool CanWrite { get; }
        ColumnMapAttribute ColumnMapAttribute { get; }
        object GetValue(object item);
        void SetValue(object item, object value);
    }
}
