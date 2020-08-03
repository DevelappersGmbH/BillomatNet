// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Develappers.BillomatNet.Queries;
using Develappers.BillomatNet.Types;
using Xunit;
using InvoicePayment = Develappers.BillomatNet.Types.InvoicePayment;

namespace Develappers.BillomatNet.Tests.IntegrationTests
{
    public class InvoiceServicePaymentsIntegrationTests : IntegrationTestBase<InvoiceService>
    {
        public InvoiceServicePaymentsIntegrationTests() : base(c => new InvoiceService(c))
        {
        }

        [Fact]
        public async Task GetPaymentListAsync()
        {
            var result = await SystemUnderTest.GetPaymentListAsync();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetPaymentListAsync_WithQuery()
        {
            var result = await SystemUnderTest.GetPaymmentListAsync(new Query<InvoicePayment, InvoicePaymentFilter>().AddFilter(x => x.Type, new List<PaymentType> { PaymentType.BankCard }));
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetPaymentByIdAsync()
        {
            var result = await SystemUnderTest.GetPaymentByIdAsync(872254);
            Assert.NotNull(result);
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task CreatePaymentAsync()
        {
            var payment = new InvoicePayment { InvoiceId = 7506691, UserId = 7506691, Amount = 17f, Comment = "", TransactionPurpose = "", CurrencyCode = "", Quote = 1, Type = PaymentType.BankCard };

            var result = await SystemUnderTest.CreatePaymentAsync(payment);
            Assert.NotNull(result);
        }
    }
}
