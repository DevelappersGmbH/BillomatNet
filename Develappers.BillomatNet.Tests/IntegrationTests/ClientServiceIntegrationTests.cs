using Develappers.BillomatNet.Queries;
using Develappers.BillomatNet.Types;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Develappers.BillomatNet.Tests.IntegrationTests
{
    [Trait(TraitNames.Category, CategoryNames.IntegrationTest)]
    public class ClientServiceIntegrationTests
    {
        [Fact]
        public async Task GetClientsByName()
        {
            var config = Helpers.GetTestConfiguration();

            var service = new ClientService(config);
            // ReSharper disable once RedundantArgumentDefaultValue
            var query = new Query<Client, ClientFilter>()
                .AddFilter(x => x.Name, "Regiofaktur")
                .AddSort(x => x.City, SortOrder.Ascending);

            var result = await service.GetListAsync(query, CancellationToken.None);

            Assert.True(result.List.Count > 0);
        }

        [Fact]
        public async Task GetClients()
        {
            var config = Helpers.GetTestConfiguration();

            var service = new ClientService(config);

            var result = await service.GetListAsync(CancellationToken.None);

            Assert.True(result.List.Count > 0);
        }

        [Fact]
        public async Task GetClientById()
        {
            var config = Helpers.GetTestConfiguration();

            var service = new ClientService(config);

            var result = await service.GetByIdAsync(1227912);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetClientByIdWhenNotFound()
        {
            var config = Helpers.GetTestConfiguration();

            var service = new ClientService(config);

            await Assert.ThrowsAsync<NotAuthorizedException>(() => service.GetByIdAsync(1));
        }
    }
}
