// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Api.Net;
using Develappers.BillomatNet.Mapping;
using Develappers.BillomatNet.Queries;
using Supplier = Develappers.BillomatNet.Types.Supplier;

namespace Develappers.BillomatNet
{
    public class SupplierService : ServiceBase,
        IEntityService<Supplier, SupplierFilter>
    {
        private const string EntityUrlFragment = "suppliers";

        /// <summary>
        /// Creates a new instance of <see cref="SupplierService"/>.
        /// </summary>
        /// <param name="httpClient">The http client.</param>
        public SupplierService(IHttpClient httpClient) : base(httpClient)
        {
        }

        /// <summary>
        /// Retrieves a list of all clients.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the list of suppliers.
        /// </returns>
        public Task<Types.PagedList<Supplier>> GetListAsync(CancellationToken token = default)
        {
            return GetListAsync(null, token);
        }

        /// <summary>
        /// Retrieves a list of all suppliers.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the list of suppliers.
        /// </returns>
        public async Task<Types.PagedList<Supplier>> GetListAsync(Query<Supplier, SupplierFilter> query, CancellationToken token = default)
        {
            var jsonModel = await GetListAsync<SupplierListWrapper>($"/api/{EntityUrlFragment}", QueryString.For(query), token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Retrieves an supplier by it's ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the supplier.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<Supplier> GetByIdAsync(int id, CancellationToken token = default)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid supplier id", nameof(id));
            }

            var jsonModel = await GetItemByIdAsync<SupplierWrapper>($"/api/{EntityUrlFragment}/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        public Task DeleteAsync(int id, CancellationToken token = default)
        {
            throw new NotImplementedException("This service is not implemented by now. You can help us by contributing to our project on github.");
        }

        Task<Supplier> IEntityService<Supplier, SupplierFilter>.CreateAsync(Supplier model, CancellationToken token)
        {
            throw new NotImplementedException("This service is not implemented by now. You can help us by contributing to our project on github.");
        }

        Task<Supplier> IEntityService<Supplier, SupplierFilter>.EditAsync(Supplier model, CancellationToken token)
        {
            throw new NotImplementedException("This service is not implemented by now. You can help us by contributing to our project on github.");
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
                throw new ArgumentException("invalid supplier id", nameof(id));
            }

            return $"{HttpClient.BaseUrl}app/{EntityUrlFragment}/show/entityId/{id}";
        }
    }
}
