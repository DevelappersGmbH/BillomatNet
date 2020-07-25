// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq.Expressions;

namespace Develappers.BillomatNet.Queries
{
    /// <summary>
    /// Represents the sorted items.
    /// </summary>
    /// <typeparam name="TSource">
    /// Any Model which gets sorted.
    /// </typeparam>
    public class SortItem<TSource> : ISortItem<TSource>
    {
        public Expression<Func<TSource, object>> Property { get; set; }
        public SortOrder Order { get; set; }
    }

    /// <summary>
    /// The interface for <see cref="SortItem{TSource}"/>
    /// </summary>
    /// <typeparam name="T">
    /// Any Model which gets sorted.
    /// </typeparam>
    public interface ISortItem<T> { }
}
