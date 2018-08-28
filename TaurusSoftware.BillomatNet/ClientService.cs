using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaurusSoftware.BillomatNet.Api;
using TaurusSoftware.BillomatNet.Api.Net;
using TaurusSoftware.BillomatNet.Helpers;
using TaurusSoftware.BillomatNet.Queries;
using Account = TaurusSoftware.BillomatNet.Types.Account;
using Client = TaurusSoftware.BillomatNet.Types.Client;

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
            var jsonModel = JsonConvert.DeserializeObject<AccountWrapper>(httpResponse);
            return jsonModel.ToDomain();
        }

        public Task<Types.PagedList<Client>> GetListAsync(CancellationToken token = default(CancellationToken))
        {
            return GetListAsync(null, token);
        }

        public async Task<Types.PagedList<Client>> GetListAsync(Query<Client, ClientFilter> query, CancellationToken token = default(CancellationToken))
        {
            var httpClient = new HttpClient(Configuration.BillomatId, Configuration.ApiKey);
            var httpResponse = await httpClient.GetAsync(new Uri("/api/clients", UriKind.Relative), QueryString.For(query), token);
            var jsonModel = JsonConvert.DeserializeObject<ClientListWrapper>(httpResponse);
            return jsonModel.ToDomain();
        }

        public async Task<Client> GetById(int id, CancellationToken token = default(CancellationToken))
        {
            var httpClient = new HttpClient(Configuration.BillomatId, Configuration.ApiKey);
            var httpResponse = await httpClient.GetAsync(new Uri($"/api/clients/{id}", UriKind.Relative), token);
            var jsonModel = JsonConvert.DeserializeObject<ClientWrapper>(httpResponse);
            return jsonModel.ToDomain();
        }
    }
}
