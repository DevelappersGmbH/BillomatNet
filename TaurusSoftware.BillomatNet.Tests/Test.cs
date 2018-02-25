using System.Threading;
using System.Threading.Tasks;
using TaurusSoftware.BillomatNet.Queries;
using TaurusSoftware.BillomatNet.Types;
using Xunit;

namespace TaurusSoftware.BillomatNet.Tests
{
    public class Test
    {
        [Fact]
        public async Task Testit()
        {
            var config = Helpers.GetTestConfiguration();
           
            var s = new ClientService(config);

            var q = new Query<Client, ClientFilter>()
                .AddFilter(x => x.Name, "Regiofaktur")
                .AddSort(x => x.City, SortOrder.Ascending);

            var r = await s.GetListAsync(q, CancellationToken.None);

            var c = await s.GetById(1227912);
            Assert.True(true);
        }

        [Fact]
        public async Task Testit2()
        {
            var config = Helpers.GetTestConfiguration();

            var s = new ArticleService(config);

            var q = new Query<Article, ArticleFilter>()
                //.AddFilter(x => x.ArticleNumber, "ART")
                .AddSort(x => x.SalesPrice, SortOrder.Descending);

            var r = await s.GetListAsync(q, CancellationToken.None);

            var c = await s.GetById(434867);

            var d = await s.GetPropertyListAsync();

            var l = await s.GetPropertyById(434532);

            Assert.True(true);
        }
    }
}
