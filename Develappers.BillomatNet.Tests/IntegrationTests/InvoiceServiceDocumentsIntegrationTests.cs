// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Xunit;

namespace Develappers.BillomatNet.Tests.IntegrationTests
{
    public class InvoiceServiceDocumentsIntegrationTests : IntegrationTestBase<InvoiceService>
    {
        public InvoiceServiceDocumentsIntegrationTests() : base(c => new InvoiceService(c))
        {
        }

        [Fact]
        public async Task GetInvoicePdf()
        {
            var result = await SystemUnderTest.GetPdfAsync(1322705);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SendInvoiceEmail()
        {

        }
    }
}
