// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Api.Net;
using Develappers.BillomatNet.Types;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Develappers.BillomatNet.Tests.UnitTests
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class InvoiceServiceTemplatesTests : UnitTestBase<InvoiceService>
    {
        [Fact]
        public async Task Create_WithCorrectValues_NoTemplate_NoFreeText_ShouldReturnCorrectValues()
        {
            //arrange
            var expectedRequestUri = new Uri("/api/invoices", UriKind.Relative);
            const string expectedRequestBody = "{\"invoice\":{\"id\":\"0\",\"created\":\"0001-01-01T00:00:00.0000000\",\"updated\":\"0001-01-01T00:00:00.0000000\",\"contact_id\":\"7722\",\"client_id\":\"485054\",\"date\":\"2020-08-06\",\"supply_date\":\"\",\"supply_date_type\":\"\",\"status\":\"DRAFT\",\"discount_amount\":\"3\",\"total_gross\":\"0\",\"total_net\":\"0\",\"quote\":\"1\",\"net_gross\":\"NET\",\"reduction\":\"0\",\"total_gross_unreduced\":\"0\",\"total_net_unreduced\":\"0\",\"paid_amount\":\"0\",\"open_amount\":\"0\"}}";
            const string responseBody = "{\"invoice\":{\"id\":\"7563765\",\"created\":\"2020-08-06T09:25:37+02:00\",\"updated\":\"2020-08-06T09:25:37+02:00\",\"client_id\":\"485054\",\"contact_id\":\"7722\",\"invoice_number\":\"RE198\",\"number\":\"\",\"number_pre\":\"RE\",\"number_length\":\"3\",\"title\":\"[Invoice.invoice_number]\",\"date\":\"2020-08-06\",\"supply_date\":\"\",\"supply_date_type\":\"\",\"due_date\":\"2020-08-11\",\"due_days\":\"5\",\"address\":\"Hallo GmbH\\nHerr Ronny Roller\\nAcme Str. 12\\n12345 München\",\"status\":\"DRAFT\",\"label\":\"\",\"intro\":\"Hiermit stellen wir Ihnen die folgenden Positionen in Rechnung.\",\"note\":\"Bitte überweisen Sie den Rechnungsbetrag bis zum[Invoice.due_date] auf unser Konto.\",\"total_net\":\"0\",\"total_gross\":\"0\",\"reduction\":\"\",\"total_net_unreduced\":\"0\",\"total_gross_unreduced\":\"0\",\"currency_code\":\"EUR\",\"quote\":\"1\",\"net_gross\":\"NET\",\"discount_rate\":\"2\",\"discount_date\":\"2020-08-13\",\"discount_days\":\"7\",\"discount_amount\":null,\"paid_amount\":\"0\",\"open_amount\":\"0\",\"payment_types\":\"CREDIT_CARD,DEBIT,CASH\",\"customerportal_url\":\"https:\\/\\/develappersdev.billomat.net\\/customerportal\\/invoices\\/show\\/c4da1693-51b1-4f81-a05c-1e412b9a9abd\",\"invoice_id\":\"\",\"offer_id\":\"\",\"confirmation_id\":\"\",\"recurring_id\":\"\",\"dig_proceeded\":\"0\",\"template_id\":\"\",\"customfield\":\"\"}}";

            var model = new Invoice
            {
                ClientId = 485054,
                Created = DateTime.Parse("0001-01-01T00:00:00", CultureInfo.InvariantCulture),
                Updated = DateTime.Parse("0001-01-01T00:00:00", CultureInfo.InvariantCulture),
                ContactId = 7722,
                Date = DateTime.Parse("2020-08-06", CultureInfo.InvariantCulture),
                DiscountAmount = 3,
                Quote = 1,
                Status = InvoiceStatus.Draft,
                NetGross = NetGrossType.Net,
            };
            var expectedResult = new Invoice
            {
                Id = 7563765,
                Created = DateTime.Parse("2020-08-06T09:25:37+02:00", CultureInfo.InvariantCulture),
                Updated = DateTime.Parse("2020-08-06T09:25:37+02:00", CultureInfo.InvariantCulture),
                ClientId = 485054,
                ContactId = 7722,
                InvoiceNumber = "RE198",
                NumberPre = "RE",
                NumberLength = 3,
                Title = "[Invoice.invoice_number]",
                Date = DateTime.Parse("2020-08-06", CultureInfo.InvariantCulture),
                DueDate = DateTime.Parse("2020-08-11", CultureInfo.InvariantCulture),
                DueDays = 5,
                Address = "Hallo GmbH\nHerr Ronny Roller\nAcme Str. 12\n12345 München",
                Status = InvoiceStatus.Draft,
                Intro = "Hiermit stellen wir Ihnen die folgenden Positionen in Rechnung.",
                Note = "Bitte überweisen Sie den Rechnungsbetrag bis zum[Invoice.due_date] auf unser Konto.",
                CurrencyCode = "EUR",
                Quote = 1,
                NetGross = NetGrossType.Net,
                DiscountRate = 2,
                DiscountDate = DateTime.Parse("2020-08-13", CultureInfo.InvariantCulture),
                DiscountDays = 7,
                PaymentTypes = new List<string> { "CREDIT_CARD", "DEBIT", "CASH" },
                CustomerPortalUrl = "https://develappersdev.billomat.net/customerportal/invoices/show/c4da1693-51b1-4f81-a05c-1e412b9a9abd",
                Label = null
            };

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.PostAsync(expectedRequestUri, expectedRequestBody, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(responseBody));

            var sut = GetSystemUnderTest(http);

            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            //act
            var result = await sut.CreateAsync(model);

            // assert
            A.CallTo(() => http.PostAsync(expectedRequestUri, expectedRequestBody, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task Create_NoTemplate_WithFreeText_ShouldReturnCorrectValues()
        {
            //arrange
            var expectedRequestUri = new Uri("/api/invoices", UriKind.Relative);
            const string expectedRequestBody = "{\"invoice\":{\"id\":\"0\",\"created\":\"0001-01-01T00:00:00.0000000\",\"updated\":\"0001-01-01T00:00:00.0000000\",\"contact_id\":\"7722\",\"client_id\":\"485054\",\"date\":\"2020-08-06\",\"supply_date\":\"\",\"supply_date_type\":\"\",\"status\":\"DRAFT\",\"discount_amount\":\"3\",\"total_gross\":\"0\",\"total_net\":\"0\",\"quote\":\"1\",\"net_gross\":\"NET\",\"reduction\":\"0\",\"total_gross_unreduced\":\"0\",\"total_net_unreduced\":\"0\",\"paid_amount\":\"0\",\"open_amount\":\"0\",\"free_text_id\":\"243287\"}}";
            const string responseBody = "{\"invoice\":{\"id\":\"7563765\",\"created\":\"2020-08-06T09:25:37+02:00\",\"updated\":\"2020-08-06T09:25:37+02:00\",\"client_id\":\"485054\",\"contact_id\":\"7722\",\"invoice_number\":\"RE198\",\"number\":\"\",\"number_pre\":\"RE\",\"number_length\":\"3\",\"title\":\"[Invoice.invoice_number]\",\"date\":\"2020-08-06\",\"supply_date\":\"\",\"supply_date_type\":\"\",\"due_date\":\"2020-08-11\",\"due_days\":\"5\",\"address\":\"Hallo GmbH\\nHerr Ronny Roller\\nAcme Str. 12\\n12345 München\",\"status\":\"DRAFT\",\"label\":\"\",\"intro\":\"Hiermit stellen wir Ihnen die folgenden Positionen in Rechnung.\",\"note\":\"Bitte überweisen Sie den Rechnungsbetrag bis zum[Invoice.due_date] auf unser Konto.\",\"total_net\":\"0\",\"total_gross\":\"0\",\"reduction\":\"\",\"total_net_unreduced\":\"0\",\"total_gross_unreduced\":\"0\",\"currency_code\":\"EUR\",\"quote\":\"1\",\"net_gross\":\"NET\",\"discount_rate\":\"2\",\"discount_date\":\"2020-08-13\",\"discount_days\":\"7\",\"discount_amount\":null,\"paid_amount\":\"0\",\"open_amount\":\"0\",\"payment_types\":\"CREDIT_CARD,DEBIT,CASH\",\"customerportal_url\":\"https:\\/\\/develappersdev.billomat.net\\/customerportal\\/invoices\\/show\\/c4da1693-51b1-4f81-a05c-1e412b9a9abd\",\"invoice_id\":\"\",\"offer_id\":\"\",\"confirmation_id\":\"\",\"recurring_id\":\"\",\"dig_proceeded\":\"0\",\"template_id\":\"\",\"customfield\":\"\"}}";

            var model = new Invoice
            {
                ClientId = 485054,
                Created = DateTime.Parse("0001-01-01T00:00:00", CultureInfo.InvariantCulture),
                Updated = DateTime.Parse("0001-01-01T00:00:00", CultureInfo.InvariantCulture),
                ContactId = 7722,
                Date = DateTime.Parse("2020-08-06", CultureInfo.InvariantCulture),
                DiscountAmount = 3,
                Quote = 1,
                Status = InvoiceStatus.Draft,
                NetGross = NetGrossType.Net,
                FreeTextId = 243287
            };
            var expectedResult = new Invoice
            {
                Id = 7563765,
                Created = DateTime.Parse("2020-08-06T09:25:37+02:00", CultureInfo.InvariantCulture),
                Updated = DateTime.Parse("2020-08-06T09:25:37+02:00", CultureInfo.InvariantCulture),
                ClientId = 485054,
                ContactId = 7722,
                InvoiceNumber = "RE198",
                NumberPre = "RE",
                NumberLength = 3,
                Title = "[Invoice.invoice_number]",
                Date = DateTime.Parse("2020-08-06", CultureInfo.InvariantCulture),
                DueDate = DateTime.Parse("2020-08-11", CultureInfo.InvariantCulture),
                DueDays = 5,
                Address = "Hallo GmbH\nHerr Ronny Roller\nAcme Str. 12\n12345 München",
                Status = InvoiceStatus.Draft,
                Intro = "Hiermit stellen wir Ihnen die folgenden Positionen in Rechnung.",
                Note = "Bitte überweisen Sie den Rechnungsbetrag bis zum[Invoice.due_date] auf unser Konto.",
                CurrencyCode = "EUR",
                Quote = 1,
                NetGross = NetGrossType.Net,
                DiscountRate = 2,
                DiscountDate = DateTime.Parse("2020-08-13", CultureInfo.InvariantCulture),
                DiscountDays = 7,
                PaymentTypes = new List<string> { "CREDIT_CARD", "DEBIT", "CASH" },
                CustomerPortalUrl = "https://develappersdev.billomat.net/customerportal/invoices/show/c4da1693-51b1-4f81-a05c-1e412b9a9abd",
                Label = null
            };

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.PostAsync(expectedRequestUri, expectedRequestBody, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(responseBody));

            var sut = GetSystemUnderTest(http);

            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            //act
            var result = await sut.CreateAsync(model);

            // assert
            A.CallTo(() => http.PostAsync(expectedRequestUri, expectedRequestBody, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task Create_WithTemplate_NoFreeText_ShouldReturnCorrectValues()
        {
            //arrange
            var expectedRequestUri = new Uri("/api/invoices", UriKind.Relative);
            const string expectedRequestBody = "{\"invoice\":{\"id\":\"0\",\"created\":\"0001-01-01T00:00:00.0000000\",\"updated\":\"0001-01-01T00:00:00.0000000\",\"contact_id\":\"7722\",\"client_id\":\"485054\",\"date\":\"2020-08-06\",\"supply_date\":\"\",\"supply_date_type\":\"\",\"status\":\"DRAFT\",\"discount_amount\":\"3\",\"total_gross\":\"0\",\"total_net\":\"0\",\"quote\":\"1\",\"net_gross\":\"NET\",\"reduction\":\"0\",\"total_gross_unreduced\":\"0\",\"total_net_unreduced\":\"0\",\"paid_amount\":\"0\",\"open_amount\":\"0\",\"template_id\":\"623454\"}}";
            const string responseBody = "{\"invoice\":{\"id\":\"7563765\",\"created\":\"2020-08-06T09:25:37+02:00\",\"updated\":\"2020-08-06T09:25:37+02:00\",\"client_id\":\"485054\",\"contact_id\":\"7722\",\"invoice_number\":\"RE198\",\"number\":\"\",\"number_pre\":\"RE\",\"number_length\":\"3\",\"title\":\"[Invoice.invoice_number]\",\"date\":\"2020-08-06\",\"supply_date\":\"\",\"supply_date_type\":\"\",\"due_date\":\"2020-08-11\",\"due_days\":\"5\",\"address\":\"Hallo GmbH\\nHerr Ronny Roller\\nAcme Str. 12\\n12345 München\",\"status\":\"DRAFT\",\"label\":\"\",\"intro\":\"Hiermit stellen wir Ihnen die folgenden Positionen in Rechnung.\",\"note\":\"Bitte überweisen Sie den Rechnungsbetrag bis zum[Invoice.due_date] auf unser Konto.\",\"total_net\":\"0\",\"total_gross\":\"0\",\"reduction\":\"\",\"total_net_unreduced\":\"0\",\"total_gross_unreduced\":\"0\",\"currency_code\":\"EUR\",\"quote\":\"1\",\"net_gross\":\"NET\",\"discount_rate\":\"2\",\"discount_date\":\"2020-08-13\",\"discount_days\":\"7\",\"discount_amount\":null,\"paid_amount\":\"0\",\"open_amount\":\"0\",\"payment_types\":\"CREDIT_CARD,DEBIT,CASH\",\"customerportal_url\":\"https:\\/\\/develappersdev.billomat.net\\/customerportal\\/invoices\\/show\\/c4da1693-51b1-4f81-a05c-1e412b9a9abd\",\"invoice_id\":\"\",\"offer_id\":\"\",\"confirmation_id\":\"\",\"recurring_id\":\"\",\"dig_proceeded\":\"0\",\"template_id\":\"623454\",\"customfield\":\"\"}}";

            var model = new Invoice
            {
                ClientId = 485054,
                Created = DateTime.Parse("0001-01-01T00:00:00", CultureInfo.InvariantCulture),
                Updated = DateTime.Parse("0001-01-01T00:00:00", CultureInfo.InvariantCulture),
                ContactId = 7722,
                Date = DateTime.Parse("2020-08-06", CultureInfo.InvariantCulture),
                DiscountAmount = 3,
                Quote = 1,
                Status = InvoiceStatus.Draft,
                NetGross = NetGrossType.Net,
                TemplateId = 623454
            };
            var expectedResult = new Invoice
            {
                Id = 7563765,
                Created = DateTime.Parse("2020-08-06T09:25:37+02:00", CultureInfo.InvariantCulture),
                Updated = DateTime.Parse("2020-08-06T09:25:37+02:00", CultureInfo.InvariantCulture),
                ClientId = 485054,
                ContactId = 7722,
                InvoiceNumber = "RE198",
                NumberPre = "RE",
                NumberLength = 3,
                Title = "[Invoice.invoice_number]",
                Date = DateTime.Parse("2020-08-06", CultureInfo.InvariantCulture),
                DueDate = DateTime.Parse("2020-08-11", CultureInfo.InvariantCulture),
                DueDays = 5,
                Address = "Hallo GmbH\nHerr Ronny Roller\nAcme Str. 12\n12345 München",
                Status = InvoiceStatus.Draft,
                Intro = "Hiermit stellen wir Ihnen die folgenden Positionen in Rechnung.",
                Note = "Bitte überweisen Sie den Rechnungsbetrag bis zum[Invoice.due_date] auf unser Konto.",
                CurrencyCode = "EUR",
                Quote = 1,
                NetGross = NetGrossType.Net,
                DiscountRate = 2,
                DiscountDate = DateTime.Parse("2020-08-13", CultureInfo.InvariantCulture),
                DiscountDays = 7,
                PaymentTypes = new List<string> { "CREDIT_CARD", "DEBIT", "CASH" },
                CustomerPortalUrl = "https://develappersdev.billomat.net/customerportal/invoices/show/c4da1693-51b1-4f81-a05c-1e412b9a9abd",
                Label = null,
                TemplateId = 623454
            };

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.PostAsync(expectedRequestUri, expectedRequestBody, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(responseBody));

            var sut = GetSystemUnderTest(http);

            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            //act
            var result = await sut.CreateAsync(model);

            // assert
            A.CallTo(() => http.PostAsync(expectedRequestUri, expectedRequestBody, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task Create_WithTemplate_WithFreeText_ShouldReturnCorrectValues()
        {
            //arrange
            var expectedRequestUri = new Uri("/api/invoices", UriKind.Relative);
            const string expectedRequestBody = "{\"invoice\":{\"id\":\"0\",\"created\":\"0001-01-01T00:00:00.0000000\",\"updated\":\"0001-01-01T00:00:00.0000000\",\"contact_id\":\"7722\",\"client_id\":\"485054\",\"date\":\"2020-08-06\",\"supply_date\":\"\",\"supply_date_type\":\"\",\"status\":\"DRAFT\",\"discount_amount\":\"3\",\"total_gross\":\"0\",\"total_net\":\"0\",\"quote\":\"1\",\"net_gross\":\"NET\",\"reduction\":\"0\",\"total_gross_unreduced\":\"0\",\"total_net_unreduced\":\"0\",\"paid_amount\":\"0\",\"open_amount\":\"0\",\"free_text_id\":\"243287\",\"template_id\":\"421389\"}}";
            const string responseBody = "{\"invoice\":{\"id\":\"7563765\",\"created\":\"2020-08-06T09:25:37+02:00\",\"updated\":\"2020-08-06T09:25:37+02:00\",\"client_id\":\"485054\",\"contact_id\":\"7722\",\"invoice_number\":\"RE198\",\"number\":\"\",\"number_pre\":\"RE\",\"number_length\":\"3\",\"title\":\"[Invoice.invoice_number]\",\"date\":\"2020-08-06\",\"supply_date\":\"\",\"supply_date_type\":\"\",\"due_date\":\"2020-08-11\",\"due_days\":\"5\",\"address\":\"Hallo GmbH\\nHerr Ronny Roller\\nAcme Str. 12\\n12345 München\",\"status\":\"DRAFT\",\"label\":\"\",\"intro\":\"Hiermit stellen wir Ihnen die folgenden Positionen in Rechnung.\",\"note\":\"Bitte überweisen Sie den Rechnungsbetrag bis zum[Invoice.due_date] auf unser Konto.\",\"total_net\":\"0\",\"total_gross\":\"0\",\"reduction\":\"\",\"total_net_unreduced\":\"0\",\"total_gross_unreduced\":\"0\",\"currency_code\":\"EUR\",\"quote\":\"1\",\"net_gross\":\"NET\",\"discount_rate\":\"2\",\"discount_date\":\"2020-08-13\",\"discount_days\":\"7\",\"discount_amount\":null,\"paid_amount\":\"0\",\"open_amount\":\"0\",\"payment_types\":\"CREDIT_CARD,DEBIT,CASH\",\"customerportal_url\":\"https:\\/\\/develappersdev.billomat.net\\/customerportal\\/invoices\\/show\\/c4da1693-51b1-4f81-a05c-1e412b9a9abd\",\"invoice_id\":\"\",\"offer_id\":\"\",\"confirmation_id\":\"\",\"recurring_id\":\"\",\"dig_proceeded\":\"0\",\"template_id\":\"421389\",\"customfield\":\"\"}}";

            var model = new Invoice
            {
                ClientId = 485054,
                Created = DateTime.Parse("0001-01-01T00:00:00", CultureInfo.InvariantCulture),
                Updated = DateTime.Parse("0001-01-01T00:00:00", CultureInfo.InvariantCulture),
                ContactId = 7722,
                Date = DateTime.Parse("2020-08-06", CultureInfo.InvariantCulture),
                DiscountAmount = 3,
                Quote = 1,
                Status = InvoiceStatus.Draft,
                NetGross = NetGrossType.Net,
                FreeTextId = 243287,
                TemplateId = 421389
            };
            var expectedResult = new Invoice
            {
                Id = 7563765,
                Created = DateTime.Parse("2020-08-06T09:25:37+02:00", CultureInfo.InvariantCulture),
                Updated = DateTime.Parse("2020-08-06T09:25:37+02:00", CultureInfo.InvariantCulture),
                ClientId = 485054,
                ContactId = 7722,
                InvoiceNumber = "RE198",
                NumberPre = "RE",
                NumberLength = 3,
                Title = "[Invoice.invoice_number]",
                Date = DateTime.Parse("2020-08-06", CultureInfo.InvariantCulture),
                DueDate = DateTime.Parse("2020-08-11", CultureInfo.InvariantCulture),
                DueDays = 5,
                Address = "Hallo GmbH\nHerr Ronny Roller\nAcme Str. 12\n12345 München",
                Status = InvoiceStatus.Draft,
                Intro = "Hiermit stellen wir Ihnen die folgenden Positionen in Rechnung.",
                Note = "Bitte überweisen Sie den Rechnungsbetrag bis zum[Invoice.due_date] auf unser Konto.",
                CurrencyCode = "EUR",
                Quote = 1,
                NetGross = NetGrossType.Net,
                DiscountRate = 2,
                DiscountDate = DateTime.Parse("2020-08-13", CultureInfo.InvariantCulture),
                DiscountDays = 7,
                PaymentTypes = new List<string> { "CREDIT_CARD", "DEBIT", "CASH" },
                CustomerPortalUrl = "https://develappersdev.billomat.net/customerportal/invoices/show/c4da1693-51b1-4f81-a05c-1e412b9a9abd",
                Label = null,
                TemplateId = 421389
            };

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.PostAsync(expectedRequestUri, expectedRequestBody, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(responseBody));

            var sut = GetSystemUnderTest(http);

            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            //act
            var result = await sut.CreateAsync(model);

            // assert
            A.CallTo(() => http.PostAsync(expectedRequestUri, expectedRequestBody, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
