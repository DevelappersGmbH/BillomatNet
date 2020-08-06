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
using Develappers.BillomatNet.Tests.UnitTests.Comparer;
using Develappers.BillomatNet.Types;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Develappers.BillomatNet.Tests.UnitTests
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class InvoiceServiceTests : UnitTestBase<InvoiceService>
    {
        [Fact]
        public async Task GetById_WithInvalidCredentials_ShouldThrowNotAuthorizedException()
        {
            const int id = 485054;
            const string expectedUri = "/api/invoices/485054";

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .ThrowsAsync(ExceptionFactory.CreateNotAuthorizedException);

            var sut = GetSystemUnderTest(http);
            await Assert.ThrowsAsync<NotAuthorizedException>(() => sut.GetByIdAsync(id));

            A.CallTo(() => http.GetAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();
        }


        [Fact]
        public async Task GetById_WithValidData_ShouldReturnCorrectValues()
        {
            const string httpResult =
                "{\"invoice\":{\"id\":\"1322705\",\"created\":\"2015-06-04T16:12:59+02:00\",\"updated\":\"2019-08-08T09:21:08+02:00\",\"client_id\":\"485054\",\"contact_id\":\"\",\"invoice_number\":\"RE17\",\"number\":\"17\",\"number_pre\":\"RE\",\"number_length\":\"0\",\"title\":\"Rechnung RE17\",\"date\":\"2015-09-04\",\"supply_date\":\"\",\"supply_date_type\":\"SUPPLY_TEXT\",\"due_date\":\"2015-09-18\",\"due_days\":\"14\",\"address\":\"ACME Ltd.\\r\\nJohn Doe\\r\\nSecond Street 123\\r\\n01159 Dresden\",\"status\":\"PAID\",\"label\":\"\",\"intro\":\"Hiermit stellen wir Ihnen die folgenden Positionen in Rechnung.\",\"note\":\"Bitte \\u00fcberweisen Sie den Rechnungsbetrag bis zum 18.09.2015 auf unser Konto.\",\"total_net\":\"3885\",\"total_gross\":\"4615.5\",\"reduction\":\"\",\"total_reduction\":\"0\",\"total_net_unreduced\":\"3885\",\"total_gross_unreduced\":\"4615.5\",\"currency_code\":\"EUR\",\"quote\":\"1\",\"net_gross\":\"NET\",\"discount_rate\":\"2\",\"discount_date\":\"2015-09-11\",\"discount_days\":\"7\",\"discount_amount\":\"92.31\",\"paid_amount\":\"4523.19\",\"open_amount\":\"0\",\"payment_types\":\"\",\"customerportal_url\":\"https:\\/\\/develappersdev.billomat.net\\/customerportal\\/invoices\\/show\\/bc3654c0-b822-4aad-894f-8c5b1620241c\",\"taxes\":{\"tax\":[{\"name\":\"ust\",\"rate\":\"19\",\"amount\":\"722\",\"amount_plain\":\"722\",\"amount_rounded\":\"722\",\"amount_net\":\"3800\",\"amount_net_plain\":\"3800\",\"amount_net_rounded\":\"3800\",\"amount_gross\":\"4522\",\"amount_gross_plain\":\"4522\",\"amount_gross_rounded\":\"4522\"},{\"name\":\"gtr\",\"rate\":\"10\",\"amount\":\"8.5\",\"amount_plain\":\"8.5\",\"amount_rounded\":\"8.5\",\"amount_net\":\"85\",\"amount_net_plain\":\"85\",\"amount_net_rounded\":\"85\",\"amount_gross\":\"93.5\",\"amount_gross_plain\":\"93.5\",\"amount_gross_rounded\":\"93.5\"}]},\"invoice_id\":\"\",\"offer_id\":\"\",\"confirmation_id\":\"\",\"recurring_id\":\"\",\"dig_proceeded\":\"0\",\"template_id\":\"\",\"customfield\":\"\"}}";
            const int id = 1322705;
            const string expectedUri = "/api/invoices/1322705";

            var expectedResult = new Invoice
            {
                Id = id,
                Created = DateTime.Parse("2015-06-04T16:12:59+02:00", CultureInfo.InvariantCulture),
                //Updated = DateTime.Parse("2019-08-08T09:21:08+02:00", CultureInfo.InvariantCulture),
                ClientId = 485054,
                ContactId = null,
                InvoiceNumber = "RE17",
                Number = 17,
                NumberPre = "RE",
                NumberLength = 0,
                Title = "Rechnung RE17",
                Date = DateTime.Parse("2015-09-04", CultureInfo.InvariantCulture),
                SupplyDateType = SupplyDateType.SupplyDate,
                SupplyDate = new FreeTextSupplyDate(),
                DueDate = DateTime.Parse("2015-09-18", CultureInfo.InvariantCulture),
                DueDays = 14,
                Address = "ACME Ltd.\r\nJohn Doe\r\nSecond Street 123\r\n01159 Dresden",
                Status = InvoiceStatus.Paid,
                Label = string.Empty,
                Intro = "Hiermit stellen wir Ihnen die folgenden Positionen in Rechnung.",
                Note = "Bitte \u00fcberweisen Sie den Rechnungsbetrag bis zum 18.09.2015 auf unser Konto.",
                TotalNet = 3885f,
                TotalGross = 4615.5f,
                Reduction = null,
                //TotalReduction = 0,
                TotalNetUnreduced = 3885,
                TotalGrossUnreduced = 4615.5f,
                CurrencyCode = "EUR",
                Quote = 1f,
                NetGross = NetGrossType.Net,
                DiscountRate = 2f,
                DiscountDate = DateTime.Parse("2015-09-11", CultureInfo.InvariantCulture),
                DiscountDays = 7,
                DiscountAmount = 92.31f,
                PaidAmount = 4523.19f,
                OpenAmount = 0f,
                PaymentTypes = null,
                CustomerPortalUrl = "https://develappersdev.billomat.net/customerportal/invoices/show/bc3654c0-b822-4aad-894f-8c5b1620241c",
                Taxes = new List<InvoiceTax>
                {
                    new InvoiceTax {Name = "ust", Rate = 19f, Amount = 722f, AmountPlain = 722f, AmountRounded = 722f, AmountNet = 3800f, AmountNetPlain = 3800f, AmountNetRounded = 3800f, AmountGross = 4522f, AmountGrossPlain = 4522f, AmountGrossRounded = 4522f},
                    new InvoiceTax {Name = "gtr", Rate = 10f, Amount = 8.5f, AmountPlain = 8.5f, AmountRounded = 8.5f, AmountNet = 85f, AmountNetPlain = 85f, AmountNetRounded = 85f, AmountGross = 93.5f, AmountGrossPlain = 93.5f, AmountGrossRounded = 93.5f}
                },
                InvoiceId = null,
                OfferId = null,
                ConfirmationId = null,
                RecurringId = null,
                //digproceeded
                // CustomField
                TemplateId = null
            };


            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(httpResult));

            var sut = GetSystemUnderTest(http);
            var result = await sut.GetByIdAsync(id);

            A.CallTo(() => http.GetAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            result.Should().BeEquivalentUsingComparerTo(expectedResult, new InvoiceEqualityComparer());
        }

        [Fact]
        public async Task Create_WithCorrectValues_ShouldCreateCommentAndReturnCorrectValues()
        {
            //arrange
            var expectedRequestUri = new Uri("/api/invoices", UriKind.Relative);
            const string expectedRequestBody = "{\"invoice\":{\"id\":\"0\",\"created\":\"0001-01-01T00:00:00.0000000\",\"updated\":\"0001-01-01T00:00:00.0000000\",\"client_id\":\"485054\",\"contact_id\":\"7722\",\"invoice_number\":null,\"number\":\"\",\"number_pre\":null,\"number_length\":\"0\",\"title\":\"Title\",\"date\":\"2020-08-06\",\"supply_date\":\"\",\"supply_date_type\":\"\",\"due_date\":\"0001-01-01\",\"due_days\":\"0\",\"address\":null,\"status\":\"DRAFT\",\"label\":null,\"intro\":null,\"note\":null,\"total_net\":\"0\",\"total_gross\":\"0\",\"reduction\":\"\",\"total_reduction\":\"0\",\"total_net_unreduced\":\"0\",\"total_gross_unreduced\":\"0\",\"currency_code\":null,\"quote\":\"1\",\"net_gross\":\"NET\",\"discount_rate\":\"0\",\"discount_date\":null,\"discount_days\":\"0\",\"discount_amount\":\"3\",\"paid_amount\":\"0\",\"open_amount\":\"0\",\"payment_types\":\"\",\"customerportal_url\":null,\"invoice_id\":\"\",\"offer_id\":\"\",\"confirmation_id\":\"\",\"recurring_id\":\"\",\"template_id\":\"\"}}";
            const string responseBody = "{\"invoice\":{\"id\":\"7563765\",\"created\":\"2020-08-06T09:25:37+02:00\",\"updated\":\"2020-08-06T09:25:37+02:00\",\"client_id\":\"485054\",\"contact_id\":\"7722\",\"invoice_number\":\"RE198\",\"number\":\"\",\"number_pre\":\"RE\",\"number_length\":\"3\",\"title\":\"Title\",\"date\":\"2020-08-06\",\"supply_date\":\"\",\"supply_date_type\":\"\",\"due_date\":\"2020-08-11\",\"due_days\":\"5\",\"address\":\"Hallo GmbH\nHerr Ronny Roller\nAcme Str. 12\n12345 M\u00fcnchen\",\"status\":\"DRAFT\",\"label\":\"\",\"intro\":\"Hiermit stellen wir Ihnen die folgenden Positionen in Rechnung.\",\"note\":\"Bitte \u00fcberweisen Sie den Rechnungsbetrag bis zum[Invoice.due_date] auf unser Konto.\",\"total_net\":\"0\",\"total_gross\":\"0\",\"reduction\":\"\",\"total_reduction\":\"0\",\"total_net_unreduced\":\"0\",\"total_gross_unreduced\":\"0\",\"currency_code\":\"EUR\",\"quote\":\"1\",\"net_gross\":\"NET\",\"discount_rate\":\"2\",\"discount_date\":\"2020-08-13\",\"discount_days\":\"7\",\"discount_amount\":\"0\",\"paid_amount\":\"0\",\"open_amount\":\"0\",\"payment_types\":\"CREDIT_CARD,DEBIT,CASH\",\"customerportal_url\":\"https:\\/\\/develappersdev.billomat.net\\/customerportal\\/invoices\\/show\\/c4da1693-51b1-4f81-a05c-1e412b9a9abd\",\"invoice_id\":\"\",\"offer_id\":\"\",\"confirmation_id\":\"\",\"recurring_id\":\"\",\"dig_proceeded\":\"0\",\"template_id\":\"\",\"customfield\":\"\"}}";

            var model = new Invoice
            {
                ClientId = 485054,
                Created = DateTime.Parse("0001-01-01T00:00:00", CultureInfo.InvariantCulture),
                Updated = DateTime.Parse("0001-01-01T00:00:00", CultureInfo.InvariantCulture),
                ContactId = 7722,
                Title = "Title",
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
                ClientId = 485054,
                ContactId = 7722,
                InvoiceNumber = "RE198",
                NumberPre = "RE",
                NumberLength = 3,
                Title = "Title",
                Date = DateTime.Parse("2020-08-06", CultureInfo.InvariantCulture),
                DueDate = DateTime.Parse("2020-08-11", CultureInfo.InvariantCulture),
                DueDays = 5,
                Address = "Hallo GmbH\nHerr Ronny Roller\nAcme Str. 12\n12345 München",
                Status = InvoiceStatus.Draft,
                Intro = "Hiermit stellen wir Ihnen die folgenden Positionen in Rechnung.",
                Note = "Bitte überweisen Sie den Rechnungsbetrag bis zum [Invoice.due_date] auf unser Konto.",
                CurrencyCode = "EUR",
                Quote = 1,
                NetGross = NetGrossType.Net,
                DiscountRate = 2,
                DiscountDate = DateTime.Parse("2020-08-13", CultureInfo.InvariantCulture),
                DiscountDays = 7,
                PaymentTypes = new List<string> { "CREDIT_CARD", "DEBIT", "CASH" },
                CustomerPortalUrl = "https:\\/\\/develappersdev.billomat.net\\/customerportal\\/invoices\\/show\\/c4da1693-51b1-4f81-a05c-1e412b9a9abd",
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

            result.Should().BeEquivalentUsingComparerTo(expectedResult, new InvoiceEqualityComparer());
        }
    }
}
