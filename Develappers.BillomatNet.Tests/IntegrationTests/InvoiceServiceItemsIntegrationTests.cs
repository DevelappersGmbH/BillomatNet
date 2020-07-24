using Develappers.BillomatNet.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Develappers.BillomatNet.Tests.IntegrationTests
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    [Trait(Traits.Category, Traits.Categories.IntegrationTest)]
    public class InvoiceServiceItemsIntegrationTests
    {
        [Fact]
        public async Task GetInvoiceItem()
        {
            var config = IntegrationTests.Helpers.GetTestConfiguration();
            var service = new InvoiceService(config);
            var result = await service.GetItemByIdAsync(3246680);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetInvoiceItems()
        {
            var config = IntegrationTests.Helpers.GetTestConfiguration();
            var service = new InvoiceService(config);
            var result = await service.GetItemsAsync(1322705, CancellationToken.None);

            Assert.True(result.List.Count > 0);
        }

        [Fact]
        public async Task CreateInvoiceItem()
        {
            var config = IntegrationTests.Helpers.GetTestConfiguration();
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

            var label = "xUnit Test Object";

            var inv = new Invoice
            {
                ClientId = cl.Id,
                Date = DateTime.Now.Date,
                Label = label,
                Quote = 1
            };
            #endregion

            var invoiceResult = await service.CreateAsync(inv);
            Assert.Equal(label, inv.Label);

            var item = new InvoiceItem
            {
                InvoiceId = invoiceResult.Id,
                ArticleId = articles.Id,
                Unit = units.Name,
                Quantity = 300,
                UnitPrice = 1.0f,
                Title = articles.Title,
                Description = articles.Description,
                TaxName = taxes.Name,
                TaxRate = taxes.Rate,
            };

            var itemResult = await service.CreateAsync(item);
            Assert.NotNull(itemResult);

            await service.DeleteAsync(invoiceResult.Id);
        }

        [Fact]
        public async Task GetMultipleInvoiceItems()
        {
            var config = IntegrationTests.Helpers.GetTestConfiguration();
            var service = new InvoiceService(config);
            var list = await service.GetListAsync(CancellationToken.None);

            foreach (var invoice in list.List)
            {
                var result = await service.GetItemsAsync(invoice.Id, CancellationToken.None);
            }

            Assert.True(true);
        }

        [Fact]
        public async Task CreateInvoiceItemWhenArgumentException()
        {
            var config = IntegrationTests.Helpers.GetTestConfiguration();
            var service = new InvoiceService(config);

            var item = new InvoiceItem();

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(item));
        }

        [Fact]
        public async Task CreateInvoiceItemWhenNotAuthorized()
        {
            var config = IntegrationTests.Helpers.GetTestConfiguration();
            config.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var service = new InvoiceService(config);

            var item = new InvoiceItem
            {
                InvoiceId = 7458050
            };

            var ex = await Assert.ThrowsAsync<NotAuthorizedException>(() => service.CreateAsync(item));
        }

        [Fact]
        public async Task CreateInvoiceItemWhenNotFound()
        {
            var config = IntegrationTests.Helpers.GetTestConfiguration();
            var service = new InvoiceService(config);

            var item = new InvoiceItem
            {
                InvoiceId = 7458050
            };

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(item));
        }

        [Fact]
        public async Task EditInvoiceItem()
        {
            var config = IntegrationTests.Helpers.GetTestConfiguration();
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

            var label = "xUnit Test Object";

            var inv = new Invoice
            {
                ClientId = cl.Id,
                Date = DateTime.Now.Date,
                Label = label,
                Quote = 1
            };
            #endregion

            var invoiceResult = await service.CreateAsync(inv);
            Assert.Equal(label, inv.Label);

            var item = new InvoiceItem
            {
                InvoiceId = invoiceResult.Id,
                ArticleId = articles.Id,
                Unit = units.Name,
                Quantity = 300,
                UnitPrice = 1.0f,
                Title = articles.Title,
                Description = articles.Description,
                TaxName = taxes.Name,
                TaxRate = taxes.Rate,
            };

            var itemResult = await service.CreateAsync(item);
            Assert.NotNull(itemResult);

            var editedItem = new InvoiceItem
            {
                Id = itemResult.Id,
                InvoiceId = invoiceResult.Id,
                ArticleId = articles.Id,
                Unit = units.Name,
                Quantity = 300,
                UnitPrice = 2.0f,
                Title = articles.Title,
                Description = articles.Description,
                TaxName = taxes.Name,
                TaxRate = taxes.Rate,
            };

            var editedItemResult = await service.EditAsync(editedItem);
            Assert.Equal(2.0f, editedItemResult.UnitPrice);

            await service.DeleteAsync(invoiceResult.Id);
        }

        [Fact]
        public async Task EditInvoiceItemArgumentException()
        {
            var config = IntegrationTests.Helpers.GetTestConfiguration();
            var service = new InvoiceService(config);

            var item = new InvoiceItem { };

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.EditAsync(item));
        }

        [Fact]
        public async Task EditInvoiceItemWhenNotAuthorized()
        {
            var config = IntegrationTests.Helpers.GetTestConfiguration();
            config.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var service = new InvoiceService(config);

            var item = new InvoiceItem
            {
                Id = 1,
                InvoiceId = 1
            };

            var ex = await Assert.ThrowsAsync<NotAuthorizedException>(() => service.EditAsync(item));
        }

        [Fact]
        public async Task EditInvoiceItemWhenNotFound()
        {
            var config = IntegrationTests.Helpers.GetTestConfiguration();
            var service = new InvoiceService(config);

            var item = new InvoiceItem
            {
                Id = 1,
                InvoiceId = 1
            };

            var ex = await Assert.ThrowsAsync<NotFoundException>(() => service.EditAsync(item));
        }

        [Fact]
        public async Task DeleteInvoiceItem()
        {
            var config = IntegrationTests.Helpers.GetTestConfiguration();
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
                Quote = 1
            };
            #endregion

            var invResult = await service.CreateAsync(inv);

            var invoiceItem = new InvoiceItem
            {
                InvoiceId = invResult.Id,
                ArticleId = articles.Id,
                Unit = units.Name,
                Quantity = 300,
                UnitPrice = 1.0f,
                Title = articles.Title,
                Description = articles.Description,
                TaxName = taxes.Name,
                TaxRate = taxes.Rate,
            };

            var invItemResult = await service.CreateAsync(invoiceItem);

            await service.DeleteInvoiceItemAsync(invItemResult.Id);
            Assert.Null(await service.GetItemByIdAsync(invItemResult.Id));

            await service.DeleteAsync(invResult.Id);
        }

        [Fact]
        public async Task DeleteInvoiceItemArgumentException()
        {
            var config = IntegrationTests.Helpers.GetTestConfiguration();
            var service = new InvoiceService(config);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.DeleteInvoiceItemAsync(0));
        }

        [Fact]
        public async Task DeleteInvoiceItemNotAuthorized()
        {
            var config = IntegrationTests.Helpers.GetTestConfiguration();
            config.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var service = new InvoiceService(config);

            var ex = await Assert.ThrowsAsync<NotAuthorizedException>(() => service.DeleteInvoiceItemAsync(1));
        }

        [Fact]
        public async Task DeleteInvoiceItemNotFound()
        {
            var config = IntegrationTests.Helpers.GetTestConfiguration();
            var service = new InvoiceService(config);

            var ex = await Assert.ThrowsAsync<NotFoundException>(() => service.DeleteInvoiceItemAsync(1));
        }
    }
}
