// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Queries;
using Develappers.BillomatNet.Types;
using Xunit;

namespace Develappers.BillomatNet.Tests.IntegrationTests
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class InvoiceServiceIntegrationTests : IntegrationTestBase<InvoiceService>
    {
        public InvoiceServiceIntegrationTests() : base(c => new InvoiceService(c))
        {
        }

        [Fact]
        public async Task GetFilteredInvoices()
        {
            var result = await SystemUnderTest.GetListAsync(
                new Query<Invoice, InvoiceFilter>().AddFilter(x => x.Status, new List<InvoiceStatus> { InvoiceStatus.Draft } ));
            Assert.True(result.List.Count > 0);
        }

        [Fact]
        public async Task GetInvoices()
        {
            var result = await SystemUnderTest.GetListAsync(CancellationToken.None);

            Assert.True(result.List.Count > 0);
        }

        [Fact]
        public async Task GetInvoiceById()
        {
            var result = await SystemUnderTest.GetByIdAsync(1322705);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetInvoiceByIdWhenNotFound()
        {
            var result = await SystemUnderTest.GetByIdAsync(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetInvoiceByIdWhenNotAuthorized()
        {
            Configuration.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var ex = await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.GetByIdAsync(1));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task CreateInvoice()
        {
            var cs = new ClientService(Configuration);
            var cl = await cs.GetByIdAsync(1506365);

            var articleService = new ArticleService(Configuration);
            var article = await articleService.GetByIdAsync(835226);

            var unitService = new UnitService(Configuration);
            var unit = await unitService.GetByIdAsync(article.UnitId.Value);

            var taxService = new TaxService(Configuration);
            var taxes = await taxService.GetByIdAsync(article.TaxId.Value);

            var settingsService = new SettingsService(Configuration);
            var settings = await settingsService.GetAsync();

            var label = "xUnit Test Object";

            var invoiceItemList = new List<InvoiceItem>
            {
                new InvoiceItem
                {
                    ArticleId = article.Id,
                    Unit = unit.Name,
                    Quantity = 300,
                    UnitPrice = 1.0f,
                    Title = article.Title,
                    Description = article.Description,
                    TaxName = taxes.Name,
                    TaxRate = taxes.Rate,
                }
            };

            var inv = new Invoice
            {
                ClientId = cl.Id,
                Date = DateTime.Now.Date,
                Label = label,
                Quote = 1,
                InvoiceItems = invoiceItemList,
                SupplyDateType = SupplyDateType.SupplyDate
            };

            var result = await SystemUnderTest.CreateAsync(inv);
            var getInvItem = await SystemUnderTest.GetByIdAsync(result.Id);
            Assert.NotNull(getInvItem);
            await SystemUnderTest.DeleteAsync(result.Id);
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task CreateInvoiceWhenNotAuthorized()
        {
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

            Configuration.ApiKey = "ajfkjeinodafkejlkdsjklj";

            await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.CreateAsync(inv));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task CreateInvoiceWhenWhenArgumentException()
        {
            var inv = new Invoice();

            await Assert.ThrowsAsync<ArgumentException>(() => SystemUnderTest.CreateAsync(inv));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task EditInvoice()
        {
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

            var result = await SystemUnderTest.CreateAsync(inv);
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

            var editedResult = await SystemUnderTest.EditAsync(editedInv);
            Assert.NotNull(await SystemUnderTest.GetByIdAsync(editedResult.Id));

            await SystemUnderTest.DeleteAsync(editedResult.Id);
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task EditInvoiceArgumentException()
        {
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

            var result = await SystemUnderTest.CreateAsync(inv);
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

            await Assert.ThrowsAsync<ArgumentException>(() => SystemUnderTest.EditAsync(editedInv));

            await SystemUnderTest.DeleteAsync(result.Id);
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task EdiInvoiceArgumentNotAuthorized()
        {
            var conf = Configuration.DeepCopy(Configuration);

            var cs = new ClientService(conf);
            var cl = await cs.GetByIdAsync(1506365);

            var articleService = new ArticleService(conf);
            var articles = await articleService.GetByIdAsync(835226);

            var unitService = new UnitService(conf);
            var units = await unitService.GetByIdAsync(articles.UnitId.Value);

            var taxService = new TaxService(conf);
            var taxes = await taxService.GetByIdAsync(articles.TaxId.Value);

            var settingsService = new SettingsService(conf);
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
            var service = new InvoiceService(conf);
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

            Configuration.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var ex = await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.EditAsync(editedInv));

            await service.DeleteAsync(result.Id);
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task EditInvoiceNotFound()
        {
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

            var ex = await Assert.ThrowsAsync<NotFoundException>(() => SystemUnderTest.EditAsync(inv));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task DeleteInvoice()
        {
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

            var result = await SystemUnderTest.CreateAsync(inv);

            Assert.Equal(title, result.Label);
            await SystemUnderTest.DeleteAsync(result.Id);

            var result2 = await SystemUnderTest.GetByIdAsync(result.Id);
            Assert.Null(result2);
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task DeleteInvoiceArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => SystemUnderTest.DeleteAsync(0));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task DeleteInvoiceNotAuthorized()
        {
            Configuration.ApiKey = "ajfkjeinodafkejlkdsjklj";
            await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.DeleteAsync(1));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task DeleteInvoiceNotFound()
        {
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => SystemUnderTest.DeleteAsync(1));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task CancelInvoiceItem()
        {
            await SystemUnderTest.CancelAsync(4340407);

            Assert.True(true);
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task UncancelInvoiceItem()
        {
            await SystemUnderTest.UncancelAsync(4340407);

            Assert.True(true);
        }


        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task CompleteInvoice()
        {
            await SystemUnderTest.CompleteAsync(4340406);

            Assert.True(true);
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task DeleteInvoiceExisting()
        {
            await SystemUnderTest.DeleteAsync(4447692);

            Assert.True(true);
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task DeleteInvoiceItemOpen()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => SystemUnderTest.DeleteAsync(3745041));
        }
    }
}
