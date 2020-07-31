// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Develappers.BillomatNet.Types
{
    /// <summary>
    /// The type of the commentar.
    /// </summary>
    public enum CommentType
    {
        Comment, Create, Copy, CreateFromOffer, CreateFromInvoice, CreateFromRecurring,
        Status, Payment, PaymentError, DeletePayment, Mail, Letter, Fax,
        Sign, SignMail, Cancel, ErrorMail, CreateCreditNote,
        ReminderCreate, ReminderStatus, ReminderMail, ReminderErrorMail, ReminderLetter, ReminderFax, ReminderSign, ReminderSignMail, ReminderCancel, ReminderDelete
    }
}
