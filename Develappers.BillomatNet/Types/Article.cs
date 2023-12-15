// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

namespace Develappers.BillomatNet.Types
{
    /// <summary>
    /// Represents an article.
    /// </summary>
    public class Article
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public string ArticleNumber { get; set; }

        public int? Number { get; set; }

        public string NumberPrefix { get; set; }

        public int NumberLength { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public float? SalesPrice { get; set; }

        public float? SalesPrice2 { get; set; }

        public float? SalesPrice3 { get; set; }

        public float? SalesPrice4 { get; set; }

        public float? SalesPrice5 { get; set; }

        public string CurrencyCode { get; set; }

        public int? UnitId { get; set; }

        public int? TaxId { get; set; }

        public float? PurchasePrice { get; set; }

        public NetGrossType PurchasePriceNetGross { get; set; }

        public int? SupplierId { get; set; }
        public ItemType? Type { get; set; }
        public string CostCenter { get; set; }
    }
}
