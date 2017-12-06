using System.Threading.Tasks;
using Xunit;

namespace TaurusSoftware.BillomatNet.Tests
{
    public class Test
    {
        [Fact]
        public async Task Testit()
        {

            var s = new ClientService(null);
            var r = await s.MyselfAsync();
            Assert.True(true);
        }
    }
}
