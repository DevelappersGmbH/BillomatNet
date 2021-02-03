// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Develappers.BillomatNet.Types
{
    /// <summary>
    /// The status of the invoice.
    /// </summary>
    public enum InvoiceStatus
    {
        Draft,
        Open,
        Paid,
        Overdue,
        Canceled
    }
}
