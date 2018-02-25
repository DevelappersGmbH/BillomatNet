using System.Threading;
using System.Threading.Tasks;
using TaurusSoftware.BillomatNet.Queries;
using TaurusSoftware.BillomatNet.Types;
using Xunit;

namespace TaurusSoftware.BillomatNet.Tests
{
    public class InvoiceServiceIntegrationTests
    {
        //[Fact]
        //public async Task GetClientsByName()
        //{
        //    var config = Helpers.GetTestConfiguration();

        //    var service = new ClientService(config);

        //    var query = new Query<Client, ClientFilter>()
        //        .AddFilter(x => x.Name, "Regiofaktur")
        //        .AddSort(x => x.City, SortOrder.Ascending);

        //    var result = await service.GetListAsync(query, CancellationToken.None);

        //    Assert.True(result.List.Count > 0);
        //}

        [Fact]
        public async Task GetInvoices()
        {
            var config = Helpers.GetTestConfiguration();

            var service = new InvoiceService(config);

            var result = await service.GetListAsync(CancellationToken.None);

            Assert.True(result.List.Count > 0);
        }

        //[Fact]
        //public async Task GetClientById()
        //{
        //    var config = Helpers.GetTestConfiguration();

        //    var service = new ClientService(config);

        //    var result = await service.GetById(1227912);
        //    Assert.NotNull(result);
        //}
    }
}