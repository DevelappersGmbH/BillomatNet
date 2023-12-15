// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Threading.Tasks;
using Develappers.BillomatNet.Types;
using FluentAssertions;
using Xunit;

namespace Develappers.BillomatNet.Tests.IntegrationTests
{
    public class OfferServiceItemsIntegrationTests : IntegrationTestBase<OfferService>
    {
        private static BillomatClient _client;

        public OfferServiceItemsIntegrationTests() : base(c =>
        {
            _client = new BillomatClient(c);
            return _client.Offers;
        })
        {
        }

        [Fact]
        public async Task GetOfferItemById()
        {
            var result = await SystemUnderTest.GetItemByIdAsync(6809664);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetItemListOfOffer()
        {
            var result = await SystemUnderTest.GetItemsAsync(1376503);

            var expectedResults = new List<OfferItem>
            {
                new()
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
                },
                new()
                {
                    Id = 6809666,
                    ArticleId = 154124,
                    OfferId = 1376503,
                    Position = 2,
                    Unit = "Beutel",
                    Quantity = 1,
                    Type = InvoiceItemType.Service,
                    UnitPrice = 2,
                    TaxName = "MwSt",
                    TaxRate = 19,
                    Title = "Net Purchase",
                    Description = "Uhb",
                    TotalNet = 2,
                    TotalGross = 2.38f,
                    Reduction = null,
                    TotalNetUnreduced = 2,
                    TotalGrossUnreduced = 2.38f
                }
            };

            result.TotalItems.Should().Be(2);
            result.List.Should().SatisfyRespectively(
                first => first.Should().BeEquivalentTo(expectedResults[0]),
                second => second.Should().BeEquivalentTo(expectedResults[1]));
        }
    }
}
