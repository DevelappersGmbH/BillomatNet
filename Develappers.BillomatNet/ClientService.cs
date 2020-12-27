// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Api.Net;
using Develappers.BillomatNet.Mapping;
using Develappers.BillomatNet.Queries;
using Newtonsoft.Json;
using Account = Develappers.BillomatNet.Types.Account;
using ArticleTag = Develappers.BillomatNet.Types.ArticleTag;
using Client = Develappers.BillomatNet.Types.Client;
using ClientProperty = Develappers.BillomatNet.Types.ClientProperty;
using ClientTag = Develappers.BillomatNet.Types.ClientTag;
using Contact = Develappers.BillomatNet.Types.Contact;
using TagCloudItem = Develappers.BillomatNet.Types.TagCloudItem;

namespace Develappers.BillomatNet
{
    public class ClientService : ServiceBase,
        IEntityService<Client, ClientFilter>,
        IEntityPropertyService<ClientProperty, ClientPropertyFilter>,
        IEntityTagService<ClientTag, ClientTagFilter>
    {
        private readonly Configuration _configuration;
        private const string EntityUrlFragment = "clients";

        /// <summary>
        /// Creates a new instance of <see cref="ClientService"/>.
        /// </summary>
        /// <param name="configuration">The service configuration.</param>
        public ClientService(Configuration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Creates a new instance of <see cref="ClientService"/> for unit tests.
        /// </summary>
        /// <param name="httpClientFactory">The function which creates a new <see cref="IHttpClient" /> implementation.</param>
        /// <exception cref="ArgumentNullException">Thrown when the parameter is null.</exception>
        internal ClientService(Func<IHttpClient> httpClientFactory) : base(httpClientFactory)
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
            var httpResponse = await httpClient.GetAsync(new Uri($"/api/{EntityUrlFragment}/myself", UriKind.Relative), token).ConfigureAwait(false);
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
            var jsonModel = await GetListAsync<ClientListWrapper>($"/api/{EntityUrlFragment}", QueryString.For(query), token).ConfigureAwait(false);
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
            var jsonModel = await GetItemByIdAsync<ClientWrapper>($"/api/{EntityUrlFragment}/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Gets the portal URL for this entity.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The url to this entity in billomat portal.</returns>
        /// <exception cref="ArgumentException">Thrown when the id is invalid.</exception>
        public string GetPortalUrl(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid client id", nameof(id));
            }

            return $"https://{_configuration.BillomatId}.billomat.net/app/{EntityUrlFragment}/show/entityId/{id}";
        }

        Task IEntityService<Client, ClientFilter>.DeleteAsync(int id, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        Task<Client> IEntityService<Client, ClientFilter>.CreateAsync(Client value, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        Task<Client> IEntityService<Client, ClientFilter>.EditAsync(Client value, CancellationToken token)
        {
            throw new NotImplementedException();
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

        Task<ClientProperty> IEntityPropertyService<ClientProperty, ClientPropertyFilter>.GetPropertyByIdAsync(int id, CancellationToken token)
        {
            throw new NotImplementedException();
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

        Task<ClientProperty> IEntityPropertyService<ClientProperty, ClientPropertyFilter>.EditPropertyAsync(ClientProperty value, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates / Edits a client property.
        /// </summary>
        /// <param name="value">The client property.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the new client property.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<ClientProperty> EditAsync(ClientProperty value, CancellationToken token = default)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.ClientId == 0 || value.ClientPropertyId == 0 || value.Value == null)
            {
                throw new ArgumentException("required value of the client invalid", nameof(value));
            }

            if (value.Id != 0)
            {
                throw new ArgumentException("invalid id", nameof(value));
            }

            var wrappedModel = new ClientPropertyWrapper
            {
                ClientProperty = value.ToApi()
            };
            try
            {
                var jsonModel = await PostAsync("/api/client-property-values", wrappedModel, token).ConfigureAwait(false);
                return jsonModel.ToDomain();
            }
            catch (WebException wex)
                when (wex.Status == WebExceptionStatus.ProtocolError && (wex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ArgumentException("wrong input parameter", nameof(value), wex);
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
        /// Retrieves the tag list for specific clients.
        /// </summary>
        /// <param name="query">The filter and sort options.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the paged list of client tags.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        public async Task<Types.PagedList<ClientTag>> GetTagListAsync(Query<ClientTag, ClientTagFilter> query, CancellationToken token = default)
        {
            if (query?.Filter == null)
            {
                throw new ArgumentException("filter has to be set", nameof(query));
            }

            var jsonModel = await GetListAsync<ClientTagListWrapper>("/api/client-tags", QueryString.For(query), token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        Task<ClientTag> IEntityTagService<ClientTag, ClientTagFilter>.GetTagByIdAsync(int id, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns an client tag by it's id. 
        /// </summary>
        /// <param name="id">The id of the tag property.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result returns the client tag.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        public async Task<ClientTag> GetTagById(int id, CancellationToken token = default)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid client tag id", nameof(id));
            }

            var jsonModel = await GetItemByIdAsync<ClientTagWrapper>($"/api/client-tags/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Creates an client tag.
        /// </summary>
        /// <param name="value">The client tag to create.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the new client tag.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<ClientTag> CreateAsync(ClientTag value, CancellationToken token = default)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.ClientId == 0 || string.IsNullOrEmpty(value.Name))
            {
                throw new ArgumentException("client tag or a value of the client tag is null", nameof(value));
            }

            if (value.Id != 0)
            {
                throw new ArgumentException("invalid model id", nameof(value));
            }
            var wrappedModel = new ClientTagWrapper
            {
                ClientTag = value.ToApi()
            };
            var jsonModel = await PostAsync("/api/client-tags", wrappedModel, token);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Deletes the client tag with the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public Task DeleteTagAsync(int id, CancellationToken token = default)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid client tag id", nameof(id));
            }
            return DeleteAsync($"/api/client-tags/{id}", token);
        }

        Task<ClientTag> IEntityTagService<ClientTag, ClientTagFilter>.CreateTagAsync(ArticleTag model, CancellationToken token)
        {
            throw new NotImplementedException();
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
