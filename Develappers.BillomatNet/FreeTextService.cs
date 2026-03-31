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
using Develappers.BillomatNet.Types;
using FreeText = Develappers.BillomatNet.Types.FreeText;

namespace Develappers.BillomatNet
{
    public class FreeTextService : ServiceBase,
        IEntityService<FreeText, FreeTextFilter>
    {
        private const string EntityUrlFragment = "free-texts";

        /// <summary>
        /// Creates a new instance of <see cref="FreeTextService"/>.
        /// </summary>
        /// <param name="httpClient">The http client.</param>
        public FreeTextService(IHttpClient httpClient) : base(httpClient)
        {
        }

        /// <summary>
        /// Retrieves a list of free texts.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns></returns>
        public Task<Types.PagedList<FreeText>> GetListAsync(CancellationToken token = default)
        {
            return GetListAsync(null, token);
        }

        /// <summary>
        /// Retrieves a list of free texts.
        /// </summary>
        /// <param name="query">The filter and sort options.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Types.PagedList<FreeText>> GetListAsync(Query<FreeText, FreeTextFilter> query, CancellationToken token = default)
        {
            var jsonModel = await GetListAsync<FreeTextListWrapper>($"/api/{EntityUrlFragment}", QueryString.For(query), token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Returns a free texts by it's ID. 
        /// </summary>
        /// <param name="id">The ID of the free text.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The article or null if not found.</returns>
        public async Task<FreeText> GetByIdAsync(int id, CancellationToken token = default)
        {
            var jsonModel = await GetItemByIdAsync<FreeTextWrapper>($"/api/{EntityUrlFragment}/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Creates a free texts.
        /// </summary>
        /// <param name="value">The free text to create.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the new free text.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<FreeText> CreateAsync(FreeText value, CancellationToken token = default)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (value.Id != 0)
            {
                throw new ArgumentException("invalid free text id", nameof(value));
            }

            var wrappedModel = new FreeTextWrapper
            {
                FreeText = value.ToApi()
            };

            var jsonModel = await PostAsync($"/api/{EntityUrlFragment}", wrappedModel, token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Updates the specified free text.
        /// </summary>
        /// <param name="value">The free text.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the updated article.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<FreeText> EditAsync(FreeText value, CancellationToken token = default)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.Id <= 0)
            {
                throw new ArgumentException("invalid free text id", nameof(value));
            }

            var wrappedModel = new FreeTextWrapper
            {
                FreeText = value.ToApi()
            };

            var jsonModel = await PutAsync($"/api/{EntityUrlFragment}/{value.Id}", wrappedModel, token);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Deletes the free text with the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public Task DeleteAsync(int id, CancellationToken token = default)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid free text id", nameof(id));
            }
            return DeleteAsync($"/api/{EntityUrlFragment}/{id}", token);
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
                throw new ArgumentException("invalid free text id", nameof(id));
            }

            return $"{HttpClient.BaseUrl}app/beta/masterdata/{EntityUrlFragment}/{id}";
        }
    }
}
