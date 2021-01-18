// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Xunit;

namespace Develappers.BillomatNet.Tests.IntegrationTests
{
    public class PurchaseInvoiceServiceIntegrationTests : IntegrationTestBase<PurchaseInvoiceService>
    {
        public PurchaseInvoiceServiceIntegrationTests() : base(c => new PurchaseInvoiceService(c))
        {
        }

        [Fact]
        public async Task GetPurchaseInvoiceById()
        {
            var result = await SystemUnderTest.GetByIdAsync(626880);
            Assert.NotNull(result);
        }
    }
}
