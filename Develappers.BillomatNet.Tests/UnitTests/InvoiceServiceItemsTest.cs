// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
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
    public class InvoiceServiceItemsTest : UnitTestBase<InvoiceService>
    {
        [Fact]
        public async Task Create_WithCorrectValues_ShouldCreateItemAndReturnCorrectValues()
        {
            //arrange
            var expectedRequestUri = new Uri("/api/invoice-items", UriKind.Relative);
            const string expectedRequestBody = "{\"invoice-item\":{\"id\":\"0\",\"article_id\":\"154123\",\"invoice_id\":\"8332909\",\"position\":\"1\",\"quantity\":\"3.54\",\"title\":\"Test-Item\",\"description\":\"Ganz viel toller Text\",\"reduction\":\"0\"}}";
            const string responseBody = "{\"invoice-item\":{\"id\":\"20643891\",\"article_id\":\"154123\",\"invoice_id\":\"8332909\",\"position\":\"1\",\"unit\":\"Stück\",\"quantity\":\"3.54\",\"type\":\"\",\"unit_price\":\"2\",\"tax_name\":\"MwSt\",\"tax_rate\":\"19\",\"title\":\"Test-Item\",\"description\":\"Ganz viel toller Text\",\"total_net\":\"7.08\",\"total_gross\":\"8.4252\",\"reduction\":\"\",\"total_net_unreduced\":\"7.0778\",\"total_gross_unreduced\":\"8.4226\",\"customfield\":\"\"}}";

            var model = new InvoiceItem
            {
                ArticleId = 154123,
                InvoiceId = 8332909,
                Position = 1,
                Quantity = 3.5389F,
                Title = "Test-Item",
                Description = "Ganz viel toller Text"
            };
            var expectedResult = new InvoiceItem
            {
                Id = 20643891,
                ArticleId = 154123,
                InvoiceId = 8332909,
                Position = 1,
                Unit = "Stück",
                Quantity = 3.54F,
                UnitPrice = 2,
                TaxName = "MwSt",
                TaxRate = 19,
                Title = "Test-Item",
                Description = "Ganz viel toller Text",
                TotalNet = 7.08F,
                TotalGross = 8.4252F,
                Reduction = null,
                TotalNetUnreduced = 7.0778F,
                TotalGrossUnreduced = 8.4226F
            };

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.PostAsync(expectedRequestUri, expectedRequestBody, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(responseBody));

            var sut = GetSystemUnderTest(http);

            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            //act
            var result = await sut.CreateItemAsync(model);

            // assert
            A.CallTo(() => http.PostAsync(expectedRequestUri, expectedRequestBody, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            result.Should().BeEquivalentUsingComparerTo(expectedResult, new InvoiceItemEqualityComparer());
        }
    }
}
