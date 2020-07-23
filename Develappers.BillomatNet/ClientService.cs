using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Api.Net;
using Develappers.BillomatNet.Helpers;
using Develappers.BillomatNet.Queries;
using Newtonsoft.Json;
using Account = Develappers.BillomatNet.Types.Account;
using Client = Develappers.BillomatNet.Types.Client;
using ClientProperty = Develappers.BillomatNet.Types.ClientProperty;
using Contact = Develappers.BillomatNet.Types.Contact;
using TagCloudItem = Develappers.BillomatNet.Types.TagCloudItem;

namespace Develappers.BillomatNet
{
    public class ClientService : ServiceBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public ClientService(Configuration configuration): base(configuration)
        {
        }

        /// <summary>
        /// Returns all information of your account.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The account data.</returns>
        public async Task<Account> MyselfAsync(CancellationToken token = default)
        {
            var httpClient = HttpClientFactory.Invoke();
            var httpResponse = await httpClient.GetAsync(new Uri("/api/clients/myself", UriKind.Relative), token).ConfigureAwait(false);
            var jsonModel = JsonConvert.DeserializeObject<AccountWrapper>(httpResponse);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Retrieves a list of all clients.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The client list or null if not found.</returns>
        public Task<Types.PagedList<Client>> GetListAsync(CancellationToken token = default)
        {
            return GetListAsync(null, token);
        }

        /// <summary>
        /// Retrieves a list of all clients.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The client list or null if not found.</returns>
        public async Task<Types.PagedList<Client>> GetListAsync(Query<Client, ClientFilter> query, CancellationToken token = default)
        {
            var jsonModel = await GetListAsync<ClientListWrapper>("/api/clients", QueryString.For(query), token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Returns an client by it's ID. 
        /// </summary>
        /// <param name="id">The ID of the client.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The client or null if not found.</returns>
        /// <exception cref="NotAuthorizedException">Thrown when the client is not accessible.</exception>
        public async Task<Client> GetByIdAsync(int id, CancellationToken token = default)
        {
            var jsonModel = await GetItemByIdAsync<ClientWrapper>($"/api/clients/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Returns a client property by it's ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the client property.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        public async Task<ClientProperty> GetPropertyById(int id, CancellationToken token = default)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid client property id", nameof(id));
            }
            var jsonModel = await GetItemByIdAsync<ClientPropertyWrapper>($"/api/client-property-values/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Retrieves a list of all properties.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result returns the client property list.
        /// </returns>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        public Task<Types.PagedList<ClientProperty>> GetPropertyListAsync(CancellationToken token = default)
        {
            return GetPropertyListAsync(null, token);
        }

        /// <summary>
        /// Retrieves a list of all properties.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result returns the client property list.
        /// </returns>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        public async Task<Types.PagedList<ClientProperty>> GetPropertyListAsync(Query<ClientProperty, ClientPropertyFilter> query, CancellationToken token = default)
        {
            var jsonModel = await GetListAsync<ClientPropertyListWrapper>("/api/client-property-values", QueryString.For(query), token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Creates / Edits a client property.
        /// </summary>
        /// <param name="model">The client property.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the new client propery.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<ClientProperty> EditAsync(ClientProperty model, CancellationToken token = default)
        {
            if (model.ClientId == 0 || model.ClientPropertyId == 0 || model.Value == null)
            {
                throw new ArgumentException("client or a value of the client is null", nameof(model));
            }
            if (model.Id != 0)
            {
                throw new ArgumentException("invalid client id", nameof(model));
            }
            var wrappedModel = new ClientPropertyWrapper
            {
                ClientProperty = model.ToApi()
            };
            try
            {
                var jsonModel = await PostAsync("/api/client-property-values", wrappedModel, token).ConfigureAwait(false);
                return jsonModel.ToDomain();
            }
            catch (WebException wex)
                when (wex.Status == WebExceptionStatus.ProtocolError && (wex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ArgumentException("wrong input parameter", nameof(model), wex);
            }
        }
 
        /// <summary>
        /// Retrieves the customer tag cloud.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the paged list of tag cloud items.
        /// </returns>
        public async Task<Types.PagedList<TagCloudItem>> GetTagCloudAsync(CancellationToken token = default)
        {
            // do we need paging possibilities in parameters? 100 items in tag cloud should be enough, shouldn't it?
            var jsonModel = await GetListAsync<ClientTagCloudItemListWrapper>("/api/client-tags", null, token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }


        /// <summary>
        /// Retrieves a list of all contacts from a client.
        /// </summary>
        /// <param name="clientId">The ID of the client.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the list of contacts </returns>
        public async Task<Types.PagedList<Contact>> GetContactListAsync(int clientId, CancellationToken token = default)
        {
            var jsonModel = await GetListAsync<ContactListWrapper>("/api/contacts", $"client_id={clientId}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Returns an contact by it's ID. 
        /// </summary>
        /// <param name="id">The ID of the contact.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The client or null if not found.</returns>
        public async Task<Contact> GetContactByIdAsync(int id, CancellationToken token = default)
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
        public async Task<byte[]> GetContactAvatarByIdAsync(int id, int size, CancellationToken token = default)
        {
            var httpClient = HttpClientFactory.Invoke();
            return await httpClient.GetBytesAsync(new Uri($"/api/contacts/{id}/avatar?size={size}", UriKind.Relative), token).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Creates a contact.
        /// </summary>
        /// <param name="model">The contact.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result returns the newly created contact with the ID.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<Contact> CreateAsync(Contact model, CancellationToken token = default)
        {
            if (model == null || model.ClientId <= 0)
            {
                throw new ArgumentException("contact or a value of the contact is null", nameof(model));
            }
            if (model.Id != 0)
            {
                throw new ArgumentException("invalid contact id", nameof(model));
            }

            var wrappedModel = new ContactWrapper
            {
                Contact = model.ToApi()
            };
            try
            {
                var result = await PostAsync("api/contacts", wrappedModel, token);
                return result.ToDomain();
            }
            catch (WebException wex)
                when (wex.Status == WebExceptionStatus.ProtocolError && (wex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ArgumentException("wrong contact parameter", nameof(model), wex);
            }

        }

        /// <summary>
        /// Edits a contact.
        /// </summary>
        /// <param name="model">The contact.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result returns the edited contact with the ID.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<Contact> EditAsync(Contact model, CancellationToken token = default)
        {
            if (model == null || model.ClientId <= 0)
            {
                throw new ArgumentException("contact or a value of the contact is null", nameof(model));
            }
            if (model.Id <= 0)
            {
                throw new ArgumentException("invalid contact id", nameof(model));
            }
            var wrappedModel = new ContactWrapper
            {
                Contact = model.ToApi()
            };
            try
            {
                var result = await PutAsync($"api/contacts/{model.Id}", wrappedModel, token);
                return result.ToDomain();
            }
            catch (WebException wex)
                when (wex.Status == WebExceptionStatus.ProtocolError && (wex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ArgumentException("wrong contact parameter", nameof(model), wex);
            }
        }

        /// <summary>
        /// Deletes a contact.
        /// </summary>
        /// <param name="id">The ID of the contact.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public Task DeleteContactAsync(int id, CancellationToken token = default)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid contact id", nameof(id));
            }
            return DeleteAsync($"/api/contacts/{id}", token);
        }
    }
}
