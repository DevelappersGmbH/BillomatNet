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
using Contact = TaurusSoftware.BillomatNet.Types.Contact;

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

        public async Task<Types.PagedList<Contact>> GetContactListAsync(int clientId, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetListAsync<ContactListWrapper>("/api/contacts", $"client_id={clientId}", token);
            return jsonModel.ToDomain();
        }

        public async Task<Types.PagedList<Client>> GetListAsync(Query<Client, ClientFilter> query, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetListAsync<ClientListWrapper>("/api/clients", QueryString.For(query), token);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Returns an client by it's id. 
        /// </summary>
        /// <param name="id">The id of the client.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The client or null if not found.</returns>
        /// <exception cref="NotAuthorizedException">Thrown when the client is not accessible.</exception>
        public async Task<Client> GetById(int id, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetItemByIdAsync<ClientWrapper>($"/api/clients/{id}", token);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Returns an contact by it's id. 
        /// </summary>
        /// <param name="id">The id of the contact.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The client or null if not found.</returns>
        public async Task<Contact> GetContactById(int id, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetItemByIdAsync<ContactWrapper>($"/api/contacts/{id}", token);
            return jsonModel.ToDomain();
        }
    }
}
