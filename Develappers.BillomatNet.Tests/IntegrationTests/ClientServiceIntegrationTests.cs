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
    public class ClientServiceIntegrationTests : IntegrationTestBase<ClientService>
    {
        public ClientServiceIntegrationTests() : base(c => new BillomatClient(c).Clients)
        {
        }

        [Fact]
        public async Task GetClientsByName()
        {
            // ReSharper disable once RedundantArgumentDefaultValue
            var query = new Query<Client, ClientFilter>()
                .AddFilter(x => x.Name, "Regiofaktur")
                .AddSort(x => x.City, SortOrder.Ascending);

            var result = await SystemUnderTest.GetListAsync(query, CancellationToken.None);

            Assert.True(result.List.Count > 0);
        }

        [Fact]
        public async Task GetClients()
        {
            var result = await SystemUnderTest.GetListAsync(CancellationToken.None);

            Assert.True(result.List.Count > 0);
        }

        [Fact]
        public async Task GetMyself()
        {
            var result = await SystemUnderTest.MyselfAsync(CancellationToken.None);

            Assert.True(result.Id > 0);
        }

        [Fact]
        public async Task GetClientById()
        {
            var result = await SystemUnderTest.GetByIdAsync(1227912);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetClientByIdWhenNotFound()
        {
            await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.GetByIdAsync(1));
        }
    }
}
