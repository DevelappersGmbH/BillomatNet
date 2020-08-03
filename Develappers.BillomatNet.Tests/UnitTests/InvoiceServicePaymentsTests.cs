// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
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
    public class InvoiceServicePaymentsTests : UnitTestBase<InvoiceService>
    {
        [Fact]
        public async Task GetList_WithValidInputValue_ShouldReturnCorrectValues()
        {
            //arrange
            var expectedRequestUri = new Uri("/api/invoice-payments", UriKind.Relative);
            const string responseBody = "{\"invoice-payments\":{\"invoice-payment\":[{\"id\":\"872254\",\"created\":\"2015-06-04T09:51:54+02:00\",\"invoice_id\":\"1220304\",\"user_id\":\"52821\",\"date\":\"2015-05-04\",\"amount\":\"-17\",\"comment\":\"\",\"transaction_purpose\":\"\",\"currency_code\":\"\",\"quote\":\"1\",\"type\":\"\",\"customfield\":\"\"},{\"id\":\"872269\",\"created\":\"2015-06-04T10:05:37+02:00\",\"invoice_id\":\"1322225\",\"user_id\":\"52821\",\"date\":\"2015-05-04\",\"amount\":\"212.33\",\"comment\":\"\",\"transaction_purpose\":\"\",\"currency_code\":\"\",\"quote\":\"1\",\"type\":\"\",\"customfield\":\"\"},{\"id\":\"872282\",\"created\":\"2015-06-04T10:09:55+02:00\",\"invoice_id\":\"1298716\",\"user_id\":\"52821\",\"date\":\"2015-05-04\",\"amount\":\"495\",\"comment\":\"\",\"transaction_purpose\":\"\",\"currency_code\":\"\",\"quote\":\"1\",\"type\":\"\",\"customfield\":\"\"}],\"@page\":\"1\",\"@per_page\":\"100\",\"@total\":\"3\"}}";
            var expectedResult = new List<InvoicePayment>
            {
                new InvoicePayment
                {
                    Id = 872254,
                    Created = DateTime.Parse("2015-06-04T09:51:54+02:00", CultureInfo.InvariantCulture),
                    InvoiceId = 1220304,
                    UserId = 52821,
                    Date = DateTime.Parse("2015-05-04", CultureInfo.InvariantCulture),
                    Amount = -17f,
                    Comment = "",
                    TransactionPurpose = "",
                    CurrencyCode = "",
                    Quote = 1,
                    MarkInvoiceAsPaid = true
                },
                new InvoicePayment
                {
                    Id = 872269,
                    Created = DateTime.Parse("2015-06-04T10:05:37+02:00", CultureInfo.InvariantCulture),
                    InvoiceId = 1322225,
                    UserId = 52821,
                    Date = DateTime.Parse("2015-05-04", CultureInfo.InvariantCulture),
                    Amount = 212.33f,
                    Comment = "",
                    TransactionPurpose = "",
                    CurrencyCode = "",
                    Quote = 1,
                    MarkInvoiceAsPaid = true
                },
                new InvoicePayment
                {
                    Id = 872282,
                    Created = DateTime.Parse("2015-06-04T10:09:55+02:00", CultureInfo.InvariantCulture),
                    InvoiceId = 1298716,
                    UserId = 52821,
                    Date = DateTime.Parse("2015-05-04", CultureInfo.InvariantCulture),
                    Amount = 495f,
                    Comment = "",
                    TransactionPurpose = "",
                    CurrencyCode = "",
                    Quote = 1,
                    MarkInvoiceAsPaid = true
                }
            };

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(expectedRequestUri, null, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(responseBody));

            var sut = GetSystemUnderTest(http);

            //act
            var result = await sut.GetPaymentListAsync();

            //assert
            A.CallTo(() => http.GetAsync(expectedRequestUri, null, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            result.List.Should()
                .HaveCount(expectedResult.Count)
                .And.ContainItemsInOrderUsingComparer(expectedResult, new InvoicePaymentEqualityComparer());
        }

        [Fact]
        public async Task GetListWithQuery_WithValidInputValue_ShouldReturnCorrectValues()
        {
            //arrange
            var expectedRequestUri = new Uri("/api/invoice-payments", UriKind.Relative);
            const string responseBody = "{\"invoice-payments\":{\"invoice-payment\":[{\"id\":\"872254\",\"created\":\"2015-06-04T09:51:54+02:00\",\"invoice_id\":\"1220304\",\"user_id\":\"52821\",\"date\":\"2015-05-04\",\"amount\":\"-17\",\"comment\":\"\",\"transaction_purpose\":\"\",\"currency_code\":\"\",\"quote\":\"1\",\"type\":\"BANK_CARD\",\"customfield\":\"\"},{\"id\":\"872269\",\"created\":\"2015-06-04T10:05:37+02:00\",\"invoice_id\":\"1322225\",\"user_id\":\"52821\",\"date\":\"2015-05-04\",\"amount\":\"212.33\",\"comment\":\"\",\"transaction_purpose\":\"\",\"currency_code\":\"\",\"quote\":\"1\",\"type\":\"BANK_CARD\",\"customfield\":\"\"},{\"id\":\"872282\",\"created\":\"2015-06-04T10:09:55+02:00\",\"invoice_id\":\"1298716\",\"user_id\":\"52821\",\"date\":\"2015-05-04\",\"amount\":\"495\",\"comment\":\"\",\"transaction_purpose\":\"\",\"currency_code\":\"\",\"quote\":\"1\",\"type\":\"BANK_CARD\",\"customfield\":\"\"}],\"@page\":\"1\",\"@per_page\":\"100\",\"@total\":\"3\"}}";
            var expectedResult = new List<InvoicePayment>
            {
                new InvoicePayment
                {
                    Id = 872254,
                    Created = DateTime.Parse("2015-06-04T09:51:54+02:00", CultureInfo.InvariantCulture),
                    InvoiceId = 1220304,
                    UserId = 52821,
                    Date = DateTime.Parse("2015-05-04", CultureInfo.InvariantCulture),
                    Amount = -17f,
                    Comment = "",
                    TransactionPurpose = "",
                    CurrencyCode = "",
                    Quote = 1,
                    MarkInvoiceAsPaid = true,
                    Type = PaymentType.BankCard
                },
                new InvoicePayment
                {
                    Id = 872269,
                    Created = DateTime.Parse("2015-06-04T10:05:37+02:00", CultureInfo.InvariantCulture),
                    InvoiceId = 1322225,
                    UserId = 52821,
                    Date = DateTime.Parse("2015-05-04", CultureInfo.InvariantCulture),
                    Amount = 212.33f,
                    Comment = "",
                    TransactionPurpose = "",
                    CurrencyCode = "",
                    Quote = 1,
                    MarkInvoiceAsPaid = true,
                    Type = PaymentType.BankCard
                },
                new InvoicePayment
                {
                    Id = 872282,
                    Created = DateTime.Parse("2015-06-04T10:09:55+02:00", CultureInfo.InvariantCulture),
                    InvoiceId = 1298716,
                    UserId = 52821,
                    Date = DateTime.Parse("2015-05-04", CultureInfo.InvariantCulture),
                    Amount = 495f,
                    Comment = "",
                    TransactionPurpose = "",
                    CurrencyCode = "",
                    Quote = 1,
                    MarkInvoiceAsPaid = true,
                    Type = PaymentType.BankCard
                }
            };

            const string expectedQuery = "type=BANK_CARD&per_page=100&page=1";

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(expectedRequestUri, expectedQuery, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(responseBody));

            var sut = GetSystemUnderTest(http);
            var query = new Query<InvoicePayment, InvoicePaymentFilter>().AddFilter(x => x.Type, new List<PaymentType> { PaymentType.BankCard });

            //act
            var result = await sut.GetPaymmentListAsync(query);

            //assert
            A.CallTo(() => http.GetAsync(expectedRequestUri, expectedQuery, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            result.List.Should()
                .HaveCount(expectedResult.Count)
                .And.ContainItemsInOrderUsingComparer(expectedResult, new InvoicePaymentEqualityComparer());
        }
    }
}
