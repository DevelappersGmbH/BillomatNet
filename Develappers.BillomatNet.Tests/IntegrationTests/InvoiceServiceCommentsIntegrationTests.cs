// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Develappers.BillomatNet.Queries;
using Xunit;
using InvoiceComment = Develappers.BillomatNet.Types.InvoiceComment;

namespace Develappers.BillomatNet.Tests.IntegrationTests
{
    public class InvoiceServiceCommentsIntegrationTests : IntegrationTestBase<InvoiceService>
    {
        public InvoiceServiceCommentsIntegrationTests() : base(c => new InvoiceService(c))
        {
        }

        [Fact]
        public async Task GetCommentListAsync()
        {
            var result = await SystemUnderTest.GetCommentListAsync(new Query<InvoiceComment, InvoiceCommentFilter>().AddFilter(x => x.InvoiceId, 1322225));
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetCommentListAsyncWhenNotAuthorized()
        {
            Configuration.ApiKey = "ajfkjeinodafkejlkdsjklj";
            await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.GetCommentListAsync(new Query<InvoiceComment, InvoiceCommentFilter>().AddFilter(x => x.InvoiceId, 1322225)));
        }

        [Fact]
        public async Task GetCommentById()
        {
            var result = await SystemUnderTest.GetCommentByIdAsync(4662801);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CreateComment()
        {
            var result = await SystemUnderTest.CreateCommentAsync(new InvoiceComment { InvoiceId = 7506691, Comment = "Test Comment" });
            Assert.NotNull(result);
        }
    }
}
