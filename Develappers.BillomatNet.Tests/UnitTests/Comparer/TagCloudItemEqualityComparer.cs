using System.Collections.Generic;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Tests.UnitTests.Comparer
{
    public class TagCloudItemEqualityComparer : IEqualityComparer<TagCloudItem>
    {
        public bool Equals(TagCloudItem x, TagCloudItem y)
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
                   x.Count == y.Count;
        }

        public int GetHashCode(TagCloudItem obj)
        {
            return obj.Id.GetHashCode() ^
                   obj.Name.GetHashCode() ^
                   obj.Count.GetHashCode();
        }
    }
}
