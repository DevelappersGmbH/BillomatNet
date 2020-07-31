using System.Collections.Generic;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Tests.UnitTests.Comparer
{
    public class InvoiceCommentEqualityComparer : IEqualityComparer<InvoiceComment>
    {
        public bool Equals(InvoiceComment x, InvoiceComment y)
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
                   x.Comment == y.Comment &&
                   x.ActionKey == y.ActionKey &&
                   x.Public == y.Public &&
                   x.ByClient == y.ByClient &&
                   x.UserId == y.UserId &&
                   x.EmailId == y.EmailId &&
                   x.ClientId == y.ClientId &&
                   x.InvoiceId == y.InvoiceId;
        }

        public int GetHashCode(InvoiceComment obj)
        {
            return obj.Id.GetHashCode() ^
                   obj.Created.GetHashCode() ^
                   obj.Comment.GetHashCode() ^
                   obj.ActionKey.GetHashCode() ^
                   obj.Public.GetHashCode() ^
                   obj.ByClient.GetHashCode() ^
                   obj.UserId.GetHashCode() ^
                   obj.EmailId.GetHashCode() ^
                   obj.ClientId.GetHashCode() ^
                   obj.InvoiceId.GetHashCode();
        }
    }
}
