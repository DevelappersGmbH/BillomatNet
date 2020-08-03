// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Types;
using Xunit;

namespace Develappers.BillomatNet.Tests.IntegrationTests
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class InvoiceServiceItemsIntegrationTests : IntegrationTestBase<InvoiceService>
    {
        public InvoiceServiceItemsIntegrationTests() : base(c => new InvoiceService(c))
        {
        }

        [Fact]
        public async Task GetInvoiceItem()
        {
            var result = await SystemUnderTest.GetItemByIdAsync(3246680);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetInvoiceItems()
        {
            var result = await SystemUnderTest.GetItemsAsync(1322705, CancellationToken.None);

            Assert.True(result.List.Count > 0);
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task CreateInvoiceItem()
        {
            var cl = await SystemUnderTest.GetByIdAsync(1506365);

            var articleService = new ArticleService(Configuration);
            var article = await articleService.GetByIdAsync(835226);

            var unitService = new UnitService(Configuration);
            var units = await unitService.GetByIdAsync(article.UnitId.Value);

            var taxService = new TaxService(Configuration);
            var taxes = await taxService.GetByIdAsync(article.TaxId.Value);

            var settingsService = new SettingsService(Configuration);
            var settings = await settingsService.GetAsync();

            var label = "xUnit Test Object";

            var inv = new Invoice
            {
                ClientId = cl.Id,
                Date = DateTime.Now.Date,
                Label = label,
                Quote = 1
            };

            var invoiceResult = await SystemUnderTest.CreateAsync(inv);
            Assert.Equal(label, inv.Label);

            var item = new InvoiceItem
            {
                InvoiceId = invoiceResult.Id,
                ArticleId = article.Id,
                Unit = units.Name,
                Quantity = 300,
                UnitPrice = 1.0f,
                Title = article.Title,
                Description = article.Description,
                TaxName = taxes.Name,
                TaxRate = taxes.Rate,
            };

            var itemResult = await SystemUnderTest.CreateItemAsync(item);
            Assert.NotNull(itemResult);

            await SystemUnderTest.DeleteAsync(invoiceResult.Id);
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task GetMultipleInvoiceItems()
        {
            var list = await SystemUnderTest.GetListAsync(CancellationToken.None);
            foreach (var invoice in list.List)
            {
                var result = await SystemUnderTest.GetItemsAsync(invoice.Id, CancellationToken.None);
            }

            Assert.True(true);
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task CreateInvoiceItemWhenArgumentException()
        {
            var item = new InvoiceItem();
            await Assert.ThrowsAsync<ArgumentException>(() => SystemUnderTest.CreateItemAsync(item));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task CreateInvoiceItemWhenNotAuthorized()
        {
            Configuration.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var item = new InvoiceItem
            {
                InvoiceId = 7458050
            };

            var ex = await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.CreateItemAsync(item));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task CreateInvoiceItemWhenNotFound()
        {
            var item = new InvoiceItem
            {
                InvoiceId = 7458050
            };

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => SystemUnderTest.CreateItemAsync(item));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task EditInvoiceItem()
        {
            #region Initializing to create
            var cs = new ClientService(Configuration);
            var cl = await cs.GetByIdAsync(1506365);

            var articleService = new ArticleService(Configuration);
            var articles = await articleService.GetByIdAsync(835226);

            var unitService = new UnitService(Configuration);
            var units = await unitService.GetByIdAsync(articles.UnitId.Value);

            var taxService = new TaxService(Configuration);
            var taxes = await taxService.GetByIdAsync(articles.TaxId.Value);

            var settingsService = new SettingsService(Configuration);
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

            var invoiceResult = await SystemUnderTest.CreateAsync(inv);
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

            var itemResult = await SystemUnderTest.CreateItemAsync(item);
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

            var editedItemResult = await SystemUnderTest.EditItemAsync(editedItem);
            Assert.Equal(2.0f, editedItemResult.UnitPrice);

            await SystemUnderTest.DeleteAsync(invoiceResult.Id);
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task EditInvoiceItemArgumentException()
        {
            var item = new InvoiceItem { };

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => SystemUnderTest.EditItemAsync(item));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task EditInvoiceItemWhenNotAuthorized()
        {
            Configuration.ApiKey = "ajfkjeinodafkejlkdsjklj";

            var item = new InvoiceItem
            {
                Id = 1,
                InvoiceId = 1
            };

            var ex = await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.EditItemAsync(item));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task EditInvoiceItemWhenNotFound()
        {
            var item = new InvoiceItem
            {
                Id = 1,
                InvoiceId = 1
            };

            var ex = await Assert.ThrowsAsync<NotFoundException>(() => SystemUnderTest.EditItemAsync(item));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task DeleteInvoiceItem()
        {

            #region Initializing to create
            var cs = new ClientService(Configuration);
            var cl = await cs.GetByIdAsync(1506365);

            var articleService = new ArticleService(Configuration);
            var articles = await articleService.GetByIdAsync(835226);

            var unitService = new UnitService(Configuration);
            var units = await unitService.GetByIdAsync(articles.UnitId.Value);

            var taxService = new TaxService(Configuration);
            var taxes = await taxService.GetByIdAsync(articles.TaxId.Value);

            var settingsService = new SettingsService(Configuration);
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

            var invResult = await SystemUnderTest.CreateAsync(inv);

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

            var invItemResult = await SystemUnderTest.CreateItemAsync(invoiceItem);

            await SystemUnderTest.DeleteInvoiceItemAsync(invItemResult.Id);
            Assert.Null(await SystemUnderTest.GetItemByIdAsync(invItemResult.Id));

            await SystemUnderTest.DeleteAsync(invResult.Id);
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task DeleteInvoiceItemArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => SystemUnderTest.DeleteInvoiceItemAsync(0));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task DeleteInvoiceItemNotAuthorized()
        {
            Configuration.ApiKey = "ajfkjeinodafkejlkdsjklj";
            await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.DeleteInvoiceItemAsync(1));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task DeleteInvoiceItemNotFound()
        {
            await Assert.ThrowsAsync<NotFoundException>(() => SystemUnderTest.DeleteInvoiceItemAsync(1));
        }
    }
}
