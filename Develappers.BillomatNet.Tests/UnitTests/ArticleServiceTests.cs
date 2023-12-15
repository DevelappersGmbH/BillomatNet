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
    public class ArticleServiceTests : UnitTestBase<ArticleService>
    {
        [Fact]
        public async Task CreateArticle_WithInvalidCredentials_ShouldThrowNotAuthorizedException()
        {
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            const string expectedUri = "/api/articles";
            A.CallTo(() => http.PostAsync(new Uri(expectedUri, UriKind.Relative), A<string>.Ignored,
                    A<CancellationToken>.Ignored))
                .ThrowsAsync(ExceptionFactory.CreateNotAuthorizedException);

            var articleItem = new Article
            {
                Title = "xUnit test",
                SalesPrice = 3.5f,
                UnitId = 20573,
                TaxId = 21281,
                PurchasePrice = 3.0f
            };

            await Assert.ThrowsAsync<NotAuthorizedException>(() => sut.CreateAsync(articleItem));
            A.CallTo(() => http.PostAsync(new Uri(expectedUri, UriKind.Relative), A<string>.Ignored,
                    A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task CreateArticle_WithInvalidInputValue_ShouldThrowArgumentException()
        {
            // arrange
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            // act and assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => sut.CreateAsync(null));
            await Assert.ThrowsAsync<ArgumentException>(() => sut.CreateAsync(new Article
            {
                Id = 999
            }));
        }

        [Fact]
        public async Task CreateArticle_WithValidInputValue_ShouldCreateArticleAndReturnCorrectValues()
        {
            // arrange
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            const string expectedUri = "/api/articles";
            const string expectedHttpRequest =
                "{\"article\":{\"id\":\"0\",\"created\":\"0001-01-01\",\"number\":\"\",\"number_length\":\"0\",\"title\":\"xUnit test\",\"sales_price\":\"3.5\",\"unit_id\":\"20573\",\"tax_id\":\"21281\",\"purchase_price\":\"3.4\",\"purchase_price_net_gross\":\"NET\",\"supplier_id\":\"\",\"type\":\"\",\"cost_center\":\"Ib34\"}}";
            const string httpResult =
                "{\"article\":{\"id\":\"842769\",\"created\":\"2020-07-28T18:32:05+02:00\",\"updated\":\"2020-07-28T18:32:05+02:00\",\"archived\":\"0\",\"unit_id\":\"20573\",\"article_number\":\"5\",\"number\":\"5\",\"number_pre\":\"\",\"number_length\":\"0\",\"type\":\"SERVICE\",\"title\":\"xUnit test\",\"description\":\"\",\"sales_price\":\"3.5\",\"sales_price2\":\"\",\"sales_price3\":\"\",\"sales_price4\":\"\",\"sales_price5\":\"\",\"currency_code\":\"EUR\",\"tax_id\":\"21281\",\"revenue_account_number\":\"\",\"cost_center\":\"Ib34\",\"purchase_price\":\"3.4\",\"purchase_price_net_gross\":\"NET\",\"supplier_id\":\"\",\"customfield\":\"\",\"article-property-values\":{\"article-property-value\":[{\"id\":\"1423686\",\"created\":\"2020-07-28T18:32:05+02:00\",\"updated\":\"2020-07-28T18:32:05+02:00\",\"article_id\":\"842769\",\"article_property_id\":\"2442\",\"type\":\"TEXTFIELD\",\"name\":\"Farbe\",\"value\":\"farblos\",\"customfield\":\"\"},{\"id\":\"1423687\",\"created\":\"2020-07-28T18:32:05+02:00\",\"updated\":\"2020-07-28T18:32:05+02:00\",\"article_id\":\"842769\",\"article_property_id\":\"2490\",\"type\":\"CHECKBOX\",\"name\":\"ist defekt?\",\"value\":\"1\",\"customfield\":\"\"},{\"id\":\"1423688\",\"created\":\"2020-07-28T18:32:05+02:00\",\"updated\":\"2020-07-28T18:32:05+02:00\",\"article_id\":\"842769\",\"article_property_id\":\"4499\",\"type\":\"CHECKBOX\",\"name\":\"Sch\\u00f6n\",\"value\":\"0\",\"customfield\":\"\"}]}}}";

            A.CallTo(() => http.PostAsync(new Uri(expectedUri, UriKind.Relative), expectedHttpRequest,
                    A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(httpResult));

            var inputArticle = new Article
            {
                Title = "xUnit test",
                SalesPrice = 3.5f,
                UnitId = 20573,
                TaxId = 21281,
                PurchasePrice = 3.4f,
                CostCenter = "Ib34"
            };

            // act
            var result = await sut.CreateAsync(inputArticle);

            // assert
            A.CallTo(() => http.PostAsync(new Uri(expectedUri, UriKind.Relative), expectedHttpRequest,
                    A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            // check input parameters - the rest of the properties is checked by GetById
            Assert.True(result.Id > 0);
            Assert.Equal(inputArticle.Title, result.Title);
            Assert.Equal(inputArticle.SalesPrice, result.SalesPrice);
            Assert.Equal(inputArticle.UnitId, result.UnitId);
            Assert.Equal(inputArticle.TaxId, result.TaxId);
            Assert.Equal(inputArticle.PurchasePrice, result.PurchasePrice);
        }

        [Fact]
        public async Task DeleteArticle_WithCorrectParameters_ShouldSucceed()
        {
            const int id = 8;
            const string expectedUri = "/api/articles/8";


            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.DeleteAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(string.Empty));

            var sut = GetSystemUnderTest(http);

            await sut.DeleteAsync(id);

            A.CallTo(() => http.DeleteAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task DeleteArticle_WithInvalidCredentials_ShouldThrowNotAuthorizedException()
        {
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            const string expectedUri = "/api/articles/1";
            const int id = 1;
            A.CallTo(() => http.DeleteAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .ThrowsAsync(ExceptionFactory.CreateNotAuthorizedException);

            await Assert.ThrowsAsync<NotAuthorizedException>(() => sut.DeleteAsync(id));
            A.CallTo(() => http.DeleteAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task DeleteArticle_WithInvalidId_ShouldThrowNotFoundException()
        {
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            const string expectedUri = "/api/articles/1";
            const int id = 1;
            A.CallTo(() => http.DeleteAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .ThrowsAsync(ExceptionFactory.CreateNotFoundException);

            await Assert.ThrowsAsync<NotFoundException>(() => sut.DeleteAsync(id));
            A.CallTo(() => http.DeleteAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task DeleteArticle_WithInvalidInputValue_ShouldThrowArgumentException()
        {
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            await Assert.ThrowsAsync<ArgumentException>(() => sut.DeleteAsync(0));
        }

        [Fact]
        public async Task EditArticle_WithInvalidArticleId_ShouldThrowNotFoundException()
        {
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            const string expectedUri = "/api/articles/1";
            A.CallTo(() => http.PutAsync(new Uri(expectedUri, UriKind.Relative), A<string>.Ignored,
                    A<CancellationToken>.Ignored))
                .ThrowsAsync(ExceptionFactory.CreateNotFoundException);

            var editedArticleItem = new Article
            {
                Id = 1
            };

            await Assert.ThrowsAsync<NotFoundException>(() => sut.EditAsync(editedArticleItem));
            A.CallTo(() => http.PutAsync(new Uri(expectedUri, UriKind.Relative), A<string>.Ignored,
                    A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();
        }


        [Fact]
        public async Task EditArticle_WithInvalidCredentials_ShouldThrowNotAuthorizedException()
        {
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            const string expectedUri = "/api/articles/1";
            A.CallTo(() => http.PutAsync(new Uri(expectedUri, UriKind.Relative), A<string>.Ignored,
                    A<CancellationToken>.Ignored))
                .ThrowsAsync(ExceptionFactory.CreateNotAuthorizedException);

            var editedArticleItem = new Article
            {
                Id = 1
            };

            await Assert.ThrowsAsync<NotAuthorizedException>(() => sut.EditAsync(editedArticleItem));
            A.CallTo(() => http.PutAsync(new Uri(expectedUri, UriKind.Relative), A<string>.Ignored,
                    A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();
        }


        [Fact]
        public async Task EditArticle_WithInvalidInputValue_ShouldThrowArgumentException()
        {
            // arrange
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            // act and assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => sut.EditAsync(null));
            await Assert.ThrowsAsync<ArgumentException>(() => sut.EditAsync(new Article
            {
                Id = 0
            }));
        }

        [Fact]
        public async Task GetArticleById_WithInvalidId_ShouldReturnNull()
        {
            const int id = 1;
            const string expectedUri = "/api/articles/1";

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .ThrowsAsync(ExceptionFactory.CreateNotFoundException);

            var sut = GetSystemUnderTest(http);
            var result = await sut.GetByIdAsync(id);

            A.CallTo(() => http.GetAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            Assert.Null(result);
        }

        [Fact]
        public async Task GetArticleById_WithValidData_ShouldReturnCorrectValues()
        {
            const int id = 8;

            const string expectedUri = "/api/articles/8";
            const string httpResult =
                "{\r\n    \"article\": {\r\n        \"id\": \"8\",\r\n        \"created\": \"2015-05-28T09:38:07+02:00\",\r\n        \"updated\": \"2020-07-10T12:20:36+02:00\",\r\n        \"archived\": \"0\",\r\n        \"unit_id\": \"20574\",\r\n        \"article_number\": \"ART3\",\r\n        \"number\": \"3\",\r\n        \"number_pre\": \"ART\",\r\n        \"number_length\": \"0\",\r\n        \"type\": \"\",\r\n        \"title\": \"Tomaten\",\r\n        \"description\": \"Rot und saftig\\r\\nDas Schengener Abkommen gehört zu den großen Errungenschaften der Europäischen Integration. Seit Mitte der neunziger Jahre steht der Begriff für die Abschaffung der Personenkontrollen an den Binnengrenzen und damit für Reisefreiheit und ein \\\"Europa ohne Schranken\\\".\",\r\n        \"sales_price\": \"2\",\r\n        \"sales_price2\": \"1.2\",\r\n        \"sales_price3\": \"0.99\",\r\n        \"sales_price4\": \"0\",\r\n        \"sales_price5\": \"0\",\r\n        \"currency_code\": \"EUR\",\r\n        \"tax_id\": \"21281\",\r\n        \"revenue_account_number\": \"\",\r\n        \"cost_center\": \"\",\r\n        \"purchase_price\": \"1.2\",\r\n        \"purchase_price_net_gross\": \"GROSS\",\r\n        \"supplier_id\": \"\",\r\n        \"customfield\": \"\",\r\n        \"article-property-values\": {\r\n            \"article-property-value\": [\r\n                {\r\n                    \"id\": \"569628\",\r\n                    \"article_id\": \"154123\",\r\n                    \"article_property_id\": \"2442\",\r\n                    \"type\": \"TEXTFIELD\",\r\n                    \"name\": \"Farbe\",\r\n                    \"value\": \"Rot\",\r\n                    \"customfield\": \"\"\r\n                },\r\n                {\r\n                    \"id\": \"581696\",\r\n                    \"article_id\": \"154123\",\r\n                    \"article_property_id\": \"2490\",\r\n                    \"type\": \"CHECKBOX\",\r\n                    \"name\": \"ist defekt?\",\r\n                    \"value\": \"0\",\r\n                    \"customfield\": \"\"\r\n                },\r\n                {\r\n                    \"id\": \"581734\",\r\n                    \"article_id\": \"154123\",\r\n                    \"article_property_id\": \"3250\",\r\n                    \"type\": \"TEXTAREA\",\r\n                    \"name\": \"Blob\",\r\n                    \"value\": \"Bums\",\r\n                    \"customfield\": \"\"\r\n                }\r\n            ]\r\n        }\r\n    }\r\n}";
            var expected = new Article
            {
                Id = id,
                Created = DateTime.Parse("2015-05-28T09:38:07+02:00", CultureInfo.InvariantCulture),
                Updated = DateTime.Parse("2020-07-10T12:20:36+02:00", CultureInfo.InvariantCulture),
                UnitId = 20574,
                ArticleNumber = "ART3",
                CostCenter = "",
                Number = 3,
                NumberPrefix = "ART",
                NumberLength = 0,
                Title = "Tomaten",
                Description =
                    "Rot und saftig\r\nDas Schengener Abkommen gehört zu den großen Errungenschaften der Europäischen Integration. Seit Mitte der neunziger Jahre steht der Begriff für die Abschaffung der Personenkontrollen an den Binnengrenzen und damit für Reisefreiheit und ein \"Europa ohne Schranken\".",
                SalesPrice = 2f,
                SalesPrice2 = 1.2f,
                SalesPrice3 = 0.99f,
                SalesPrice4 = 0f,
                SalesPrice5 = 0f,
                CurrencyCode = "EUR",
                TaxId = 21281,
                PurchasePrice = 1.2f,
                PurchasePriceNetGross = NetGrossType.Gross
            };

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(httpResult));

            var sut = GetSystemUnderTest(http);
            var result = await sut.GetByIdAsync(id);

            A.CallTo(() => http.GetAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetList_ShouldReturnCorrectValues()
        {
            // arrange
            var expectedRequestUri = new Uri("/api/articles", UriKind.Relative);
            const string expectedRequestQuery = "per_page=10&page=2";
            const string responseBody =
                "{\"articles\":{\"article\":[{\"id\":\"203719\",\"created\":\"2015-11-04T18:00:37+01:00\",\"updated\":\"2017-07-22T13:57:37+02:00\",\"archived\":\"0\",\"unit_id\":\"\",\"article_number\":\"ART62\",\"number\":\"62\",\"number_pre\":\"ART\",\"number_length\":\"0\",\"type\":\"\",\"title\":\"Banane\",\"description\":\"Gelb und krumm\",\"sales_price\":\"5\",\"sales_price2\":\"4.2\",\"sales_price3\":\"3\",\"sales_price4\":\"2.9\",\"sales_price5\":\"2.8\",\"currency_code\":\"EUR\",\"tax_id\":\"\",\"revenue_account_number\":\"\",\"cost_center\":\"\",\"purchase_price\":\"2.5\",\"purchase_price_net_gross\":\"NET\",\"supplier_id\":\"\",\"customfield\":\"\",\"article-property-values\":{\"article-property-value\":[{\"id\":\"309134\",\"article_id\":\"203719\",\"article_property_id\":\"2442\",\"type\":\"TEXTFIELD\",\"name\":\"Farbe\",\"value\":\"\",\"customfield\":\"\"},{\"id\":\"309135\",\"article_id\":\"203719\",\"article_property_id\":\"2490\",\"type\":\"CHECKBOX\",\"name\":\"ist defekt?\",\"value\":\"1\",\"customfield\":\"\"},{\"id\":\"569678\",\"article_id\":\"203719\",\"article_property_id\":\"3250\",\"type\":\"TEXTAREA\",\"name\":\"Blob\",\"value\":\"\",\"customfield\":\"\"}]}},{\"id\":\"205143\",\"created\":\"2015-11-12T15:53:24+01:00\",\"updated\":\"2020-07-10T12:20:36+02:00\",\"archived\":\"0\",\"unit_id\":\"\",\"article_number\":\"ART63\",\"number\":\"63\",\"number_pre\":\"ART\",\"number_length\":\"0\",\"type\":\"\",\"title\":\"Apfel\",\"description\":\"\",\"sales_price\":\"1.2\",\"sales_price2\":\"1.15\",\"sales_price3\":\"1.13\",\"sales_price4\":\"0.8\",\"sales_price5\":\"0.78\",\"currency_code\":\"EUR\",\"tax_id\":\"21282\",\"revenue_account_number\":\"\",\"cost_center\":\"\",\"purchase_price\":\"0.58\",\"purchase_price_net_gross\":\"NET\",\"supplier_id\":\"\",\"customfield\":\"\",\"article-property-values\":{\"article-property-value\":[{\"id\":\"298535\",\"article_id\":\"205143\",\"article_property_id\":\"2442\",\"type\":\"TEXTFIELD\",\"name\":\"Farbe\",\"value\":\"Rot\",\"customfield\":\"\"},{\"id\":\"320391\",\"article_id\":\"205143\",\"article_property_id\":\"2490\",\"type\":\"CHECKBOX\",\"name\":\"ist defekt?\",\"value\":\"\",\"customfield\":\"\"},{\"id\":\"569679\",\"article_id\":\"205143\",\"article_property_id\":\"3250\",\"type\":\"TEXTAREA\",\"name\":\"Blob\",\"value\":\"\",\"customfield\":\"\"}]}},{\"id\":\"205176\",\"created\":\"2015-11-12T22:08:43+01:00\",\"updated\":\"2020-07-10T12:20:36+02:00\",\"archived\":\"0\",\"unit_id\":\"\",\"article_number\":\"ART64\",\"number\":\"64\",\"number_pre\":\"ART\",\"number_length\":\"0\",\"type\":\"\",\"title\":\"Weintrauben\",\"description\":\"Aus Spanien\",\"sales_price\":\"1.09\",\"sales_price2\":\"1\",\"sales_price3\":\"0.8\",\"sales_price4\":\"0.45\",\"sales_price5\":\"0\",\"currency_code\":\"EUR\",\"tax_id\":\"21282\",\"revenue_account_number\":\"\",\"cost_center\":\"\",\"purchase_price\":\"0.45\",\"purchase_price_net_gross\":\"GROSS\",\"supplier_id\":\"\",\"customfield\":\"\",\"article-property-values\":{\"article-property-value\":[{\"id\":\"298529\",\"article_id\":\"205176\",\"article_property_id\":\"2442\",\"type\":\"TEXTFIELD\",\"name\":\"Farbe\",\"value\":\"farblos\",\"customfield\":\"\"},{\"id\":\"309133\",\"article_id\":\"205176\",\"article_property_id\":\"2490\",\"type\":\"CHECKBOX\",\"name\":\"ist defekt?\",\"value\":\"1\",\"customfield\":\"\"},{\"id\":\"569680\",\"article_id\":\"205176\",\"article_property_id\":\"3250\",\"type\":\"TEXTAREA\",\"name\":\"Blob\",\"value\":\"\",\"customfield\":\"\"}]}}],\"@page\":\"2\",\"@per_page\":\"10\",\"@total\":\"32\"}}";
            var expectedResult = new List<Article>
            {
                new()
                {
                    Id = 203719,
                    Created = DateTime.Parse("2015-11-04T18:00:37+01:00", CultureInfo.InvariantCulture),
                    Updated = DateTime.Parse("2017-07-22T13:57:37+02:00", CultureInfo.InvariantCulture),
                    UnitId = null,
                    ArticleNumber = "ART62",
                    CostCenter = "",
                    Number = 62,
                    NumberPrefix = "ART",
                    NumberLength = 0,
                    Title = "Banane",
                    Description = "Gelb und krumm",
                    SalesPrice = 5f,
                    SalesPrice2 = 4.2f,
                    SalesPrice3 = 3f,
                    SalesPrice4 = 2.9f,
                    SalesPrice5 = 2.8f,
                    CurrencyCode = "EUR",
                    TaxId = null,
                    PurchasePrice = 2.5f,
                    PurchasePriceNetGross = NetGrossType.Net
                },
                new()
                {
                    Id = 205143,
                    Created = DateTime.Parse("2015-11-12T15:53:24+01:00", CultureInfo.InvariantCulture),
                    Updated = DateTime.Parse("2020-07-10T12:20:36+02:00", CultureInfo.InvariantCulture),
                    UnitId = null,
                    ArticleNumber = "ART63",
                    CostCenter = "",
                    Number = 63,
                    NumberPrefix = "ART",
                    NumberLength = 0,
                    Title = "Apfel",
                    Description = "",
                    SalesPrice = 1.2f,
                    SalesPrice2 = 1.15f,
                    SalesPrice3 = 1.13f,
                    SalesPrice4 = 0.8f,
                    SalesPrice5 = 0.78f,
                    CurrencyCode = "EUR",
                    TaxId = 21282,
                    PurchasePrice = 0.58f,
                    PurchasePriceNetGross = NetGrossType.Net
                },
                new()
                {
                    Id = 205176,
                    Created = DateTime.Parse("2015-11-12T22:08:43+01:00", CultureInfo.InvariantCulture),
                    Updated = DateTime.Parse("2020-07-10T12:20:36+02:00", CultureInfo.InvariantCulture),
                    UnitId = null,
                    ArticleNumber = "ART64",
                    CostCenter = "",
                    Number = 64,
                    NumberPrefix = "ART",
                    NumberLength = 0,
                    Title = "Weintrauben",
                    Description = "Aus Spanien",
                    SalesPrice = 1.09f,
                    SalesPrice2 = 1f,
                    SalesPrice3 = 0.8f,
                    SalesPrice4 = 0.45f,
                    SalesPrice5 = 0f,
                    CurrencyCode = "EUR",
                    TaxId = 21282,
                    PurchasePrice = 0.45f,
                    PurchasePriceNetGross = NetGrossType.Gross
                }
            };

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(expectedRequestUri, expectedRequestQuery, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(responseBody));

            var sut = GetSystemUnderTest(http);

            // act
            var result = await sut.GetListAsync(new Query<Article, ArticleFilter>().SetItemsPerPage(10).SetPage(2));

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
