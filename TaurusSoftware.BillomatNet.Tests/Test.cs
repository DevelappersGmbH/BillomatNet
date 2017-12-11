using System.Threading.Tasks;
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
            var r = await s.MyselfAsync();
            Assert.True(true);
        }
    }
}
