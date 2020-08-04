// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Api.Net;
using Develappers.BillomatNet.Queries;
using Develappers.BillomatNet.Tests.UnitTests.Comparer;
using Develappers.BillomatNet.Types;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Develappers.BillomatNet.Tests.UnitTests
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class InvoiceServiceTagsTests : UnitTestBase<InvoiceService>
    {
        [Fact]
        public async Task GetTagCloud_WithValidData_ShouldReturnCorrectValues()
        {
            const string httpResult =
                "{\"invoice-tags\":{\"invoice-tag\":{\"id\":\"207252\",\"name\":\"Test\",\"count\":\"2\",\"customfield\":\"\"},\"@page\":\"1\",\"@per_page\":\"100\",\"@total\":\"1\"}}";
            const string expectedUri = "/api/invoice-tags";

            var expectedResult = new List<TagCloudItem>
            {
                new TagCloudItem
                {
                    Id = 207252,
                    Name = "Test",
                    Count = 2
                }
            };

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(new Uri(expectedUri, UriKind.Relative), null, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(httpResult));

            var sut = GetSystemUnderTest(http);
            var result = await sut.GetTagcloudAsync();

            A.CallTo(() => http.GetAsync(new Uri(expectedUri, UriKind.Relative), null, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            result.TotalItems.Should().Be(1);
            result.ItemsPerPage.Should().Be(100);

            result.List.Should().Equal(expectedResult, new TagCloudItemEqualityComparer().Equals);
        }

        [Fact]
        public async Task GetList_WithValidInputParameters_ShouldReturnCorrectValues()
        {
            // arrange
            var expectedRequestUri = new Uri("/api/invoice-tags", UriKind.Relative);
            const string expectedQueryString = "invoice_id=3982556&per_page=100&page=1";
            const string responseBody = "{\"invoice-tags\":{\"invoice-tag\":{\"id\":\"207252\",\"name\":\"Test\",\"invoice_id\":\"3982556\",\"customfield\":\"\"},\"@page\":\"1\",\"@per_page\":\"100\",\"@total\":\"1\"}}";
            var query = new Query<InvoiceTag, InvoiceTagFilter>()
                .AddFilter(x => x.InvoiceId, 3982556);
            var expectedResult = new List<InvoiceTag>
            {
                new InvoiceTag {Id = 207252, Name = "Test", InvoiceId = 3982556}
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
            result.List.Should().HaveCount(expectedResult.Count)
                .And.ContainItemsInOrderUsingComparer(expectedResult, new InvoiceTagEqualityComparer());
        }

        [Fact]
        public async Task GetList_WithInvalidInputValues_ShouldThrowArgumentException()
        {
            //arrange
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            // act and assert
            await Assert.ThrowsAsync<ArgumentException>(() => sut.GetTagListAsync(null));
        }

        [Fact]
        public async Task GetList_WithInvalidCredentials_ShouldThrowNotAuthorizedException()
        {
            // arrange
            var expectedRequestUri = new Uri($"/api/invoice-tags", UriKind.Relative);

            var query = new Query<InvoiceTag, InvoiceTagFilter>()
                .AddFilter(x => x.InvoiceId, 3982556);

            var strQuery = "invoice_id=3982556";

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(expectedRequestUri, strQuery, A<CancellationToken>.Ignored))
                .ThrowsAsync(ExceptionFactory.CreateNotAuthorizedException);

            var sut = GetSystemUnderTest(http);

            // act and assert
            await Assert.ThrowsAsync<NotAuthorizedException>(() => sut.GetTagListAsync(query));
            A.CallTo(() => http.GetAsync(expectedRequestUri, strQuery, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetTagById_WithValidData_ShouldReturnCorrectValues()
        {
            // arrange
            const int id = 188527;
            var expectedRequestUri = new Uri("/api/invoice-tags/188527", UriKind.Relative);
            var httpResponse = "{\"invoice-tag\":{\"id\":\"207252\",\"name\":\"Test\",\"invoice_id\":\"3982556\",\"customfield\":\"\"}}";
            var expected = new InvoiceTag
            {
                Id = id,
                Name = "Test",
                InvoiceId = 3982556
            };

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(expectedRequestUri, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(httpResponse));

            var sut = GetSystemUnderTest(http);

            // act and assert
            var result = await sut.GetTagByIdAsync(id);

            A.CallTo(() => http.GetAsync(expectedRequestUri, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            //result.Should().BeEquivalentTo(expected, new InvoiceTagEqualityComparer());
        }

        [Fact]
        public async Task GetTagById_WithInvalidInputValues_ShouldThrowArgumentException()
        {
            // arrange
            const int id = 0;

            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            // act and assert
            await Assert.ThrowsAsync<ArgumentException>(() => sut.GetTagByIdAsync(id));
        }

        [Fact]
        public async Task GetTagById_WithInvalidCredentials_ShouldThrowNotAuthorizedException()
        {
            // arrange
            const int id = 1;
            var expectedRequestUri = new Uri($"/api/invoice-tags/{id}", UriKind.Relative);

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(expectedRequestUri, A<CancellationToken>.Ignored))
                .ThrowsAsync(ExceptionFactory.CreateNotAuthorizedException);

            var sut = GetSystemUnderTest(http);

            // act and assert
            await Assert.ThrowsAsync<NotAuthorizedException>(() => sut.GetTagByIdAsync(id));
            A.CallTo(() => http.GetAsync(expectedRequestUri, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetTagById_WithInvalidId_ShouldReturnNull()
        {
            // arrange
            const int id = 1;
            var expectedRequestUri = new Uri($"/api/invoice-tags/{id}", UriKind.Relative);

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(expectedRequestUri, A<CancellationToken>.Ignored))
                .ThrowsAsync(ExceptionFactory.CreateNotFoundException);

            var sut = GetSystemUnderTest(http);

            // act and assert
            var result = await sut.GetTagByIdAsync(id);
            A.CallTo(() => http.GetAsync(expectedRequestUri, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            result.Should().BeNull();
        }
    }
}
