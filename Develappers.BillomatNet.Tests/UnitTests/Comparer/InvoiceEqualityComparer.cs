// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Tests.UnitTests.Comparer
{
    public class InvoiceEqualityComparer : IEqualityComparer<Invoice>
    {
        private const float FloatingPointTolerance = 0.0001f;

        public bool Equals(Invoice x, Invoice y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return x.Id == y.Id &&
                   x.Created == y.Created &&
                   //x.Updated == y.Updated &&
                   x.ClientId == y.ClientId &&
                   x.ContactId == y.ContactId &&
                   x.InvoiceNumber == y.InvoiceNumber &&
                   x.Number == y.Number &&
                   x.NumberPre == y.NumberPre &&
                   x.NumberLength == y.NumberLength &&
                   x.Title == y.Title &&
                   x.Date == y.Date &&
                   x.SupplyDateType == y.SupplyDateType &&
                   //x.SupplyDate == y.SupplyDate &&
                   x.DueDate == y.DueDate &&
                   x.DueDays == y.DueDays &&
                   x.Address == y.Address &&
                   x.Status == y.Status &&
                   x.Label == y.Label &&
                   x.Intro == y.Intro &&
                   x.Note == y.Note &&
                   Math.Abs(x.TotalNet - y.TotalNet) < FloatingPointTolerance &&
                   Math.Abs(x.TotalGross - y.TotalGross) < FloatingPointTolerance &&
                   x.Reduction == y.Reduction &&
                   Math.Abs(x.TotalNetUnreduced - y.TotalNetUnreduced) < FloatingPointTolerance &&
                   Math.Abs(x.TotalGrossUnreduced - y.TotalGrossUnreduced) < FloatingPointTolerance &&
                   x.CurrencyCode == y.CurrencyCode &&
                   Math.Abs(x.Quote - y.Quote) < FloatingPointTolerance &&
                   x.NetGross == y.NetGross &&
                   Math.Abs(x.DiscountRate - y.DiscountRate) < FloatingPointTolerance &&
                   x.DiscountDate == y.DiscountDate &&
                   x.DiscountAmount == y.DiscountAmount &&
                   Math.Abs(x.PaidAmount - y.PaidAmount) < FloatingPointTolerance &&
                   //PaymentTypes
                   x.CustomerPortalUrl == y.CustomerPortalUrl &&
                   //Taxes
                   x.InvoiceId == y.InvoiceId &&
                   x.OfferId == y.OfferId &&
                   x.ConfirmationId == y.ConfirmationId &&
                   x.RecurringId == y.RecurringId &&
                   //digproceeded
                   // CustomField
                   x.TemplateId == y.TemplateId;
        }

        public int GetHashCode(Invoice obj)
        {
            return obj.Id.GetHashCode() ^
                   obj.Created.GetHashCode() ^
                   obj.ClientId.GetHashCode() ^
                   obj.InvoiceId.GetHashCode();

            // TODO: add other values
        }
    }
}
