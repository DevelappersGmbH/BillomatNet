// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Develappers.BillomatNet.Types
{
    /// <summary>
    /// The type of the payment.
    /// </summary>
    public enum PaymentType
    {
        InvoiceCorrection,
        CreditNote,
        BankCard,
        BankTransfer,
        Debit,
        Cash,
        Check,
        PayPal,
        CreditCard,
        Coupon,
        Misc
    }
}
