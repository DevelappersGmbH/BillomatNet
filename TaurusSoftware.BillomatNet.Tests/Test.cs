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
    }
}
