using Develappers.BillomatNet.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Develappers.BillomatNet.Tests
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

        //[Fact]
        //public async Task GetFilteredInvoices()
        //{
        //    var config = Helpers.GetTestConfiguration();
        //    var service = new InvoiceService(config);
        //    var result = await service.GetListAsync(
        //        new Query<Invoice, InvoiceFilter>().AddFilter(x => x.Status, InvoiceStatus.Draft));
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
        public async Task GetInvoiceByIdWhenNotAuthorized()
        {
            var config = Helpers.GetTestConfiguration();
            config.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var service = new InvoiceService(config);

            var ex = await Assert.ThrowsAsync<NotAuthorizedException>(() => service.GetByIdAsync(1));
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
        public async Task DeleteInvoiceItem()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new InvoiceService(config);

            #region Initializing to create
            var cs = new ClientService(config);
            var cl = await cs.GetByIdAsync(1506365);

            var articleService = new ArticleService(config);
            var articles = await articleService.GetByIdAsync(835226);

            var unitService = new UnitService(config);
            var units = await unitService.GetByIdAsync(articles.UnitId.Value);

            var taxService = new TaxService(config);
            var taxes = await taxService.GetByIdAsync(articles.TaxId.Value);

            var settingsService = new SettingsService(config);
            var settings = await settingsService.GetAsync();

            var title = "xUnit Test Object";

            var invoiceItemList = new List<InvoiceItem>
            {
                new InvoiceItem
                {
                    ArticleId = articles.Id,
                    Unit = units.Name,
                    Quantity = 300,
                    UnitPrice = 1.0f,
                    Title = articles.Title,
                    Description = articles.Description,
                    TaxName = taxes.Name,
                    TaxRate = taxes.Rate,
                }
            };

            var inv = new Invoice
            {
                ClientId = cl.Id,
                Address = cl.Address,
                Date = DateTime.Now.Date,
                DueDate = DateTime.Now.Date.AddDays(14),
                DueDays = 20,
                SupplyDate = new DateSupplyDate(),
                Label = title,
                CurrencyCode = "EUR",
                NetGross = NetGrossType.Net,
                Reduction = new AbsoluteReduction { Value = 0 },
                DiscountRate = 0f,
                DiscountDate = DateTime.Now.Date.AddDays(0),
                PaymentTypes = new List<string>(),
                Quote = 1,
                InvoiceItems = invoiceItemList
            };
            #endregion

            var result = await service.CreateAsync(inv);

            Assert.Equal(title, result.Label);
            await service.DeleteAsync(result.Id);

            var result2 = await service.GetByIdAsync(result.Id);
            Assert.Null(result2);
        }

        [Fact]
        public async Task DeleteInvoiceItemArgumentException()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new InvoiceService(config);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.DeleteAsync(0));
        }

        [Fact]
        public async Task DeleteInvoiceItemNotAuthorized()
        {
            var config = Helpers.GetTestConfiguration();
            config.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var service = new InvoiceService(config);

            var ex = await Assert.ThrowsAsync<NotAuthorizedException>(() => service.DeleteAsync(1));
        }

        [Fact]
        public async Task DeleteInvoiceItemNotFound()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new InvoiceService(config);

            var ex = await Assert.ThrowsAsync<NotFoundException>(() => service.DeleteAsync(1));
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

        [Fact]
        public async Task CreateInvoiceItem()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new InvoiceService(config);

            #region Initializing to create
            var cs = new ClientService(config);
            var cl = await cs.GetByIdAsync(1506365);

            var articleService = new ArticleService(config);
            var articles = await articleService.GetByIdAsync(835226);

            var unitService = new UnitService(config);
            var units = await unitService.GetByIdAsync(articles.UnitId.Value);

            var taxService = new TaxService(config);
            var taxes = await taxService.GetByIdAsync(articles.TaxId.Value);

            var settingsService = new SettingsService(config);
            var settings = await settingsService.GetAsync();

            var title = "xUnit Test Object";

            var invoiceItemList = new List<InvoiceItem>
            {
                new InvoiceItem
                {
                    ArticleId = articles.Id,
                    Unit = units.Name,
                    Quantity = 300,
                    UnitPrice = 1.0f,
                    Title = articles.Title,
                    Description = articles.Description,
                    TaxName = taxes.Name,
                    TaxRate = taxes.Rate,
                }
            };

            var inv = new Invoice
            {
                ClientId = cl.Id,
                //Date = DateTime.Now.Date,
                Label = title,
                Quote = 1,
                InvoiceItems = invoiceItemList
            };
            #endregion

            var result = await service.CreateAsync(inv);
            var getInvItem = await service.GetByIdAsync(result.Id);
            Assert.NotNull(getInvItem);
            await service.DeleteAsync(result.Id);
        }

        [Fact]
        public async Task CreateInvoiceItemWhenNotAuthorized()
        {
            var config = Helpers.GetTestConfiguration();
            var config2 = Helpers.GetTestConfiguration();
            config2.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var service = new InvoiceService(config2);

            #region Initializing to create
            var cs = new ClientService(config);
            var cl = await cs.GetByIdAsync(1506365);

            var articleService = new ArticleService(config);
            var articles = await articleService.GetByIdAsync(835226);

            var unitService = new UnitService(config);
            var units = await unitService.GetByIdAsync(articles.UnitId.Value);

            var taxService = new TaxService(config);
            var taxes = await taxService.GetByIdAsync(articles.TaxId.Value);

            var settingsService = new SettingsService(config);
            var settings = await settingsService.GetAsync();

            var title = "xUnit Test Object";

            var invoiceItemList = new List<InvoiceItem>
            {
                new InvoiceItem
                {
                    ArticleId = articles.Id,
                    Unit = units.Name,
                    Quantity = 300,
                    UnitPrice = 1.0f,
                    Title = articles.Title,
                    Description = articles.Description,
                    TaxName = taxes.Name,
                    TaxRate = taxes.Rate,
                }
            };

            var inv = new Invoice
            {
                ClientId = cl.Id,
                Address = cl.Address,
                //Title = "",
                Date = DateTime.Now.Date,
                DueDate = DateTime.Now.Date.AddDays(14),
                DueDays = 20,
                SupplyDate = new DateSupplyDate(),
                Label = title,
                //Intro = "Hiermit stellen wir Ihnen die folgenden Positionen in Rechnung.",
                //Note = "Netto K/P Programm Test",
                CurrencyCode = "EUR",
                NetGross = NetGrossType.Net,
                Reduction = new AbsoluteReduction { Value = 0 },
                DiscountRate = 0f,
                DiscountDate = DateTime.Now.Date.AddDays(0),
                PaymentTypes = new List<string>(),
                Quote = 1,
                InvoiceItems = invoiceItemList
            };
            #endregion

            var ex = await Assert.ThrowsAsync<NotAuthorizedException>(() => service.CreateAsync(inv));
        }

        [Fact]
        public async Task CreateInvoiceItemWhenNull()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new InvoiceService(config);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(null));
        }

        [Fact]
        public async Task EdiInvoice()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new InvoiceService(config);

            #region Initializing to create
            var cs = new ClientService(config);
            var cl = await cs.GetByIdAsync(1506365);

            var articleService = new ArticleService(config);
            var articles = await articleService.GetByIdAsync(835226);

            var unitService = new UnitService(config);
            var units = await unitService.GetByIdAsync(articles.UnitId.Value);

            var taxService = new TaxService(config);
            var taxes = await taxService.GetByIdAsync(articles.TaxId.Value);

            var settingsService = new SettingsService(config);
            var settings = await settingsService.GetAsync();

            var title = "xUnit Test Object";

            var invoiceItemList = new List<InvoiceItem>
            {
                new InvoiceItem
                {
                    ArticleId = articles.Id,
                    Unit = units.Name,
                    Quantity = 300,
                    UnitPrice = 1.0f,
                    Title = articles.Title,
                    Description = articles.Description,
                    TaxName = taxes.Name,
                    TaxRate = taxes.Rate,
                }
            };

            var inv = new Invoice
            {
                ClientId = cl.Id,
                Date = DateTime.Now.Date,
                Label = title,
                Quote = 1,
                InvoiceItems = invoiceItemList
            };
            #endregion

            var result = await service.CreateAsync(inv);
            Assert.NotNull(result);

            var editedLabel = "xUint Edited";

            var editedInv = new Invoice
            {
                Id = result.Id,
                ClientId = result.ClientId,
                Date = result.Date,
                Label = editedLabel,
                Quote = result.Quote,
                InvoiceItems = result.InvoiceItems
            };

            var editedResult = await service.EditAsync(editedInv);
            Assert.NotNull(await service.GetByIdAsync(editedResult.Id));

            await service.DeleteAsync(editedResult.Id);
        }

        [Fact]
        public async Task EdiInvoiceArgumentException()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new InvoiceService(config);

            #region Initializing to create
            var cs = new ClientService(config);
            var cl = await cs.GetByIdAsync(1506365);

            var articleService = new ArticleService(config);
            var articles = await articleService.GetByIdAsync(835226);

            var unitService = new UnitService(config);
            var units = await unitService.GetByIdAsync(articles.UnitId.Value);

            var taxService = new TaxService(config);
            var taxes = await taxService.GetByIdAsync(articles.TaxId.Value);

            var settingsService = new SettingsService(config);
            var settings = await settingsService.GetAsync();

            var title = "xUnit Test Object";

            var invoiceItemList = new List<InvoiceItem>
            {
                new InvoiceItem
                {
                    ArticleId = articles.Id,
                    Unit = units.Name,
                    Quantity = 300,
                    UnitPrice = 1.0f,
                    Title = articles.Title,
                    Description = articles.Description,
                    TaxName = taxes.Name,
                    TaxRate = taxes.Rate,
                }
            };

            var inv = new Invoice
            {
                ClientId = cl.Id,
                Date = DateTime.Now.Date,
                Label = title,
                Quote = 1,
                InvoiceItems = invoiceItemList
            };
            #endregion

            var result = await service.CreateAsync(inv);
            Assert.NotNull(result);

            var editedLabel = "xUint Edited";

            var editedInv = new Invoice
            {
                ClientId = result.ClientId,
                Date = result.Date,
                Label = editedLabel,
                Quote = result.Quote,
                InvoiceItems = result.InvoiceItems
            };

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.EditAsync(editedInv));

            await service.DeleteAsync(result.Id);
        }

        [Fact]
        public async Task EdiInvoiceArgumentNotAuthorized()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new InvoiceService(config);

            #region Initializing to create
            var cs = new ClientService(config);
            var cl = await cs.GetByIdAsync(1506365);

            var articleService = new ArticleService(config);
            var articles = await articleService.GetByIdAsync(835226);

            var unitService = new UnitService(config);
            var units = await unitService.GetByIdAsync(articles.UnitId.Value);

            var taxService = new TaxService(config);
            var taxes = await taxService.GetByIdAsync(articles.TaxId.Value);

            var settingsService = new SettingsService(config);
            var settings = await settingsService.GetAsync();

            var title = "xUnit Test Object";

            var invoiceItemList = new List<InvoiceItem>
            {
                new InvoiceItem
                {
                    ArticleId = articles.Id,
                    Unit = units.Name,
                    Quantity = 300,
                    UnitPrice = 1.0f,
                    Title = articles.Title,
                    Description = articles.Description,
                    TaxName = taxes.Name,
                    TaxRate = taxes.Rate,
                }
            };

            var inv = new Invoice
            {
                ClientId = cl.Id,
                Date = DateTime.Now.Date,
                Label = title,
                Quote = 1,
                InvoiceItems = invoiceItemList
            };
            #endregion

            var result = await service.CreateAsync(inv);
            Assert.NotNull(result);

            var editedLabel = "xUint Edited";

            var editedInv = new Invoice
            {
                Id = result.Id,
                ClientId = result.ClientId,
                Date = result.Date,
                Label = editedLabel,
                Quote = result.Quote,
                InvoiceItems = result.InvoiceItems
            };

            var editConf = Helpers.GetTestConfiguration();
            editConf.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var editService = new InvoiceService(editConf);

            var ex = await Assert.ThrowsAsync<NotAuthorizedException>(() => editService.EditAsync(editedInv));

            await service.DeleteAsync(result.Id);
        }

        [Fact]
        public async Task EdiInvoiceNotFound()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new InvoiceService(config);

            #region Initializing to create
            var cs = new ClientService(config);
            var cl = await cs.GetByIdAsync(1506365);

            var articleService = new ArticleService(config);
            var articles = await articleService.GetByIdAsync(835226);

            var unitService = new UnitService(config);
            var units = await unitService.GetByIdAsync(articles.UnitId.Value);

            var taxService = new TaxService(config);
            var taxes = await taxService.GetByIdAsync(articles.TaxId.Value);

            var settingsService = new SettingsService(config);
            var settings = await settingsService.GetAsync();

            var title = "xUnit Test Object";

            var invoiceItemList = new List<InvoiceItem>
            {
                new InvoiceItem
                {
                    ArticleId = articles.Id,
                    Unit = units.Name,
                    Quantity = 300,
                    UnitPrice = 1.0f,
                    Title = articles.Title,
                    Description = articles.Description,
                    TaxName = taxes.Name,
                    TaxRate = taxes.Rate,
                }
            };

            var inv = new Invoice
            {
                Id = 1,
                ClientId = cl.Id,
                Date = DateTime.Now.Date,
                Label = title,
                Quote = 1,
                InvoiceItems = invoiceItemList
            };
            #endregion

            var ex = await Assert.ThrowsAsync<NotFoundException>(() => service.EditAsync(inv));
        }
    }
}