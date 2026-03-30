// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Api.Net;
using Develappers.BillomatNet.Queries;
using Develappers.BillomatNet.Types;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Develappers.BillomatNet.Tests.UnitTests
{
    public class FreeTextServiceTests : UnitTestBase<FreeTextService>
    {

        [Fact]
        public async Task GetList_ShouldReturnCorrectValues()
        {
            // arrange
            var expectedRequestUri = new Uri("/GET /api/free-texts", UriKind.Relative);
            const string expectedRequestQuery = "per_page=10&page=0";
            const string responseBody = "";
            var expectedResult = new List<FreeText>
            {
                new()
                {
                    Id = 203719,
                    FreeTextType=FreeTextType.Invoice,
                    Title="Invoice 1",
                    Intro="We hereby provide you with the following items into account.",
                    Note="Please transfer the invoice amount to 1.7.2023 to our account",
                    IsDefault=0
                }
            };

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(expectedRequestUri, expectedRequestQuery, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(responseBody));

            var sut = GetSystemUnderTest(http);

            // act
            var result = await sut.GetListAsync(new Query<FreeText, FreeTextFilter>().SetItemsPerPage(10).SetPage(0));

            // assert
            A.CallTo(() => http.GetAsync(expectedRequestUri, expectedRequestQuery, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            result.TotalItems.Should().Be(32);
            result.ItemsPerPage.Should().Be(10);
            result.Page.Should().Be(2);
            result.List.Should().SatisfyRespectively(
                first => first.Should().BeEquivalentTo(expectedResult[0]),
                second => second.Should().BeEquivalentTo(expectedResult[1]),
                third => third.Should().BeEquivalentTo(expectedResult[2]));
        }
    }
}
