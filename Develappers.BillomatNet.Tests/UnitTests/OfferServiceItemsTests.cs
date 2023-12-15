// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Api.Net;
using Develappers.BillomatNet.Types;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Develappers.BillomatNet.Tests.UnitTests
{
    public class OfferServiceItemsTests : UnitTestBase<OfferService>
    {
        [Fact]
        public async Task GetByIdWithValidData_ShouldReturnWithCorrectValues()
        {
            const string httpResult = "{\"offer-item\":{\"id\":\"6809664\",\"article_id\":\"\",\"offer_id\":\"1376503\",\"position\":\"1\",\"unit\":\"apfel\",\"quantity\":\"1\",\"type\":\"PRODUCT\",\"unit_price\":\"100\",\"tax_name\":\"MwSt\",\"tax_rate\":\"19\",\"title\":\"test1\",\"description\":\"test2\",\"total_net\":\"100\",\"total_gross\":\"119\",\"reduction\":\"\",\"total_net_unreduced\":\"100\",\"total_gross_unreduced\":\"119\",\"optional\":\"0\",\"tax_changed_manually\":\"0\",\"customfield\":\"\"}}";
            const int id = 6809664;
            const string expectedUri = "/api/offer-items/6809664";
            var expectedResult = new OfferItem
            {
                Id = 6809664,
                ArticleId = null,
                OfferId = 1376503,
                Position = 1,
                Unit = "apfel",
                Quantity = 1,
                Type = InvoiceItemType.Product,
                UnitPrice = 100,
                TaxName = "MwSt",
                TaxRate = 19,
                Title = "test1",
                Description = "test2",
                TotalNet = 100,
                TotalGross = 119,
                Reduction = null,
                TotalNetUnreduced = 100,
                TotalGrossUnreduced = 119
            };

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(httpResult));

            var sut = GetSystemUnderTest(http);
            var result = await sut.GetItemByIdAsync(id);

            A.CallTo(() => http.GetAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetByIdWithWrongType_ShouldReturnDeviatingItem()
        {
            const string httpResult = "{\"offer-item\":{\"id\":\"6809664\",\"article_id\":\"\",\"offer_id\":\"1376503\",\"position\":\"1\",\"unit\":\"apfel\",\"quantity\":\"1\",\"type\":\"SERVICE\",\"unit_price\":\"100\",\"tax_name\":\"MwSt\",\"tax_rate\":\"19\",\"title\":\"test1\",\"description\":\"test2\",\"total_net\":\"100\",\"total_gross\":\"119\",\"reduction\":\"\",\"total_net_unreduced\":\"100\",\"total_gross_unreduced\":\"119\",\"optional\":\"0\",\"tax_changed_manually\":\"0\",\"customfield\":\"\"}}";
            const int id = 6809664;
            const string expectedUri = "/api/offer-items/6809664";
            var expectedResult = new OfferItem
            {
                Id = 6809664,
                ArticleId = null,
                OfferId = 1376503,
                Position = 1,
                Unit = "apfel",
                Quantity = 1,
                Type = InvoiceItemType.Product,
                UnitPrice = 100,
                TaxName = "MwSt",
                TaxRate = 19,
                Title = "test1",
                Description = "test2",
                TotalNet = 100,
                TotalGross = 119,
                Reduction = null,
                TotalNetUnreduced = 100,
                TotalGrossUnreduced = 119
            };

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(httpResult));

            var sut = GetSystemUnderTest(http);
            var result = await sut.GetItemByIdAsync(id);

            A.CallTo(() => http.GetAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            result.Should().NotBeEquivalentTo(expectedResult);
        }
    }
}
