// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Develappers.BillomatNet.Types;
using Xunit;

namespace Develappers.BillomatNet.Tests
{
    public class DomainAssert
    {
        public static void Equal(Tax expected, Tax actual)
        {
            if (expected == null && actual == null)
            {
                return;
            }

            if (expected == null || actual == null)
            {
                Assert.True(false, "one of the items is null");
            }

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Rate, actual.Rate);
            Assert.Equal(expected.Created, actual.Created);
            Assert.Equal(expected.Updated, actual.Updated);
            Assert.Equal(expected.IsDefault, actual.IsDefault);
        }
    }
}
