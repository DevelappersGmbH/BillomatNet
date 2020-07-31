// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

    }
}
