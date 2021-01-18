// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

namespace Develappers.BillomatNet.Types
{
    /// <summary>
    /// Represents an incoming invoice
    /// </summary>
    public class PurchaseInvoice
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int SupplierId { get; set; }
        public string ClientNumber { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public DateTime? DueDate { get; set; }
        public string Address { get; set; }
        public PurchaseInvoiceStatus Status { get; set; }
        public string Label { get; set; }
        public string Note { get; set; }
        public float TotalGross { get; set; }
        public float TotalNet { get; set; }
        public string CurrencyCode { get; set; }
        public float Quote { get; set; }
        public float PaidAmount { get; set; }
        public float OpenAmount { get; set; }
        public string ExpenseAccountNumber { get; set; }
        public string Category { get; set; }
        public int PageCount { get; set; }
    }
}
