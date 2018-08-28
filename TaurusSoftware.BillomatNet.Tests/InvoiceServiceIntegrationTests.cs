using System.Threading;
using System.Threading.Tasks;
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

        [Fact]
        public async Task GetInvoicePdf()
        {
            var config = Helpers.GetTestConfiguration();

            var service = new InvoiceService(config);

            var result = await service.GetPdfAsync(1322705);

            //File.WriteAllBytes($"C:\\temp\\{result.FileName}", result.Bytes);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetInvoice()
        {
            var config = Helpers.GetTestConfiguration();

            var service = new InvoiceService(config);

            var result = await service.GetByIdAsync(1322705);

            //File.WriteAllBytes($"C:\\temp\\{result.FileName}", result.Bytes);

            Assert.NotNull(result);
        }
    }
}