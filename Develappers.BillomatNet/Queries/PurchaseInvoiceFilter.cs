// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Queries
{
    /// <summary>
    /// Represents the filter for incoming invoices.
    /// </summary>
    public class PurchaseInvoiceFilter
    {
        public int? SupplierId { get; set; }
        public string Number { get; set; }
        public List<PurchaseInvoiceStatus> Status { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string Note { get; set; }
        public List<string> Tags { get; set; }
    }
}
