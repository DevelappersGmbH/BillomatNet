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
using Develappers.BillomatNet.Types;
using Offer = Develappers.BillomatNet.Types.Offer;
//using OfferItem = Develappers.BillomatNet.Types.OfferItem;
using TagCloudItem = Develappers.BillomatNet.Types.TagCloudItem;

namespace Develappers.BillomatNet
{
    public class OfferService : ServiceBase, IEntityReadService<Offer, OfferFilter>
    {
        /// <summary>
        /// Creates a new instance of <see cref="OfferService"/>.
        /// </summary>
        /// <param name="configuration">The service configuration.</param>
        public OfferService(Configuration configuration) : base(configuration)
        {
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

        public Task<Types.PagedList<Offer>> GetListAsync(CancellationToken token = default)
        {
            return GetListAsync(null, token);
        }

        public async Task<Types.PagedList<Offer>> GetListAsync(Query<Offer, OfferFilter> query, CancellationToken token = default)
        {
            var jsonModel = await GetListAsync<OfferListWrapper>("/api/offers", QueryString.For(query), token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }
    }
}
