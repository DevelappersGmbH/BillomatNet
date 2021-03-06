﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
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
    public class PurchaseInvoiceServiceTests : UnitTestBase<PurchaseInvoiceService>
    {
        [Fact]
        public async Task GetById_WithInvalidCredentials_ShouldThrowNotAuthorizedException()
        {
            const int id = 485054;
            const string expectedUri = "/api/incomings/485054";

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .ThrowsAsync(ExceptionFactory.CreateNotAuthorizedException);

            var sut = GetSystemUnderTest(http);
            await Assert.ThrowsAsync<NotAuthorizedException>(() => sut.GetByIdAsync(id));

            A.CallTo(() => http.GetAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetById_WithValidData_ShouldReturnCorrectValues()
        {
            const string httpResult =
                "{\"incoming\":{\"id\":\"485054\",\"created\":\"2020-07-01T09:38:21+02:00\",\"updated\":\"2020-07-01T09:38:22+02:00\",\"supplier_id\":\"50013\",\"number\":\"1234\",\"client_number\":\"\",\"date\":\"2020-07-01\",\"due_date\":\"\",\"address\":\"Meyers AG\\r\\nHerr Jens  Maul\",\"status\":\"OPEN\",\"label\":\"\",\"note\":\"\",\"total_net\":\"-12.36\",\"total_gross\":\"-14.71\",\"currency_code\":\"EUR\",\"quote\":\"1\",\"paid_amount\":\"0\",\"open_amount\":\"-14.71\",\"expense_account_number\":\"\",\"category\":\"\",\"page_count\":\"1\",\"customfield\":\"\",\"incoming-property-values\":\"\"}}";
            const int id = 485054;
            const string expectedUri = "/api/incomings/485054";

            var expectedResult = new PurchaseInvoice
            {
                Id = id,
                Created = DateTime.Parse("2020-07-01T09:38:21+02:00", CultureInfo.InvariantCulture),
                Updated = DateTime.Parse("2020-07-01T09:38:22+02:00", CultureInfo.InvariantCulture),
                SupplierId = 50013,
                Number = "1234",
                ClientNumber = null,
                Date = DateTime.Parse("2020-07-01", CultureInfo.InvariantCulture),
                Address = "Meyers AG\r\nHerr Jens  Maul",
                Status = PurchaseInvoiceStatus.Open,
                PageCount = 1,
                Category = null,
                CurrencyCode = "EUR",
                DueDate = null,
                ExpenseAccountNumber = null,
                Label = null,
                TotalNet = -12.36F,
                TotalGross = -14.71F,
                Quote = 1,
                PaidAmount = 0,
                OpenAmount = -14.71F,
                Note = null
            };

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(httpResult));

            var sut = GetSystemUnderTest(http);
            var result = await sut.GetByIdAsync(id);

            A.CallTo(() => http.GetAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            result.Should().BeEquivalentTo(expectedResult);
        }


        [Fact]
        public async Task GetByPdf_WithValidData_ShouldReturnCorrectValues()
        {
            const string httpResult =
                "{\"pdf\":{\"id\":\"11807505\",\"created\":\"2020-07-01T09:38:21+02:00\",\"incoming_id\":\"626880\",\"filename\":\"Eingangsrechnung 1234.pdf\",\"mimetype\":\"application\\/pdf\",\"filesize\":\"238280\",\"base64file\":\"VGVzdA==\"}}";
            const int id = 626880;
            const string expectedUri = "/api/incomings/626880/pdf";

            var expectedResult = new PurchaseInvoiceDocument
            {
                Id = 11807505,
                Created = DateTime.Parse("2020-07-01T09:38:21+02:00", CultureInfo.InvariantCulture),
                IncomingId = id,
                FileName = "Eingangsrechnung 1234.pdf",
                MimeType = "application/pdf",
                FileSize = 238280,
                FileContent = new[] { (byte)0x54, (byte)0x65, (byte)0x73, (byte)0x74 }
            };

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(httpResult));

            var sut = GetSystemUnderTest(http);
            var result = await sut.GetPdfAsync(id);

            A.CallTo(() => http.GetAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetFilteredList_ShouldReturnCorrectResult()
        {
            //arrange
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            var expectedRequestUri = new Uri("/api/incomings", UriKind.Relative);
            const string expectedRequestQuery = "supplier_id=50013&order_by=date+DESC&per_page=100&page=1";
            const string responseBody = "{\"incomings\":{\"incoming\":{\"id\":\"626880\",\"created\":\"2020-07-01T09:38:21+02:00\",\"updated\":\"2020-07-01T09:38:22+02:00\",\"supplier_id\":\"50013\",\"number\":\"1234\",\"client_number\":\"\",\"date\":\"2020-07-01\",\"due_date\":\"\",\"address\":\"Meyers AG\\r\\nHerr Jens  Maul\",\"status\":\"OPEN\",\"label\":\"\",\"note\":\"\",\"total_net\":\"-12.36\",\"total_gross\":\"-14.71\",\"currency_code\":\"EUR\",\"quote\":\"1\",\"paid_amount\":\"0\",\"open_amount\":\"-14.71\",\"expense_account_number\":\"\",\"category\":\"\",\"page_count\":\"1\",\"customfield\":\"\",\"incoming-property-values\":\"\"},\"@page\":\"1\",\"@per_page\":\"100\",\"@total\":\"1\"}}";

            A.CallTo(() => http.GetAsync(expectedRequestUri, expectedRequestQuery, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(responseBody));

            // ReSharper disable once RedundantArgumentDefaultValue
            var query = new Query<PurchaseInvoice, PurchaseInvoiceFilter>()
                .AddFilter(x => x.SupplierId, 50013)
                .AddSort(x => x.Date, SortOrder.Descending);

            var result = await sut.GetListAsync(query, CancellationToken.None);

            result.Page.Should().Be(1);
            result.ItemsPerPage.Should().Be(100);
            result.TotalItems.Should().Be(1);
            result.List.Should().SatisfyRespectively(
                first =>
                {
                    first.Id.Should().Be(626880);
                    first.Created.Should().Be(DateTime.Parse("2020-07-01T09:38:21+02:00", CultureInfo.InvariantCulture));
                    first.Updated.Should().Be(DateTime.Parse("2020-07-01T09:38:22+02:00", CultureInfo.InvariantCulture));
                    first.SupplierId.Should().Be(50013);
                });
        }
    }
}
