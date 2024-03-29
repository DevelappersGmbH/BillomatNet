﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Api.Net;
using Develappers.BillomatNet.Mapping;
using Develappers.BillomatNet.Queries;
using Tax = Develappers.BillomatNet.Types.Tax;

namespace Develappers.BillomatNet
{
    public class TaxService : ServiceBase, IEntityService<Tax, TaxFilter>
    {
        private const string EntityUrlFragment = "taxes";


        /// <summary>
        ///     Creates a new instance of <see cref="TaxService" />.
        /// </summary>
        /// <param name="httpClient">The http client.</param>
        public TaxService(IHttpClient httpClient) : base(httpClient)
        {
        }

        /// <summary>
        ///     Retrieves a list of taxes.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the list of taxes.
        /// </returns>
        public async Task<Types.PagedList<Tax>> GetListAsync(CancellationToken token = default)
        {
            var jsonModel = await GetListAsync<TaxListWrapper>($"/api/{EntityUrlFragment}", null, token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        Task<Types.PagedList<Tax>> IEntityService<Tax, TaxFilter>.GetListAsync(Query<Tax, TaxFilter> query,
            CancellationToken token)
        {
            throw new NotImplementedException("This service is not implemented by now. You can help us by contributing to our project on github.");
        }

        /// <summary>
        ///     Retrieves a tax item by it's ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the tax.
        /// </returns>
        public async Task<Tax> GetByIdAsync(int id, CancellationToken token = default)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid tax id", nameof(id));
            }

            var jsonModel = await GetItemByIdAsync<TaxWrapper>($"/api/{EntityUrlFragment}/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        ///     Deletes a tax.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        /// </returns>
        public async Task DeleteAsync(int id, CancellationToken token = default)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid tax id", nameof(id));
            }

            await DeleteAsync($"/api/{EntityUrlFragment}/{id}", token).ConfigureAwait(false);
        }

        /// <summary>
        ///     Creates an tax.
        /// </summary>
        /// <param name="value">The tax object.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result returns the newly created tax with the ID.
        /// </returns>
        public async Task<Tax> CreateAsync(Tax value, CancellationToken token = default)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (string.IsNullOrEmpty(value.Name) || value.Id != 0)
            {
                throw new ArgumentException("invalid property values for tax", nameof(value));
            }

            var wrappedModel = new TaxWrapper { Tax = value.ToApi() };
            var result = await PostAsync($"/api/{EntityUrlFragment}", wrappedModel, token);

            return result.ToDomain();
        }

        Task<Tax> IEntityService<Tax, TaxFilter>.EditAsync(Tax model, CancellationToken token)
        {
            throw new NotImplementedException("This service is not implemented by now. You can help us by contributing to our project on github.");
        }
    }
}
