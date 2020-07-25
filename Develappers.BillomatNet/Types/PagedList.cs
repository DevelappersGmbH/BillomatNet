// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Develappers.BillomatNet.Types
{
    /// <summary>
    /// Represents a page of a list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T>
    {
        public List<T> List { get; set; }

        public int Page { get; set; }

        public int ItemsPerPage { get; set; }

        public int TotalItems { get; set; }
    }
}
