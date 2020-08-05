// Licensed to the .NET Foundation under one or more agreements.
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
        public async Task GetList_WithValidInputValue_WithEnumQuery_ShouldReturnCorrectValues()
        {
            //arrange
            var expectedRequestUri = new Uri("/api/invoice-payments", UriKind.Relative);
            const string responseBody = "{\"invoice-payments\":{\"invoice-payment\":[{\"id\":\"872254\",\"created\":\"2015-06-04T09:51:54+02:00\",\"invoice_id\":\"1220304\",\"user_id\":\"52821\",\"date\":\"2015-05-04\",\"amount\":\"-17\",\"comment\":\"\",\"transaction_purpose\":\"\",\"currency_code\":\"\",\"quote\":\"1\",\"type\":\"\",\"type\":\"BANK_CARD\",\"customfield\":\"\"},{\"id\":\"872269\",\"created\":\"2015-06-04T10:05:37+02:00\",\"invoice_id\":\"1322225\",\"user_id\":\"52821\",\"date\":\"2015-05-04\",\"amount\":\"212.33\",\"comment\":\"\",\"transaction_purpose\":\"\",\"currency_code\":\"\",\"quote\":\"1\",\"type\":\"\",\"type\":\"CASH\",\"customfield\":\"\"},{\"id\":\"872282\",\"created\":\"2015-06-04T10:09:55+02:00\",\"invoice_id\":\"1298716\",\"user_id\":\"52821\",\"date\":\"2015-05-04\",\"amount\":\"495\",\"comment\":\"\",\"transaction_purpose\":\"\",\"currency_code\":\"\",\"quote\":\"1\",\"type\":\"\",\"type\":\"DEBIT\",\"customfield\":\"\"}],\"@page\":\"1\",\"@per_page\":\"100\",\"@total\":\"3\"}}";
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
                    Type = PaymentType.Cash
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
                    Type = PaymentType.Debit
                }
            };
            const string expectedQuery = "type=BANK_CARD,CASH,DEBIT&per_page=100&page=1";

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(expectedRequestUri, expectedQuery, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(responseBody));

            var sut = GetSystemUnderTest(http);
            var query = new Query<InvoicePayment, InvoicePaymentFilter>()
                .AddFilter(x => x.Type, new List<PaymentType> { PaymentType.BankCard, PaymentType.Cash, PaymentType.Debit });

            //act
            var result = await sut.GetPaymentListAsync(query);

            //assert
            A.CallTo(() => http.GetAsync(expectedRequestUri, expectedQuery, A<CancellationToken>.Ignored))
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
            var result = await sut.GetPaymentListAsync(query);

            //assert
            A.CallTo(() => http.GetAsync(expectedRequestUri, expectedQuery, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            result.List.Should()
                .HaveCount(expectedResult.Count)
                .And.ContainItemsInOrderUsingComparer(expectedResult, new InvoicePaymentEqualityComparer());
        }

        [Fact]
        public async Task GetList_WithInvalidCredentials_ShouldThrowNotAuthorizedException()
        {
            // arrange
            var expectedRequestUri = new Uri("/api/invoice-payments", UriKind.Relative);
            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(expectedRequestUri, null, A<CancellationToken>.Ignored))
                .ThrowsAsync(ExceptionFactory.CreateNotAuthorizedException);

            var sut = GetSystemUnderTest(http);

            //act and assert
            await Assert.ThrowsAsync<NotAuthorizedException>(() => sut.GetPaymentListAsync());
            A.CallTo(() => http.GetAsync(expectedRequestUri, null, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetList_WithNonMatchingFilter_ShouldReturnEmptyList()
        {
            //arrange
            var expectedRequestUri = new Uri("/api/invoice-payments", UriKind.Relative);
            const string responseBody = "{\"invoice-payments\":{\"@page\":\"1\",\"@per_page\":\"100\",\"@total\":\"0\"}}";
            const string expectedRequestQuery = "invoice_id=1322226&per_page=100&page=1";

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(expectedRequestUri, expectedRequestQuery, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(responseBody));

            var sut = GetSystemUnderTest(http);

            //act
            var query = new Query<InvoicePayment, InvoicePaymentFilter>().AddFilter(x => x.InvoiceId, 1322226);
            var result = await sut.GetPaymentListAsync(query);

            //assert
            A.CallTo(() => http.GetAsync(expectedRequestUri, expectedRequestQuery, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            Assert.Equal(0, result.TotalItems);
        }

        [Fact]
        public async Task GetById_WithValidInputValue_ShouldReturnCorrectValue()
        {
            //arrange
            const int id = 872254;
            var expectedRequestUri = new Uri($"/api/invoice-payments/{id}", UriKind.Relative);
            const string responseBody = "{\"invoice-payment\":{\"id\":\"872254\",\"created\":\"2015-06-04T09:51:54+02:00\",\"invoice_id\":\"1220304\",\"user_id\":\"52821\",\"date\":\"2015-05-04\",\"amount\":\"-17\",\"comment\":\"\",\"transaction_purpose\":\"\",\"currency_code\":\"\",\"quote\":\"1\",\"type\":\"\",\"customfield\":\"\"}}";
            var expectedResult = new InvoicePayment
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
            };

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(expectedRequestUri, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(responseBody));

            var sut = GetSystemUnderTest(http);

            //act
            var result = await sut.GetPaymentByIdAsync(id);

            //assert
            A.CallTo(() => http.GetAsync(expectedRequestUri, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            result.Should().BeEquivalentUsingComparerTo(expectedResult, new InvoicePaymentEqualityComparer());
        }

        [Fact]
        public async Task GetById_WithInvalidInputData_ShouldThrowArgumentException()
        {
            // arrange
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            //act and assert
            await Assert.ThrowsAsync<ArgumentException>(() => sut.GetPaymentByIdAsync(0));
        }

        [Fact]
        public async Task GetById_WithInvalidCredentials_ShouldThrowNotAuthorizedException()
        {
            // arrange
            const int id = 1;
            var expectedRequestUri = new Uri($"/api/invoice-payments/{id}", UriKind.Relative);
            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(expectedRequestUri, A<CancellationToken>.Ignored))
                .ThrowsAsync(ExceptionFactory.CreateNotAuthorizedException);

            var sut = GetSystemUnderTest(http);

            //act and assert
            await Assert.ThrowsAsync<NotAuthorizedException>(() => sut.GetPaymentByIdAsync(id));
            A.CallTo(() => http.GetAsync(expectedRequestUri, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetById_WithInvalidId_ShouldReturnNull()
        {
            // arrange
            const int id = 1;
            var expectedRequestUri = new Uri($"/api/invoice-payments/{id}", UriKind.Relative);
            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(expectedRequestUri, A<CancellationToken>.Ignored))
                .ThrowsAsync(ExceptionFactory.CreateNotFoundException);

            var sut = GetSystemUnderTest(http);

            //act and assert
            var result = await sut.GetPaymentByIdAsync(id);
            A.CallTo(() => http.GetAsync(expectedRequestUri, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            result.Should().BeNull();
        }

        [Fact]
        public async Task Create_WithCorrectValues_ShouldCreateCommentAndReturnCorrectValues()
        {
            //arrange
            var expectedRequestUri = new Uri("/api/invoice-payments", UriKind.Relative);
            const string expectedRequestBody = "{\"invoice-payment\":{\"id\":\"0\",\"created\":\"0001-01-01T00:00:00.0000000\",\"invoice_id\":\"7506691\",\"user_id\":\"7506691\",\"date\":\"0001-01-01\",\"amount\":\"17\",\"comment\":\"\",\"transaction_purpose\":\"\",\"currency_code\":\"\",\"quote\":\"1\",\"type\":\"BANK_CARD\",\"mark_invoice_as_paid\":\"0\"}}";
            const string responseBody = "{\"invoice-payment\":{\"id\":\"872254\",\"created\":\"2015-06-04T09:51:54+02:00\",\"invoice_id\":\"7506691\",\"user_id\":\"7506691\",\"date\":\"2015-05-04\",\"amount\":\"17\",\"comment\":\"\",\"transaction_purpose\":\"\",\"currency_code\":\"\",\"quote\":\"1\",\"type\":\"BANK_CARD\",\"customfield\":\"\"}}";

            var model = new InvoicePayment{ Created = DateTime.Parse("0001-01-01T00:00:00.0000000", CultureInfo.InvariantCulture), InvoiceId = 7506691, UserId = 7506691, Date = DateTime.Parse("0001-01-01", CultureInfo.InvariantCulture), Amount = 17f, Comment = "", TransactionPurpose = "", CurrencyCode = "", Quote = 1, Type = PaymentType.BankCard };
            var expectedResult = new InvoicePayment{ Id = 872254, Created = DateTime.Parse("2015-06-04T09:51:54+02:00", CultureInfo.InvariantCulture), InvoiceId = 7506691, UserId = 7506691, Date = DateTime.Parse("2015-05-04", CultureInfo.InvariantCulture), Amount = 17f, Comment = "", TransactionPurpose = "", CurrencyCode = "", Quote = 1, Type = PaymentType.BankCard, MarkInvoiceAsPaid = true };

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.PostAsync(expectedRequestUri, expectedRequestBody, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(responseBody));

            var sut = GetSystemUnderTest(http);

            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            //act
            var result = await sut.CreatePaymentAsync(model);

            // assert
            A.CallTo(() => http.PostAsync(expectedRequestUri, expectedRequestBody, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            result.Should().BeEquivalentUsingComparerTo(expectedResult, new InvoicePaymentEqualityComparer());
        }

        [Fact]
        public async Task Create_WithInvalidInputValue_ShouldThrowArgumentException()
        {
            // arrange
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            // act and assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => sut.CreatePaymentAsync(null));
            await Assert.ThrowsAsync<ArgumentException>(() => sut.CreatePaymentAsync(new InvoicePayment()));
            await Assert.ThrowsAsync<ArgumentException>(() => sut.CreatePaymentAsync(new InvoicePayment { Id = 1 }));
        }

        [Fact]
        public async Task Create_WithInvalidApiKey_ShouldThrowNotAuthorizedException()
        {
            //arrange
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            var expectedRequestUri = new Uri("/api/invoice-payments", UriKind.Relative);
            A.CallTo(() => http.PostAsync(expectedRequestUri, A<string>.Ignored, A<CancellationToken>.Ignored))
                .ThrowsAsync(ExceptionFactory.CreateNotAuthorizedException);

            var model = new InvoicePayment { InvoiceId = 1, Amount = 17f };

            await Assert.ThrowsAsync<NotAuthorizedException>(() => sut.CreatePaymentAsync(model));
            A.CallTo(() => http.PostAsync(expectedRequestUri, A<string>.Ignored, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();
        }
    }
}
