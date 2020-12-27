// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
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
    public class ClientServiceTagsTest : UnitTestBase<ClientService>
    {
        [Fact]
        public async Task GetTagCloud_WithValidData_ShouldReturnCorrectValues()
        {
            const string httpResult =
                "{\"client-tags\":{\"client-tag\":{\"id\":\"188521\",\"name\":\"Testag\",\"count\":\"2\",\"customfield\":\"\"},\"@page\":\"1\",\"@per_page\":\"100\",\"@total\":\"1\"}}";
            const string expectedUri = "/api/client-tags";

            var expectedResult = new List<TagCloudItem>
            {
                new()
                {
                    Id = 188521,
                    Name = "Testag",
                    Count = 2
                }
            };

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(new Uri(expectedUri, UriKind.Relative), null, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(httpResult));

            var sut = GetSystemUnderTest(http);
            var result = await sut.GetTagCloudAsync();

            A.CallTo(() => http.GetAsync(new Uri(expectedUri, UriKind.Relative), null, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            result.TotalItems.Should().Be(1);
            result.ItemsPerPage.Should().Be(100);

            result.List.Should().SatisfyRespectively(first => first.Should().BeEquivalentTo(expectedResult[0]));
        }

        [Fact]
        public async Task GetTagById_WithValidData_ShouldReturnCorrectValues()
        {
            // arrange
            const int id = 188527;
            var expectedRequestUri = new Uri("/api/client-tags/188527", UriKind.Relative);
            var httpResponse = "{\"client-tag\":{\"id\":\"188527\",\"name\":\"Testag\",\"client_id\":\"796650\",\"customfield\":\"\"}}";
            var expected = new ClientTag
            {
                Id = id,
                Name = "Testag",
                ClientId = 796650
            };

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(expectedRequestUri, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(httpResponse));

            var sut = GetSystemUnderTest(http);

            // act and assert
            var result = await sut.GetTagById(id);

            A.CallTo(() => http.GetAsync(expectedRequestUri, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            //result.Should().BeEquivalentTo(expected, new ClientTagEqualityComparer());
        }


        [Fact]
        public async Task GetTagById_WithInvalidInputValues_ShouldThrowArgumentException()
        {
            // arrange
            const int id = 0;

            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            // act and assert
            await Assert.ThrowsAsync<ArgumentException>(() => sut.GetTagById(id));
        }

        [Fact]
        public async Task GetTagById_WithInvalidCredentials_ShouldThrowNotAuthorizedException()
        {
            // arrange
            const int id = 1;
            var expectedRequestUri = new Uri("/api/client-tags/1", UriKind.Relative);

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(expectedRequestUri, A<CancellationToken>.Ignored))
                .ThrowsAsync(ExceptionFactory.CreateNotAuthorizedException);

            var sut = GetSystemUnderTest(http);

            // act and assert
            await Assert.ThrowsAsync<NotAuthorizedException>(() => sut.GetTagById(id));
            A.CallTo(() => http.GetAsync(expectedRequestUri, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetTagById_WithInvalidId_ShouldReturnNull()
        {
            // arrange
            const int id = 1;
            var expectedRequestUri = new Uri("/api/client-tags/1", UriKind.Relative);

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(expectedRequestUri, A<CancellationToken>.Ignored))
                .ThrowsAsync(ExceptionFactory.CreateNotFoundException);

            var sut = GetSystemUnderTest(http);

            // act and assert
            var result = await sut.GetTagById(id);
            A.CallTo(() => http.GetAsync(expectedRequestUri, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetList_WithValidInputParameters_ShouldReturnCorrectValues()
        {
            // arrange
            var expectedRequestUri = new Uri("/api/client-tags", UriKind.Relative);
            const string expectedQueryString = "client_id=796659&per_page=100&page=1";
            const string responseBody = "{\"client-tags\":{\"client-tag\":{\"id\":\"188521\",\"name\":\"A-Kunde\",\"client_id\":\"796659\",\"customfield\":\"\"},\"@page\":\"1\",\"@per_page\":\"100\",\"@total\":\"1\"}}";
            var query = new Query<ClientTag, ClientTagFilter>()
                .AddFilter(x => x.ClientId, 796659);
            var expectedResult = new List<ClientTag>
            {
                new() {Id = 188521, Name = "A-Kunde", ClientId = 796659}
            };

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(expectedRequestUri, expectedQueryString, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(responseBody));

            var sut = GetSystemUnderTest(http);

            // act
            var result = await sut.GetTagListAsync(query);

            // assert
            result.TotalItems.Should().Be(1);
            result.Page.Should().Be(1);
            result.ItemsPerPage.Should().Be(100);
            result.List.Should().SatisfyRespectively(first => first.Should().BeEquivalentTo(expectedResult[0]));
        }
    }
}
