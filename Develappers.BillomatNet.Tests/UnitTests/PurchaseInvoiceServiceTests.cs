// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Api.Net;
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

            var expectedResult = new PurchaseInvoice()
            {
                Id = id,
                Created = DateTime.Parse("2020-07-01T09:38:21+02:00", CultureInfo.InvariantCulture),
                Updated = DateTime.Parse("2020-07-01T09:38:22+02:00", CultureInfo.InvariantCulture),
                SupplierId = 50013,
                Number = "1234",
                ClientNumber = null,
                Date = DateTime.Parse("2020-07-01", CultureInfo.InvariantCulture),
                Address = "Meyers AG\r\nHerr Jens  Maul",
                Status = InvoiceStatus.Open,
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
    }
}
