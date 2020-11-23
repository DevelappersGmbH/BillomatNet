// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Tests.UnitTests.Comparer
{
    class InvoiceItemEqualityComparer : IEqualityComparer<InvoiceItem>
    {
        private const float FloatingPointTolerance = 0.0001f;

        public bool Equals(InvoiceItem x, InvoiceItem y)
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
                x.ArticleId == y.ArticleId &&
                x.InvoiceId == y.InvoiceId &&
                x.Position == y.Position &&
                x.Unit == y.Unit &&
                x.Quantity == y.Quantity &&
                Math.Abs(x.UnitPrice - y.UnitPrice) < FloatingPointTolerance &&
                x.TaxName == y.TaxName &&
                x.TaxRate == y.TaxRate &&
                x.Description == y.Description &&
                Math.Abs(x.TotalGross - y.TotalGross) < FloatingPointTolerance &&
                Math.Abs(x.TotalNet - y.TotalNet) < FloatingPointTolerance &&
                x.Reduction == y.Reduction &&
                Math.Abs(x.TotalGrossUnreduced - y.TotalGrossUnreduced) < FloatingPointTolerance &&
                Math.Abs(x.TotalNetUnreduced - y.TotalNetUnreduced) < FloatingPointTolerance;
        }

        public int GetHashCode(InvoiceItem obj)
        {
            return obj.Id.GetHashCode() ^
                obj.ArticleId.GetHashCode() ^
                obj.InvoiceId.GetHashCode() ^
                obj.Position.GetHashCode() ^
                obj.Unit.GetHashCode() ^
                obj.Quantity.GetHashCode() ^
                obj.UnitPrice.GetHashCode() ^
                obj.TaxName.GetHashCode() ^
                obj.TaxRate.GetHashCode() ^
                obj.Description.GetHashCode() ^
                obj.TotalGross.GetHashCode() ^
                obj.TotalNet.GetHashCode() ^
                obj.Reduction.GetHashCode() ^
                obj.TotalGrossUnreduced.GetHashCode() ^
                obj.TotalNetUnreduced.GetHashCode();
        }
    }
}
