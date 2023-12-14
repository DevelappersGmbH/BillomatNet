// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Develappers.BillomatNet.Types
{
    /// <summary>
    /// Represents an invoice item
    /// </summary>
    public class InvoiceItem
    {
        public int Id { get; set; }
        public int? ArticleId { get; set; }

        public int InvoiceId { get; set; }

        public int Position { get; set; }

        public string Unit { get; set; }

        public float Quantity { get; set; }

        public float UnitPrice { get; set; }

        public string TaxName { get; set; }

        public float? TaxRate { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public float TotalGross { get; set; }

        public float TotalNet { get; set; }

        public IReduction Reduction { get; set; }

        public float TotalGrossUnreduced { get; set; }

        public float TotalNetUnreduced { get; set; }
        public InvoiceItemType? InvoiceItemType { get; set; }
    }
}
