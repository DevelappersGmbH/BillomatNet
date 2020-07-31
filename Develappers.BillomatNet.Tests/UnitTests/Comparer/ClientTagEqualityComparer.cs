using System.Collections.Generic;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Tests.UnitTests.Comparer
{
    public class ClientTagEqualityComparer : IEqualityComparer<ClientTag>
    {
        public bool Equals(ClientTag x, ClientTag y)
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
                   x.ClientId == y.ClientId;
        }

        public int GetHashCode(ClientTag obj)
        {
            return obj.Id.GetHashCode() ^
                   obj.Name.GetHashCode() ^
                   obj.ClientId.GetHashCode();
        }
    }
}
