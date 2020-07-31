// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Tests.UnitTests.Comparer
{
    public class TaxEqualityComparer : IEqualityComparer<Tax>
    {
        public bool Equals(Tax x, Tax y)
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
                   x.Updated == y.Updated &&
                   x.IsDefault == y.IsDefault &&
                   x.Rate == y.Rate;
        }

        public int GetHashCode(Tax obj)
        {
            return obj.Id.GetHashCode() ^
                   obj.Name.GetHashCode() ^
                   obj.Created.GetHashCode() ^
                   obj.Updated.GetHashCode() ^
                   obj.Rate.GetHashCode() ^
                   obj.IsDefault.GetHashCode();
        }
    }
}
