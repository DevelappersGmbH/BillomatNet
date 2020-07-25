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
        public async Task DeleteArticle()
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
        public async Task GetArticleById()
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
            Assert.Equal(new DateTime(2015,5,28,7,38,7, DateTimeKind.Utc),result.Created.ToUniversalTime());
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
    }
}
