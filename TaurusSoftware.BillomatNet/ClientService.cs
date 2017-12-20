using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaurusSoftware.BillomatNet.Api;
using TaurusSoftware.BillomatNet.Api.Net;
using TaurusSoftware.BillomatNet.Helpers;
using TaurusSoftware.BillomatNet.Model;
using Account = TaurusSoftware.BillomatNet.Model.Account;
using Client = TaurusSoftware.BillomatNet.Model.Client;

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

        public Task<PagedList<Client>> ListAsync(CancellationToken token = default(CancellationToken))
        {
            return ListAsync(null, token);
        }

        public async Task<PagedList<Client>> ListAsync(ClientFilterSortOptions options, CancellationToken token = default(CancellationToken))
        {
            var httpClient = new HttpClient(Configuration.BillomatId, Configuration.ApiKey);
            var httpResponse = await httpClient.GetAsync(new Uri("/api/clients", UriKind.Relative), QueryString.For(options), token);
            var jsonModel = JsonConvert.DeserializeObject<ClientListWrapper>(httpResponse);
            return jsonModel.ToDomain();
        }
    }
}
