using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaurusSoftware.BillomatNet.Model;
using TaurusSoftware.BillomatNet.Net;

namespace TaurusSoftware.BillomatNet
{
    public class ClientService : ServiceBase
    {
        public ClientService(Configuration configuration): base(configuration)
        {
        }

        public async Task<Account> MyselfAsync(CancellationToken token = default(CancellationToken))
        {
            var httpClient = new HttpClient(Configuration.BillomatId, Configuration.ApiKey);
            var httpResponse = await httpClient.GetAsync(new Uri("/api/clients/myself", UriKind.Relative), token);
            var jsonModel = JsonConvert.DeserializeObject<Json.AccountWrapper>(httpResponse);
            return jsonModel.ToDomain();
        }

        public Task<PagedList<Client>> ListAsync(int page = 1, int perPage = 100, CancellationToken token = default(CancellationToken))
        {
            return ListAsync(null, null, page, perPage, token);
        }

        public async Task<PagedList<Client>> ListAsync(ClientFilter filter, ClientSort sort, int page = 1, int perPage = 100, CancellationToken token = default(CancellationToken))
        {
            var filterQuery = filter.ToQueryString();
           
            var httpClient = new HttpClient(Configuration.BillomatId, Configuration.ApiKey);
            var httpResponse = await httpClient.GetAsync(new Uri("/api/clients", UriKind.Relative), filterQuery, token);
            return null;
            //var jsonModel = JsonConvert.DeserializeObject<Json.AccountWrapper>(httpResponse);
            //return jsonModel.ToDomain();
        }
    }
}
