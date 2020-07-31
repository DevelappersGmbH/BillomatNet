using System.Collections.Generic;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Tests.UnitTests.Comparer
{
    public class InvoicePaymentEqualityComparer : IEqualityComparer<InvoicePayment>
    {
        public bool Equals(InvoicePayment x, InvoicePayment y)
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
                   x.InvoiceId == y.InvoiceId &&
                   x.UserId == y.UserId &&
                   x.Date == y.Date &&
                   x.Amount == y.Amount &&
                   x.Comment == y.Comment &&
                   x.TransactionPurpose == y.TransactionPurpose &&
                   x.CurrencyCode == y.CurrencyCode &&
                   x.Quote == y.Quote &&
                   x.Type == y.Type &&
                   x.MarkInvoiceAsPaid == y.MarkInvoiceAsPaid;
        }

        public int GetHashCode(InvoicePayment obj)
        {
            return obj.Id.GetHashCode() ^
                   obj.Created.GetHashCode() ^
                   obj.InvoiceId.GetHashCode() ^
                   obj.UserId.GetHashCode() ^
                   obj.Date.GetHashCode() ^
                   obj.Amount.GetHashCode() ^
                   obj.Comment.GetHashCode() ^
                   obj.TransactionPurpose.GetHashCode() ^
                   obj.CurrencyCode.GetHashCode() ^
                   obj.Quote.GetHashCode() ^
                   obj.Type.GetHashCode() ^
                   obj.MarkInvoiceAsPaid.GetHashCode();
        }
    }
}
