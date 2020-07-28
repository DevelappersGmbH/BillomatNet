// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Api.Net;
using Develappers.BillomatNet.Types;
using FakeItEasy;
using Xunit;

namespace Develappers.BillomatNet.Tests.UnitTests
{
    public class ArticleServiceTests : UnitTestBase<ArticleService>
    {
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
        public async Task GetArticleById_WithValidData_ShouldReturnCorrectValues()
        {
            const int id = 8;

            const string expectedUri = "/api/articles/8";
            const string httpResult = "{\r\n    \"article\": {\r\n        \"id\": \"8\",\r\n        \"created\": \"2015-05-28T09:38:07+02:00\",\r\n        \"updated\": \"2020-07-10T12:20:36+02:00\",\r\n        \"archived\": \"0\",\r\n        \"unit_id\": \"20574\",\r\n        \"article_number\": \"ART3\",\r\n        \"number\": \"3\",\r\n        \"number_pre\": \"ART\",\r\n        \"number_length\": \"0\",\r\n        \"type\": \"\",\r\n        \"title\": \"Tomaten\",\r\n        \"description\": \"Rot und saftig\\r\\nDas Schengener Abkommen gehört zu den großen Errungenschaften der Europäischen Integration. Seit Mitte der neunziger Jahre steht der Begriff für die Abschaffung der Personenkontrollen an den Binnengrenzen und damit für Reisefreiheit und ein \\\"Europa ohne Schranken\\\".\",\r\n        \"sales_price\": \"2\",\r\n        \"sales_price2\": \"1.2\",\r\n        \"sales_price3\": \"0.99\",\r\n        \"sales_price4\": \"0\",\r\n        \"sales_price5\": \"0\",\r\n        \"currency_code\": \"EUR\",\r\n        \"tax_id\": \"21281\",\r\n        \"revenue_account_number\": \"\",\r\n        \"cost_center\": \"\",\r\n        \"purchase_price\": \"1.2\",\r\n        \"purchase_price_net_gross\": \"GROSS\",\r\n        \"supplier_id\": \"\",\r\n        \"customfield\": \"\",\r\n        \"article-property-values\": {\r\n            \"article-property-value\": [\r\n                {\r\n                    \"id\": \"569628\",\r\n                    \"article_id\": \"154123\",\r\n                    \"article_property_id\": \"2442\",\r\n                    \"type\": \"TEXTFIELD\",\r\n                    \"name\": \"Farbe\",\r\n                    \"value\": \"Rot\",\r\n                    \"customfield\": \"\"\r\n                },\r\n                {\r\n                    \"id\": \"581696\",\r\n                    \"article_id\": \"154123\",\r\n                    \"article_property_id\": \"2490\",\r\n                    \"type\": \"CHECKBOX\",\r\n                    \"name\": \"ist defekt?\",\r\n                    \"value\": \"0\",\r\n                    \"customfield\": \"\"\r\n                },\r\n                {\r\n                    \"id\": \"581734\",\r\n                    \"article_id\": \"154123\",\r\n                    \"article_property_id\": \"3250\",\r\n                    \"type\": \"TEXTAREA\",\r\n                    \"name\": \"Blob\",\r\n                    \"value\": \"Bums\",\r\n                    \"customfield\": \"\"\r\n                }\r\n            ]\r\n        }\r\n    }\r\n}";


            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(httpResult));

            var sut = GetSystemUnderTest(http);
            var result = await sut.GetByIdAsync(id);

            A.CallTo(() => http.GetAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            Assert.Equal(id, result.Id);
            Assert.Equal(new DateTime(2015, 5, 28, 7, 38, 7, DateTimeKind.Utc), result.Created.ToUniversalTime());
            Assert.Equal(new DateTime(2020, 7, 10, 10, 20, 36, DateTimeKind.Utc), result.Updated.ToUniversalTime());
            //Assert.Equal(false, result.Archived);
            Assert.Equal(20574, result.UnitId);
            Assert.Equal("ART3", result.ArticleNumber);
            Assert.Equal(3, result.Number);
            Assert.Equal("ART", result.NumberPrefix);
            Assert.Equal(0, result.NumberLength);
            Assert.Equal("Tomaten", result.Title);
            Assert.Equal("Rot und saftig\r\nDas Schengener Abkommen gehört zu den großen Errungenschaften der Europäischen Integration. Seit Mitte der neunziger Jahre steht der Begriff für die Abschaffung der Personenkontrollen an den Binnengrenzen und damit für Reisefreiheit und ein \"Europa ohne Schranken\".", result.Description);
            Assert.Equal(2f, result.SalesPrice);
            Assert.Equal(1.2f, result.SalesPrice2);
            Assert.Equal(0.99f, result.SalesPrice3);
            Assert.Equal(0f, result.SalesPrice4);
            Assert.Equal(0f, result.SalesPrice5);
            Assert.Equal("EUR", result.CurrencyCode);
            //Assert.Equal("", result.RevenueAccountNumber);
            //Assert.Equal("", result.CostCenter);
            Assert.Equal(1.2f, result.PurchasePrice);
            Assert.Equal(NetGrossType.Gross, result.PurchasePriceNetGross);
        }

        [Fact]
        public async Task CreateArticle_WithInvalidInputValue_ShouldThrowArgumentException()
        {
            // arrange
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            // act and assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => sut.CreateAsync(null));
            await Assert.ThrowsAsync<ArgumentException>(() => sut.CreateAsync(new Article { Id = 999 }));
        }

        [Fact]
        public async Task CreateArticle_WithValidInputValue_ShouldReturnCorrectValues()
        {
            // arrange
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            const string expectedUri = "/api/articles";
            const string expectedHttpRequest = "{\"article\":{\"id\":\"0\",\"created\":\"0001-01-01\",\"updated\":null,\"article_number\":null,\"number\":\"\",\"number_pre\":null,\"number_length\":\"0\",\"title\":\"xUnit test\",\"description\":null,\"sales_price\":\"3.5\",\"sales_price2\":null,\"sales_price3\":null,\"sales_price4\":null,\"sales_price5\":null,\"currency_code\":null,\"unit_id\":\"20573\",\"tax_id\":\"21281\",\"purchase_price\":\"3.4\",\"purchase_price_net_gross\":\"NET\",\"supplier_id\":\"\"}}";
            const string httpResult = "{\"article\":{\"id\":\"842769\",\"created\":\"2020-07-28T18:32:05+02:00\",\"updated\":\"2020-07-28T18:32:05+02:00\",\"archived\":\"0\",\"unit_id\":\"20573\",\"article_number\":\"5\",\"number\":\"5\",\"number_pre\":\"\",\"number_length\":\"0\",\"type\":\"SERVICE\",\"title\":\"xUnit test\",\"description\":\"\",\"sales_price\":\"3.5\",\"sales_price2\":\"\",\"sales_price3\":\"\",\"sales_price4\":\"\",\"sales_price5\":\"\",\"currency_code\":\"EUR\",\"tax_id\":\"21281\",\"revenue_account_number\":\"\",\"cost_center\":\"\",\"purchase_price\":\"3.4\",\"purchase_price_net_gross\":\"NET\",\"supplier_id\":\"\",\"customfield\":\"\",\"article-property-values\":{\"article-property-value\":[{\"id\":\"1423686\",\"created\":\"2020-07-28T18:32:05+02:00\",\"updated\":\"2020-07-28T18:32:05+02:00\",\"article_id\":\"842769\",\"article_property_id\":\"2442\",\"type\":\"TEXTFIELD\",\"name\":\"Farbe\",\"value\":\"farblos\",\"customfield\":\"\"},{\"id\":\"1423687\",\"created\":\"2020-07-28T18:32:05+02:00\",\"updated\":\"2020-07-28T18:32:05+02:00\",\"article_id\":\"842769\",\"article_property_id\":\"2490\",\"type\":\"CHECKBOX\",\"name\":\"ist defekt?\",\"value\":\"1\",\"customfield\":\"\"},{\"id\":\"1423688\",\"created\":\"2020-07-28T18:32:05+02:00\",\"updated\":\"2020-07-28T18:32:05+02:00\",\"article_id\":\"842769\",\"article_property_id\":\"4499\",\"type\":\"CHECKBOX\",\"name\":\"Sch\\u00f6n\",\"value\":\"0\",\"customfield\":\"\"}]}}}";

            A.CallTo(() => http.PostAsync(new Uri(expectedUri, UriKind.Relative), expectedHttpRequest, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(httpResult));

            var inputArticle = new Article
            {
                Title = "xUnit test",
                SalesPrice = 3.5f,
                UnitId = 20573,
                TaxId = 21281,
                PurchasePrice = 3.4f
            };

            // act
            var result = await sut.CreateAsync(inputArticle);

            // assert
            A.CallTo(() => http.PostAsync(new Uri(expectedUri, UriKind.Relative), expectedHttpRequest, A<CancellationToken>.Ignored))
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
        public async Task CreateArticle_WithInvalidCredentials_ShouldThrowNotAuthorizedException()
        {
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            const string expectedUri = "/api/articles";
            A.CallTo(() => http.PostAsync(new Uri(expectedUri, UriKind.Relative), A<string>.Ignored, A<CancellationToken>.Ignored))
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
            A.CallTo(() => http.PostAsync(new Uri(expectedUri, UriKind.Relative), A<string>.Ignored, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task EditArticle_WithInvalidArticleId_ShouldThrowNotFoundException()
        {
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            const string expectedUri = "/api/articles/1";
            A.CallTo(() => http.PutAsync(new Uri(expectedUri, UriKind.Relative), A<string>.Ignored, A<CancellationToken>.Ignored))
                .ThrowsAsync(ExceptionFactory.CreateNotFoundException);

            var editedArticleItem = new Article
            {
                Id = 1,
            };

            await Assert.ThrowsAsync<NotFoundException>(() => sut.EditAsync(editedArticleItem));
            A.CallTo(() => http.PutAsync(new Uri(expectedUri, UriKind.Relative), A<string>.Ignored, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();
        }


        [Fact]
        public async Task EditArticleWithInvalidCredentials_ShouldThrowNotAuthorizedException()
        {
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            const string expectedUri = "/api/articles/1";
            A.CallTo(() => http.PutAsync(new Uri(expectedUri, UriKind.Relative), A<string>.Ignored, A<CancellationToken>.Ignored))
                .ThrowsAsync(ExceptionFactory.CreateNotAuthorizedException);

            var editedArticleItem = new Article
            {
                Id = 1,
            };

            await Assert.ThrowsAsync<NotAuthorizedException>(() => sut.EditAsync(editedArticleItem));
            A.CallTo(() => http.PutAsync(new Uri(expectedUri, UriKind.Relative), A<string>.Ignored, A<CancellationToken>.Ignored))
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
            await Assert.ThrowsAsync<ArgumentException>(() => sut.EditAsync(new Article { Id = 0 }));
        }
    }
}
