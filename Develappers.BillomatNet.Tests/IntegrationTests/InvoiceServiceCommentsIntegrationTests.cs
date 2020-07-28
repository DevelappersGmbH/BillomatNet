using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Develappers.BillomatNet.Api;
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
            var result = await SystemUnderTest.GetCommentListAsync(new Query<InvoiceComment, InvoiceCommentFilter>().AddFilter(x => x.InvoiceId, 1298716));
            Assert.NotNull(result);
        }
    }
}
