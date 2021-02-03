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
                new()
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
            var result = await sut.GetTagCloudAsync();

            A.CallTo(() => http.GetAsync(new Uri(expectedUri, UriKind.Relative), null, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            result.TotalItems.Should().Be(1);
            result.ItemsPerPage.Should().Be(100);

            result.List.Should().SatisfyRespectively(first => first.Should().BeEquivalentTo(expectedResult[0]));
        }

        [Fact]
        public async Task GetTagCloudList_WithInvalidCredentials_ShouldThrowNotAuthorizedException()
        {
            // arrange
            var expectedRequestUri = new Uri("/api/invoice-tags", UriKind.Relative);
            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(expectedRequestUri, null, A<CancellationToken>.Ignored))
                .ThrowsAsync(ExceptionFactory.CreateNotAuthorizedException);

            var sut = GetSystemUnderTest(http);

            //act and assert
            await Assert.ThrowsAsync<NotAuthorizedException>(() => sut.GetTagCloudAsync());
            A.CallTo(() => http.GetAsync(expectedRequestUri, null, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetList_WithValidData_ShouldReturnCorrectValues()
        {
            // arrange
            var expectedRequestUri = new Uri("/api/invoice-tags", UriKind.Relative);
            const string expectedQueryString = "invoice_id=3982556&per_page=100&page=1";
            const string responseBody = "{\"invoice-tags\":{\"invoice-tag\":{\"id\":\"207252\",\"name\":\"Test\",\"invoice_id\":\"3982556\",\"customfield\":\"\"},\"@page\":\"1\",\"@per_page\":\"100\",\"@total\":\"1\"}}";
            var query = new Query<InvoiceTag, InvoiceTagFilter>()
                .AddFilter(x => x.InvoiceId, 3982556);
            var expectedResult = new List<InvoiceTag>
            {
                new() {Id = 207252, Name = "Test", InvoiceId = 3982556}
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

            var strQuery = "invoice_id=3982556&per_page=100&page=1";

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

        [Fact]
        public async Task Create_WithCorrectValues_ShouldCreateCommentAndReturnCorrectValues()
        {
            //arrange
            var expectedRequestUri = new Uri("/api/invoice-tags", UriKind.Relative);
            const string expectedRequestBody = "{\"invoice-tag\":{\"id\":\"0\",\"invoice_id\":\"3982556\",\"name\":\"Test\"}}";
            const string responseBody = "{\"invoice-tag\":{\"id\":\"872254\",\"invoice_id\":\"3982556\",\"name\":\"Test\"}}";

            var model = new InvoiceTag { InvoiceId = 3982556, Name = "Test" };
            var expectedResult = new InvoiceTag { Id = 872254, InvoiceId = 3982556, Name = "Test" };

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.PostAsync(expectedRequestUri, expectedRequestBody, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(responseBody));

            var sut = GetSystemUnderTest(http);

            //act
            var result = await sut.CreateTagAsync(model);

            // assert
            A.CallTo(() => http.PostAsync(expectedRequestUri, expectedRequestBody, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task Create_WithInvalidInputValue_ShouldThrowArgumentException()
        {
            // arrange
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            // act and assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => sut.CreateTagAsync(null));
            await Assert.ThrowsAsync<ArgumentException>(() => sut.CreateTagAsync(new InvoiceTag()));
            await Assert.ThrowsAsync<ArgumentException>(() => sut.CreateTagAsync(new InvoiceTag { Id = 1 }));
        }

        [Fact]
        public async Task Create_WithInvalidCredentials_ShouldThrowNotAuthorizedException()
        {
            //arrange
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            var expectedRequestUri = new Uri("/api/invoice-tags", UriKind.Relative);
            A.CallTo(() => http.PostAsync(expectedRequestUri, A<string>.Ignored, A<CancellationToken>.Ignored))
                .ThrowsAsync(ExceptionFactory.CreateNotAuthorizedException);

            var model = new InvoiceTag { InvoiceId = 1, Name = "Test" };

            await Assert.ThrowsAsync<NotAuthorizedException>(() => sut.CreateTagAsync(model));
            A.CallTo(() => http.PostAsync(expectedRequestUri, A<string>.Ignored, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Delete_WithCorrectParameters_ShouldSucceed()
        {
            const int id = 8;
            const string expectedUri = "/api/invoice-tags/8";


            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.DeleteAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(string.Empty));

            var sut = GetSystemUnderTest(http);

            await sut.DeleteTagAsync(id);

            A.CallTo(() => http.DeleteAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Delete_WithInvalidCredentials_ShouldThrowNotAuthorizedException()
        {
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            const string expectedUri = "/api/invoice-tags/1";
            const int id = 1;
            A.CallTo(() => http.DeleteAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .ThrowsAsync(ExceptionFactory.CreateNotAuthorizedException);

            await Assert.ThrowsAsync<NotAuthorizedException>(() => sut.DeleteTagAsync(id));
            A.CallTo(() => http.DeleteAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Delete_WithInvalidId_ShouldThrowNotFoundException()
        {
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            const string expectedUri = "/api/invoice-tags/1";
            const int id = 1;
            A.CallTo(() => http.DeleteAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .ThrowsAsync(ExceptionFactory.CreateNotFoundException);

            await Assert.ThrowsAsync<NotFoundException>(() => sut.DeleteTagAsync(id));
            A.CallTo(() => http.DeleteAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Delete_WithInvalidInputValue_ShouldThrowArgumentException()
        {
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            await Assert.ThrowsAsync<ArgumentException>(() => sut.DeleteTagAsync(0));
        }
    }
}
