using System;
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

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetInvoiceById()
        {
            var config = Helpers.GetTestConfiguration();

            var service = new InvoiceService(config);
            var result = await service.GetByIdAsync(1322705);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetInvoiceByIdWhenNotFound()
        {
            var config = Helpers.GetTestConfiguration();

            var service = new InvoiceService(config);
            var result = await service.GetByIdAsync(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetInvoiceItem()
        {
            var config = Helpers.GetTestConfiguration();

            var service = new InvoiceService(config);

            var result = await service.GetItemByIdAsync(3246680);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetInvoiceItems()
        {
            var config = Helpers.GetTestConfiguration();

            var service = new InvoiceService(config);

            var result = await service.GetItemsAsync(1322705, CancellationToken.None);

            Assert.True(result.List.Count > 0);
        }


        [Fact]
        public async Task DeleteInvoiceItemNotExisting()
        {
            var config = Helpers.GetTestConfiguration();

            var service = new InvoiceService(config);

            // delete an invoice that doesn't exist
            await service.DeleteAsync(4447692);

            Assert.True(true);
        }

        //[Fact]
        //public async Task CancelInvoiceItem()
        //{
        //    var config = Helpers.GetTestConfiguration();

        //    var service = new InvoiceService(config);

        //    await service.CancelAsync(4340407);

        //    Assert.True(true);
        //}

        //[Fact]
        //public async Task UncancelInvoiceItem()
        //{
        //    var config = Helpers.GetTestConfiguration();

        //    var service = new InvoiceService(config);

        //    await service.UncancelAsync(4340407);

        //    Assert.True(true);
        //}


        //[Fact]
        //public async Task CompleteInvoiceItem()
        //{
        //    var config = Helpers.GetTestConfiguration();

        //    var service = new InvoiceService(config);

        //    // delete an invoice that doesn't exist
        //    await service.CompleteAsync(4340406);

        //    Assert.True(true);
        //}

        //[Fact]
        //public async Task DeleteInvoiceItemExisting()
        //{
        //    var config = Helpers.GetTestConfiguration();

        //    var service = new InvoiceService(config);

        //    // delete an invoice that doesn't exist
        //    await service.DeleteAsync(4447692);

        //    Assert.True(true);
        //}

        //[Fact]
        //public async Task DeleteInvoiceItemOpen()
        //{
        //    var config = Helpers.GetTestConfiguration();

        //    var service = new InvoiceService(config);

        //    // try to delete an invoice that is open
        //    await Assert.ThrowsAsync<ArgumentException>(() => service.DeleteAsync(3745041));
        //}


        [Fact]
        public async Task GetMultipleInvoiceItems()
        {
            var config = Helpers.GetTestConfiguration();

            var service = new InvoiceService(config);

            var list = await service.GetListAsync(CancellationToken.None);

            foreach (var invoice in list.List)
            {
                var result = await service.GetItemsAsync(invoice.Id, CancellationToken.None);
            }

            Assert.True(true);
        }
    }
}