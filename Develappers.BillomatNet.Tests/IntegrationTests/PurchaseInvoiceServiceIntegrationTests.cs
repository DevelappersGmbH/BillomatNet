// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Queries;
using Develappers.BillomatNet.Types;
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

        [Fact]
        public async Task GetPurchaseInvoiceBySupplier()
        {
            // ReSharper disable once RedundantArgumentDefaultValue
            var query = new Query<PurchaseInvoice, PurchaseInvoiceFilter>()
                .AddFilter(x => x.SupplierId, 50013)
                .AddSort(x => x.Date, SortOrder.Descending);

            var result = await SystemUnderTest.GetListAsync(query, CancellationToken.None);

            Assert.True(result.List.Count > 0);
        }
    }
}
