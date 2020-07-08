using System;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Api.Net;
using Develappers.BillomatNet.Helpers;
using Develappers.BillomatNet.Queries;
using Newtonsoft.Json;
using Account = Develappers.BillomatNet.Types.Account;
using Client = Develappers.BillomatNet.Types.Client;
using Contact = Develappers.BillomatNet.Types.Contact;
using TagCloudItem = Develappers.BillomatNet.Types.TagCloudItem;

namespace Develappers.BillomatNet
{
    public class ClientService : ServiceBase
    {
        public ClientService(Configuration configuration): base(configuration)
        {
        }

        public async Task<Account> MyselfAsync(CancellationToken token = default(CancellationToken))
        {
            var httpClient = new HttpClient(Configuration.BillomatId, Configuration.ApiKey);
            var httpResponse = await httpClient.GetAsync(new Uri("/api/clients/myself", UriKind.Relative), token).ConfigureAwait(false);
            var jsonModel = JsonConvert.DeserializeObject<AccountWrapper>(httpResponse);
            return jsonModel.ToDomain();
        }

        public Task<Types.PagedList<Client>> GetListAsync(CancellationToken token = default(CancellationToken))
        {
            return GetListAsync(null, token);
        }

        public async Task<Types.PagedList<Contact>> GetContactListAsync(int clientId, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetListAsync<ContactListWrapper>("/api/contacts", $"client_id={clientId}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }


        public async Task<Types.PagedList<Client>> GetListAsync(Query<Client, ClientFilter> query, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetListAsync<ClientListWrapper>("/api/clients", QueryString.For(query), token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Returns an client by it's id. 
        /// </summary>
        /// <param name="id">The id of the client.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The client or null if not found.</returns>
        /// <exception cref="NotAuthorizedException">Thrown when the client is not accessible.</exception>
        public async Task<Client> GetByIdAsync(int id, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetItemByIdAsync<ClientWrapper>($"/api/clients/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Returns an contact by it's id. 
        /// </summary>
        /// <param name="id">The id of the contact.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The client or null if not found.</returns>
        public async Task<Contact> GetContactByIdAsync(int id, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetItemByIdAsync<ContactWrapper>($"/api/contacts/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Retrieves the avatar for a specific contact.
        /// </summary>
        /// <param name="id">The id of the contact.</param>
        /// <param name="size">The size in pixels.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns></returns>
        public async Task<byte[]> GetContactAvatarByIdAsync(int id, int size, CancellationToken token = default(CancellationToken))
        {
            var httpClient = new HttpClient(Configuration.BillomatId, Configuration.ApiKey);
            return await httpClient.GetBytesAsync(new Uri($"/api/contacts/{id}/avatar?size={size}", UriKind.Relative), token).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves the customer tag cloud.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the paged list of tag cloud items.
        /// </returns>
        public async Task<Types.PagedList<TagCloudItem>> GetTagCloudAsync(CancellationToken token = default(CancellationToken))
        {
            // do we need paging possibilities in parameters? 100 items in tag cloud should be enough, shouldn't it?
            var jsonModel = await GetListAsync<ClientTagCloudItemListWrapper>("/api/client-tags", null, token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }
    }
}
