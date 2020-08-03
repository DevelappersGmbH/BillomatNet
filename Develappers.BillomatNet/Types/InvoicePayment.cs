// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

namespace Develappers.BillomatNet.Types
{
    /// <summary>
    /// Represents an invoice payment.
    /// </summary>
    public class InvoicePayment
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int InvoiceId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public float Amount { get; set; }
        public string Comment { get; set; }
        public string TransactionPurpose { get; set; }
        public string CurrencyCode { get; set; }
        public float Quote { get; set; }
        public PaymentType Type { get; set; }
        public bool MarkInvoiceAsPaid { get; set; }
    }
}
