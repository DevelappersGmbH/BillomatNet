// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Develappers.BillomatNet.Queries
{
    /// <summary>
    /// Represents the filter for the article.
    /// </summary>
    public class ArticleFilter
    {
        public string Title { get; set; }
        public string ArticleNumber { get; set; }
        public string Description { get; set; }
        public string CurrencyCode { get; set; }
        public int? UnitId { get; set; }
        public int? SupplierId { get; set; }
        public List<string> Tags { get; set; }
    }
}
