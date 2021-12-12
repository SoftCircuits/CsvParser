// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SoftCircuits.CsvParser
{
    /// <summary>
    /// Derive from this abstract class to implement custom column mapping.
    /// Call the <see cref="MapColumn"></see> method from the constructor
    /// for each property being mapped.
    /// </summary>
    /// <typeparam name="T">Specifies the class that is being mapped.</typeparam>
    public abstract class ColumnMaps<T> where T : class, new()
    {
        private readonly List<ColumnMap> Maps = new();

        /// <summary>
        /// Adds mapping information to a class property or field. Returns a
        /// <see cref="ColumnMap"></see> reference, allowing for fluent syntax.
        /// For example, <c>MapColumn(m => m.Member).Index(0).Name("NewName")</c>.
        /// </summary>
        /// <param name="expression">Member access expression that identifies
        /// the class member to be mapped.</param>
        public ColumnMap MapColumn<TMember>(Expression<Func<T, TMember>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            MemberExpression? member = null;
            if (expression.Body.NodeType == ExpressionType.Convert)
                member = (expression.Body as UnaryExpression)?.Operand as MemberExpression;
            else if (expression.Body.NodeType == ExpressionType.MemberAccess)
                member = expression.Body as MemberExpression;

            if (member == null)
                throw new InvalidOperationException("Unsupported expression type used to specify class property.");

            ColumnMap propertyMap = new(member.Member.Name);
            Maps.Add(propertyMap);
            return propertyMap;
        }

        /// <summary>
        /// Adds mapping information to a class property or field. Returns a
        /// <see cref="ColumnMap"></see> reference, allowing for fluent syntax.
        /// For example, <c>MapColumn("MemberName").Index(0).Name("NewName")</c>.
        /// </summary>
        /// <param name="memberName">Name of the class member to be mapped.</param>
        public ColumnMap MapColumn(string memberName)
        {
            if (memberName == null)
                throw new ArgumentNullException(nameof(memberName));

            ColumnMap propertyMap = new(memberName);
            Maps.Add(propertyMap);
            return propertyMap;
        }

        internal List<ColumnMap> GetCustomMaps() => Maps;
    }
}
