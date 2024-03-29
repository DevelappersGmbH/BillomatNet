﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class InvoiceServiceCommentsTests : UnitTestBase<InvoiceService>
    {
        [Fact]
        public async Task GetList_ShouldReturnCorrectValues()
        {
            //arrange
            var expectedRequestUri = new Uri("/api/invoice-comments", UriKind.Relative);
            const string responseBody = "{\"invoice-comments\":{\"invoice-comment\":[{\"id\":\"4662801\",\"created\":\"2015-06-04T10:04:54+02:00\",\"comment\":\"Rechnung erstellt.\",\"actionkey\":\"CREATE\",\"public\":\"0\",\"by_client\":\"0\",\"user_id\":\"52821\",\"email_id\":\"\",\"client_id\":\"\",\"invoice_id\":\"1322225\",\"customfield\":\"\"},{\"id\":\"4662804\",\"created\":\"2015-06-04T10:05:08+02:00\",\"comment\":\"Status geändert von Entwurf nach offen.\",\"actionkey\":\"STATUS\",\"public\":\"0\",\"by_client\":\"0\",\"user_id\":\"52821\",\"email_id\":\"\",\"client_id\":\"\",\"invoice_id\":\"1322225\",\"customfield\":\"\"},{\"id\":\"4662810\",\"created\":\"2015-06-04T10:05:37+02:00\",\"comment\":\"Zahlung über 212,33 EUR entgegengenommen. Aktueller Status: bezahlt.\",\"actionkey\":\"PAYMENT\",\"public\":\"0\",\"by_client\":\"0\",\"user_id\":\"52821\",\"email_id\":\"\",\"client_id\":\"\",\"invoice_id\":\"1322225\",\"customfield\":\"\"}],\"@page\":\"1\",\"@per_page\":\"100\",\"@total\":\"3\"}}";
            var expectedResult = new List<InvoiceComment>
            {
                new()
                {
                    Id = 4662801,
                    Created = DateTime.Parse("2015-06-04T10:04:54+02:00", CultureInfo.InvariantCulture),
                    Comment = "Rechnung erstellt.",
                    ActionKey = CommentType.Create,
                    Public = false,
                    ByClient = false,
                    UserId = 52821,
                    EmailId = null,
                    ClientId = null,
                    InvoiceId = 1322225
                },
                new()
                {
                    Id = 4662804,
                    Created = DateTime.Parse("2015-06-04T10:05:08+02:00", CultureInfo.InvariantCulture),
                    Comment = "Status geändert von Entwurf nach offen.",
                    ActionKey = CommentType.Status,
                    Public = false,
                    ByClient = false,
                    UserId = 52821,
                    EmailId = null,
                    ClientId = null,
                    InvoiceId = 1322225
                },
                new()
                {
                    Id = 4662810,
                    Created = DateTime.Parse("2015-06-04T10:05:37+02:00", CultureInfo.InvariantCulture),
                    Comment = "Zahlung über 212,33 EUR entgegengenommen. Aktueller Status: bezahlt.",
                    ActionKey = CommentType.Payment,
                    Public = false,
                    ByClient = false,
                    UserId = 52821,
                    EmailId = null,
                    ClientId = null,
                    InvoiceId = 1322225
                },
            };

            const string expectedQuery = "invoice_id=1322225&per_page=100&page=1";

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(expectedRequestUri, expectedQuery, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(responseBody));

            var sut = GetSystemUnderTest(http);
            var query = new Query<InvoiceComment, InvoiceCommentFilter>().AddFilter(x => x.InvoiceId, 1322225);

            //act
            var result = await sut.GetCommentListAsync(query);

            //assert
            A.CallTo(() => http.GetAsync(expectedRequestUri, expectedQuery, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            result.List.Should().SatisfyRespectively(first => first.Should().BeEquivalentTo(expectedResult[0]),
                    second => second.Should().BeEquivalentTo(expectedResult[1]),
                    third => third.Should().BeEquivalentTo(expectedResult[2]));
        }

        [Fact]
        public async Task GetList_WithEnumQuery_ShouldReturnCorrectValues()
        {
            //arrange
            var expectedRequestUri = new Uri("/api/invoice-comments", UriKind.Relative);
            const string responseBody = "{\"invoice-comments\":{\"invoice-comment\":[{\"id\":\"4662801\",\"created\":\"2015-06-04T10:04:54+02:00\",\"comment\":\"Rechnung erstellt.\",\"actionkey\":\"CREATE\",\"public\":\"0\",\"by_client\":\"0\",\"user_id\":\"52821\",\"email_id\":\"\",\"client_id\":\"\",\"invoice_id\":\"1322225\",\"customfield\":\"\"},{\"id\":\"4662804\",\"created\":\"2015-06-04T10:05:08+02:00\",\"comment\":\"Status geändert von Entwurf nach offen.\",\"actionkey\":\"STATUS\",\"public\":\"0\",\"by_client\":\"0\",\"user_id\":\"52821\",\"email_id\":\"\",\"client_id\":\"\",\"invoice_id\":\"1322225\",\"customfield\":\"\"},{\"id\":\"4662810\",\"created\":\"2015-06-04T10:05:37+02:00\",\"comment\":\"Zahlung über 212,33 EUR entgegengenommen. Aktueller Status: bezahlt.\",\"actionkey\":\"PAYMENT\",\"public\":\"0\",\"by_client\":\"0\",\"user_id\":\"52821\",\"email_id\":\"\",\"client_id\":\"\",\"invoice_id\":\"1322225\",\"customfield\":\"\"}],\"@page\":\"1\",\"@per_page\":\"100\",\"@total\":\"3\"}}";
            var expectedResult = new List<InvoiceComment>
            {
                new()
                {
                    Id = 4662801,
                    Created = DateTime.Parse("2015-06-04T10:04:54+02:00", CultureInfo.InvariantCulture),
                    Comment = "Rechnung erstellt.",
                    ActionKey = CommentType.Create,
                    Public = false,
                    ByClient = false,
                    UserId = 52821,
                    EmailId = null,
                    ClientId = null,
                    InvoiceId = 1322225
                },
                new()
                {
                    Id = 4662804,
                    Created = DateTime.Parse("2015-06-04T10:05:08+02:00", CultureInfo.InvariantCulture),
                    Comment = "Status geändert von Entwurf nach offen.",
                    ActionKey = CommentType.Status,
                    Public = false,
                    ByClient = false,
                    UserId = 52821,
                    EmailId = null,
                    ClientId = null,
                    InvoiceId = 1322225
                },
                new()
                {
                    Id = 4662810,
                    Created = DateTime.Parse("2015-06-04T10:05:37+02:00", CultureInfo.InvariantCulture),
                    Comment = "Zahlung über 212,33 EUR entgegengenommen. Aktueller Status: bezahlt.",
                    ActionKey = CommentType.Payment,
                    Public = false,
                    ByClient = false,
                    UserId = 52821,
                    EmailId = null,
                    ClientId = null,
                    InvoiceId = 1322225
                },
            };

            const string expectedQuery = "invoice_id=1322225&actionkey=CREATE,STATUS,PAYMENT&per_page=100&page=1";

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(expectedRequestUri, expectedQuery, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(responseBody));

            var sut = GetSystemUnderTest(http);
            var query = new Query<InvoiceComment, InvoiceCommentFilter>()
                .AddFilter(x => x.InvoiceId, 1322225)
                .AddFilter(x => x.ActionKey, new List<CommentType> { CommentType.Create, CommentType.Status, CommentType.Payment });

            //act
            var result = await sut.GetCommentListAsync(query);

            //assert
            A.CallTo(() => http.GetAsync(expectedRequestUri, expectedQuery, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            result.List.Should().SatisfyRespectively(first => first.Should().BeEquivalentTo(expectedResult[0]),
                second => second.Should().BeEquivalentTo(expectedResult[1]),
                third => third.Should().BeEquivalentTo(expectedResult[2]));
        }

        [Fact]
        public async Task GetList_WithInvalidInputValue_ShouldThrowArgumentException()
        {
            //arrange
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            //act and assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => sut.GetCommentListAsync(null));
            await Assert.ThrowsAsync<ArgumentException>(() => sut.GetCommentListAsync(new Query<InvoiceComment, InvoiceCommentFilter>().AddFilter(x => x.InvoiceId, 0)));
            await Assert.ThrowsAsync<ArgumentException>(() => sut.GetCommentListAsync(new Query<InvoiceComment, InvoiceCommentFilter>()));
        }

        [Fact]
        public async Task GetList_WithInvalidCredentials_ShouldThrowNotAuthorizedException()
        {
            // arrange
            var query = new Query<InvoiceComment, InvoiceCommentFilter>().AddFilter(x => x.InvoiceId, 1322225);
            const string expectedQuery = "invoice_id=1322225&per_page=100&page=1";
            var expectedRequestUri = new Uri("/api/invoice-comments", UriKind.Relative);
            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(expectedRequestUri, A<string>.Ignored, A<CancellationToken>.Ignored))
                .ThrowsAsync(ExceptionFactory.CreateNotAuthorizedException);

            var sut = GetSystemUnderTest(http);
            await Assert.ThrowsAsync<NotAuthorizedException>(() => sut.GetCommentListAsync(query));

            A.CallTo(() => http.GetAsync(expectedRequestUri, expectedQuery, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetList_WithNonMatchingFilter_ShouldReturnEmptyList()
        {
            //arrange
            var expectedRequestUri = new Uri("/api/invoice-comments", UriKind.Relative);
            const string responseBody = "{\"invoice-comments\":{\"@page\":\"1\",\"@per_page\":\"100\",\"@total\":\"0\"}}";
            const string expectedRequestQuery = "invoice_id=1322226&per_page=100&page=1";

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(expectedRequestUri, expectedRequestQuery, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(responseBody));

            var sut = GetSystemUnderTest(http);

            //act
            var query = new Query<InvoiceComment, InvoiceCommentFilter>().AddFilter(x => x.InvoiceId, 1322226);
            var result = await sut.GetCommentListAsync(query);

            //assert
            A.CallTo(() => http.GetAsync(expectedRequestUri, expectedRequestQuery, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            Assert.Equal(0, result.TotalItems);
        }

        [Fact]
        public async Task GetCommentById_ShouldReturnCorrectValues()
        {
            //arrange
            var expectedRequestUri = new Uri("/api/invoice-comments/4662801", UriKind.Relative);
            const string responseBody = "{\"invoice-comment\":{\"id\":\"4662801\",\"created\":\"2015-06-04T10:04:54+02:00\",\"comment\":\"Rechnung erstellt.\",\"actionkey\":\"CREATE\",\"public\":\"0\",\"by_client\":\"0\",\"user_id\":\"52821\",\"email_id\":\"\",\"client_id\":\"\",\"invoice_id\":\"1322225\",\"customfield\":\"\"}}";
            var expectedResult = new InvoiceComment
            {
                Id = 4662801,
                Created = DateTime.Parse("2015-06-04T10:04:54+02:00", CultureInfo.InvariantCulture),
                Comment = "Rechnung erstellt.",
                ActionKey = CommentType.Create,
                Public = false,
                ByClient = false,
                UserId = 52821,
                EmailId = null,
                ClientId = null,
                InvoiceId = 1322225
            };

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(expectedRequestUri, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(responseBody));

            var sut = GetSystemUnderTest(http);

            //act
            var result = await sut.GetCommentByIdAsync(4662801);

            //assert
            A.CallTo(() => http.GetAsync(expectedRequestUri, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetCommentById__WithInvalidInputData_ShouldReturnArgumentException()
        {
            //arrange
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            //act and assert
            await Assert.ThrowsAsync<ArgumentException>(() => sut.GetCommentByIdAsync(0));
        }

        [Fact]
        public async Task GetCommentById_WithInvalidCredentials_ShouldThrowNotAuthorizedException()
        {
            //arrange
            var expectedRequestUri = new Uri("/api/invoice-comments/4662801", UriKind.Relative);
            var http = A.Fake<IHttpClient>();

            A.CallTo(() => http.GetAsync(expectedRequestUri, A<CancellationToken>.Ignored))
                .ThrowsAsync(ExceptionFactory.CreateNotAuthorizedException);

            var sut = GetSystemUnderTest(http);
            await Assert.ThrowsAsync<NotAuthorizedException>(() => sut.GetCommentByIdAsync(4662801));

            A.CallTo(() => http.GetAsync(expectedRequestUri, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetCommentById_WithInvalidId_ShouldThrowNotFoundException()
        {
            //arrange
            const int commentId = 1;
            var expectedRequestUri = new Uri("/api/invoice-comments/1", UriKind.Relative);

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(expectedRequestUri, A<CancellationToken>.Ignored))
                .ThrowsAsync(ExceptionFactory.CreateNotFoundException());

            var sut = GetSystemUnderTest(http);

            //act
            var result = await sut.GetCommentByIdAsync(commentId);

            //assert
            A.CallTo(() => http.GetAsync(expectedRequestUri, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            Assert.Null(result);
        }

        [Fact]
        public async Task Create_WithCorrectValues_ShouldCreateCommentAndReturnCorrectValues()
        {
            //arrange
            var comment = new InvoiceComment { InvoiceId = 7506691, Created = DateTime.Parse("0001-01-01T00:00:00", CultureInfo.InvariantCulture), Comment = "Test Comment", Public = true, ActionKey = CommentType.Comment, ByClient = true, UserId = 52821, ClientId = 3722360 };
            var expectedResult = new InvoiceComment { Id = 31327675, Created = DateTime.Parse("2020-07-30T10:42:51+02:00", CultureInfo.InvariantCulture), Comment = "Test Comment", ActionKey = CommentType.Comment, Public = true, ByClient = true, UserId = 52821, ClientId = 3722360, InvoiceId = 7506691 };

            var expectedRequestUri = new Uri("/api/invoice-comments", UriKind.Relative);
            const string expectedRequestBody = "{\"invoice-comment\":{\"id\":\"0\",\"created\":\"0001-01-01T00:00:00.0000000\",\"comment\":\"Test Comment\",\"actionkey\":\"COMMENT\",\"public\":\"True\",\"by_client\":\"True\",\"user_id\":\"52821\",\"email_id\":\"\",\"client_id\":\"3722360\",\"invoice_id\":\"7506691\"}}";
            const string responseBody = "{\"invoice-comment\":{\"id\":\"31327675\",\"created\":\"2020-07-30T10:42:51+02:00\",\"comment\":\"Test Comment\",\"actionkey\":\"COMMENT\",\"public\":\"1\",\"by_client\":\"1\",\"user_id\":\"52821\",\"email_id\":\"\",\"client_id\":\"3722360\",\"invoice_id\":\"7506691\",\"customfield\":\"\"}}";

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.PostAsync(expectedRequestUri, expectedRequestBody, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(responseBody));

            var sut = GetSystemUnderTest(http);

            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            //act
            var result = await sut.CreateCommentAsync(comment);

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
            await Assert.ThrowsAsync<ArgumentNullException>(() => sut.CreateCommentAsync(null));
            await Assert.ThrowsAsync<ArgumentException>(() => sut.CreateCommentAsync(new InvoiceComment()));
            await Assert.ThrowsAsync<ArgumentException>(() => sut.CreateCommentAsync(new InvoiceComment { Id = 1 }));
        }

        [Fact]
        public async Task Create_WithInvalidApiKey_ShouldThrowNotAuthorizedException()
        {
            //arrange
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            var expectedRequestUri = new Uri("/api/invoice-comments", UriKind.Relative);
            A.CallTo(() => http.PostAsync(expectedRequestUri, A<string>.Ignored, A<CancellationToken>.Ignored))
                .ThrowsAsync(ExceptionFactory.CreateNotAuthorizedException);

            var comment = new InvoiceComment { InvoiceId = 1, Comment = "asdf" };

            await Assert.ThrowsAsync<NotAuthorizedException>(() => sut.CreateCommentAsync(comment));
            A.CallTo(() => http.PostAsync(expectedRequestUri, A<string>.Ignored, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Delete_WithCorrectParameters_ShouldSucceed()
        {
            const int id = 8;
            const string expectedUri = "/api/invoice-comments/8";


            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.DeleteAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(string.Empty));

            var sut = GetSystemUnderTest(http);

            await sut.DeleteCommentAsync(id);

            A.CallTo(() => http.DeleteAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Delete_WithInvalidCredentials_ShouldThrowNotAuthorizedException()
        {
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            const string expectedUri = "/api/invoice-comments/1";
            const int id = 1;
            A.CallTo(() => http.DeleteAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .ThrowsAsync(ExceptionFactory.CreateNotAuthorizedException);

            await Assert.ThrowsAsync<NotAuthorizedException>(() => sut.DeleteCommentAsync(id));
            A.CallTo(() => http.DeleteAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Delete_WithInvalidId_ShouldThrowNotFoundException()
        {
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            const string expectedUri = "/api/invoice-comments/1";
            const int id = 1;
            A.CallTo(() => http.DeleteAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .ThrowsAsync(ExceptionFactory.CreateNotFoundException);

            await Assert.ThrowsAsync<NotFoundException>(() => sut.DeleteCommentAsync(id));
            A.CallTo(() => http.DeleteAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Delete_WithInvalidInputValue_ShouldThrowArgumentException()
        {
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            await Assert.ThrowsAsync<ArgumentException>(() => sut.DeleteCommentAsync(0));
        }
    }
}
