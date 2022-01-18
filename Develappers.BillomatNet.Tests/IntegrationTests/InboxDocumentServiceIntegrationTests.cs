// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Xunit;

namespace Develappers.BillomatNet.Tests.IntegrationTests
{
    public class InboxDocumentServiceIntegrationTests : IntegrationTestBase<InboxDocumentService>
    {
        public InboxDocumentServiceIntegrationTests() : base(c => new BillomatClient(c).InboxDocuments)
        {
        }

        [Fact]
        public async Task GetInboxDocumentById()
        {
            var result = await SystemUnderTest.GetByIdAsync(729809);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetInboxDocumentList()
        {
            var result = await SystemUnderTest.GetListAsync();
            Assert.NotNull(result);
        }
    }
}
