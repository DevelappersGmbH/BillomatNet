using System.Collections.Generic;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Tests.UnitTests.Comparer
{
    public class UnitEqualityComparer : IEqualityComparer<Unit>
    {
        public bool Equals(Unit x, Unit y)
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
                   x.Created == y.Created &&
                   x.Updated == y.Updated;
        }

        public int GetHashCode(Unit obj)
        {
            return obj.Id.GetHashCode() ^
                   obj.Name.GetHashCode() ^
                   obj.Created.GetHashCode() ^
                   obj.Updated.GetHashCode();
        }
    }
}
