using System.Collections.Generic;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Tests.UnitTests.Comparer
{
    public class InvoiceTagEqualityComparer : IEqualityComparer<InvoiceTag>
    {
        public bool Equals(InvoiceTag x, InvoiceTag y)
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
                   x.Name == y.Name &&
                   x.InvoiceId == y.InvoiceId;
        }

        public int GetHashCode(InvoiceTag obj)
        {
            return obj.Id.GetHashCode() ^
                   obj.Name.GetHashCode() ^
                   obj.InvoiceId.GetHashCode();
        }
    }
}
