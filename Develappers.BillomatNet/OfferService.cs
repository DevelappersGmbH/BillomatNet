// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Api.Net;
using Develappers.BillomatNet.Mapping;
using Develappers.BillomatNet.Queries;
using Offer = Develappers.BillomatNet.Types.Offer;
using OfferItem = Develappers.BillomatNet.Types.OfferItem;

namespace Develappers.BillomatNet
{
    public class OfferService : ServiceBase, IEntityService<Offer, OfferFilter>
    {
        private readonly Configuration _configuration;

        /// <summary>
        /// Creates a new instance of <see cref="OfferService"/>.
        /// </summary>
        /// <param name="configuration">The service configuration.</param>
        public OfferService(Configuration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Creates a new instance of <see cref="OfferService"/> for unit tests.
        /// </summary>
        /// <param name="httpClientFactory">The function which creates a new <see cref="IHttpClient" /> implementation.</param>
        /// <exception cref="ArgumentNullException">Thrown when the parameter is null.</exception>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        internal OfferService(Func<IHttpClient> httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<Offer> GetByIdAsync(int id, CancellationToken token = default)
        {
            var jsonModel = await GetItemByIdAsync<OfferWrapper>($"/api/offers/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        public string GetPortalUrl(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid offer id", nameof(id));
            }

            return $"https://{_configuration.BillomatId}.billomat.net/app/offers/show/entityId/{id}";
        }

        Task IEntityService<Offer, OfferFilter>.DeleteAsync(int id, CancellationToken token)
        {
            throw new NotImplementedException("This service is not implemented by now. You can help us by contributing to our project on github.");
        }

        Task<Offer> IEntityService<Offer, OfferFilter>.CreateAsync(Offer model, CancellationToken token)
        {
            throw new NotImplementedException("This service is not implemented by now. You can help us by contributing to our project on github.");
        }

        Task<Offer> IEntityService<Offer, OfferFilter>.EditAsync(Offer model, CancellationToken token)
        {
            throw new NotImplementedException("This service is not implemented by now. You can help us by contributing to our project on github.");
        }

        public Task<Types.PagedList<Offer>> GetListAsync(CancellationToken token = default)
        {
            return GetListAsync(null, token);
        }

        public async Task<Types.PagedList<Offer>> GetListAsync(Query<Offer, OfferFilter> query, CancellationToken token = default)
        {
            var jsonModel = await GetListAsync<OfferListWrapper>("/api/offers", QueryString.For(query), token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Returns and invoice by it's ID.
        /// </summary>
        /// <param name="id">The ID of the invoice.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The invoice or null if not found.</returns>
        public async Task<OfferItem> GetItemByIdAsync(int id, CancellationToken token = default)
        {
            var jsonModel = await GetItemByIdAsync<OfferItemWrapper>($"/api/offer-items/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Retrieves a list of the items (articles) used in the offer.
        /// </summary>
        /// <param name="offerId">The ID of the invoice.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The invoice items list or null if not found.</returns>
        public async Task<Types.PagedList<OfferItem>> GetItemsAsync(int offerId, CancellationToken token = default)
        {
            var jsonModel = await GetListAsync<OfferItemListWrapper>("/api/offer-items", $"offer_id={offerId}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }
    }
}
