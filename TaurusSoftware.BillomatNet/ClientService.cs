using System;
using System.Threading;
using System.Threading.Tasks;
using TaurusSoftware.BillomatNet.Net;

namespace TaurusSoftware.BillomatNet
{
    public class ClientService : ServiceBase
    {
        public ClientService(Configuration configuration): base(configuration)
        {
        }

        public async Task<string> MyselfAsync(CancellationToken token = default(CancellationToken))
        {
            var httpClient = new HttpClient(Configuration.BillomatId, Configuration.ApiKey);
            var result = await httpClient.GetAsync(new Uri("/api/clients/myself", UriKind.Relative), token);
            return result;
        }
    }
}
