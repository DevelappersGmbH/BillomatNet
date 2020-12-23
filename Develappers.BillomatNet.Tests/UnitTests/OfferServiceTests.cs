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
    public class OfferServiceTests : UnitTestBase<OfferService>
    {
        [Fact]
        public async Task GetById_WithInvalidCredentials_ShouldThrowNotAuthorizedException()
        {
            const int id = 485054;
            const string expectedUri = "/api/offers/485054";

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
                "{\"offer\":{\"id\":\"1322705\",\"created\":\"2015-06-04T16:12:59+02:00\",\"updated\":\"2019-08-08T09:21:08+02:00\",\"client_id\":\"485054\",\"contact_id\":\"\",\"offer_number\":\"RE17\",\"number\":\"17\",\"number_pre\":\"RE\",\"number_length\":\"0\",\"title\":\"Rechnung RE17\",\"date\":\"2015-09-04\",\"supply_date\":\"\",\"supply_date_type\":\"SUPPLY_TEXT\",\"due_date\":\"2015-09-18\",\"due_days\":\"14\",\"address\":\"ACME Ltd.\\r\\nJohn Doe\\r\\nSecond Street 123\\r\\n01159 Dresden\",\"status\":\"WON\",\"label\":\"\",\"intro\":\"Hiermit stellen wir Ihnen die folgenden Positionen in Rechnung.\",\"note\":\"Bitte \\u00fcberweisen Sie den Rechnungsbetrag bis zum 18.09.2015 auf unser Konto.\",\"total_net\":\"3885\",\"total_gross\":\"4615.5\",\"reduction\":\"\",\"total_reduction\":\"0\",\"total_net_unreduced\":\"3885\",\"total_gross_unreduced\":\"4615.5\",\"currency_code\":\"EUR\",\"quote\":\"1\",\"net_gross\":\"NET\",\"discount_rate\":\"2\",\"discount_date\":\"2015-09-11\",\"discount_days\":\"7\",\"discount_amount\":\"92.31\",\"paid_amount\":\"4523.19\",\"open_amount\":\"0\",\"payment_types\":\"\",\"customerportal_url\":\"https:\\/\\/develappersdev.billomat.net\\/customerportal\\/invoices\\/show\\/bc3654c0-b822-4aad-894f-8c5b1620241c\",\"taxes\":{\"tax\":[{\"name\":\"ust\",\"rate\":\"19\",\"amount\":\"722\",\"amount_plain\":\"722\",\"amount_rounded\":\"722\",\"amount_net\":\"3800\",\"amount_net_plain\":\"3800\",\"amount_net_rounded\":\"3800\",\"amount_gross\":\"4522\",\"amount_gross_plain\":\"4522\",\"amount_gross_rounded\":\"4522\"},{\"name\":\"gtr\",\"rate\":\"10\",\"amount\":\"8.5\",\"amount_plain\":\"8.5\",\"amount_rounded\":\"8.5\",\"amount_net\":\"85\",\"amount_net_plain\":\"85\",\"amount_net_rounded\":\"85\",\"amount_gross\":\"93.5\",\"amount_gross_plain\":\"93.5\",\"amount_gross_rounded\":\"93.5\"}]},\"invoice_id\":\"\",\"offer_id\":\"\",\"confirmation_id\":\"\",\"recurring_id\":\"\",\"dig_proceeded\":\"0\",\"template_id\":\"\",\"customfield\":\"\"}}";
            const int id = 1322705;
            const string expectedUri = "/api/offers/1322705";

            var expectedResult = new Offer
            {
                Id = id,
                Created = DateTime.Parse("2015-06-04T16:12:59+02:00", CultureInfo.InvariantCulture),
                //Updated = DateTime.Parse("2019-08-08T09:21:08+02:00", CultureInfo.InvariantCulture),
                ClientId = 485054,
                ContactId = null,
                OfferNumber = "RE17",
                Number = 17,
                NumberPre = "RE",
                NumberLength = 0,
                Title = "Rechnung RE17",
                Date = DateTime.Parse("2015-09-04", CultureInfo.InvariantCulture),
                Address = "ACME Ltd.\r\nJohn Doe\r\nSecond Street 123\r\n01159 Dresden",
                Status = OfferStatus.Won,
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
                CustomerPortalUrl = "https://develappersdev.billomat.net/customerportal/invoices/show/bc3654c0-b822-4aad-894f-8c5b1620241c",
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

            result.Should().BeEquivalentUsingComparerTo(expectedResult, new OfferEqualityComparer());
        }
    }
}
