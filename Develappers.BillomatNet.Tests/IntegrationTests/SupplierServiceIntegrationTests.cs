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
    public class SupplierServiceIntegrationTests : IntegrationTestBase<SupplierService>
    {
        public SupplierServiceIntegrationTests() : base(c => new SupplierService(c))
        {
        }

        [Fact]
        public async Task GetSuppliersByName()
        {
            // ReSharper disable once RedundantArgumentDefaultValue
            var query = new Query<Supplier, SupplierFilter>()
                .AddFilter(x => x.Name, "GmbH")
                .AddSort(x => x.City, SortOrder.Ascending);

            var result = await SystemUnderTest.GetListAsync(query, CancellationToken.None);

            Assert.True(result.List.Count > 0);
        }

        [Fact]
        public async Task GetSuppliers()
        {
            var result = await SystemUnderTest.GetListAsync(CancellationToken.None);

            Assert.True(result.List.Count > 0);
        }

        [Fact]
        public async Task GetSupplierById()
        {
            var result = await SystemUnderTest.GetByIdAsync(40577);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetSupplierByIdWhenNotFound()
        {
            await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.GetByIdAsync(1));
        }
    }
}
