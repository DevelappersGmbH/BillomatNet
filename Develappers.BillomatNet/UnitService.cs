// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Helpers;
using Develappers.BillomatNet.Queries;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Api.Net;
using Unit = Develappers.BillomatNet.Types.Unit;

namespace Develappers.BillomatNet
{
    public class UnitService : ServiceBase, IEntityService<Unit, UnitFilter>
    {
        /// <summary>
        /// Creates a new instance of <see cref="UnitService"/>.
        /// </summary>
        /// <param name="configuration">The service configuration.</param>
        public UnitService(Configuration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="UnitService"/> for unit tests.
        /// </summary>
        /// <param name="httpClientFactory">The function which creates a new <see cref="IHttpClient" /> implementation.</param>
        /// <exception cref="ArgumentNullException">Thrown when the parameter is null.</exception>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        internal UnitService(Func<IHttpClient> httpClientFactory) : base(httpClientFactory)
        {
        }

        /// <summary>
        /// Retrieves a list of units.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the list of units.
        /// </returns>
        public Task<Types.PagedList<Unit>> GetListAsync(CancellationToken token = default)
        {
            return GetListAsync(null, token);
        }

        /// <summary>
        /// Retrieves a list of units and applies the filter.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the list of units.
        /// </returns>
        public async Task<Types.PagedList<Unit>> GetListAsync(Query<Unit, UnitFilter> query, CancellationToken token = default)
        {
            var jsonModel = await GetListAsync<UnitListWrapper>("/api/units", QueryString.For(query), token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Retrieves a unit item by it's ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the unit.
        /// </returns>
        public async Task<Unit> GetByIdAsync(int id, CancellationToken token = default)
        {
            var jsonModel = await GetItemByIdAsync<UnitWrapper>($"/api/units/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Deletes the unit with the given ID.
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
                throw new ArgumentException("invalid unit id", nameof(id));
            }
            return DeleteAsync($"/api/units/{id}", token);
        }

        /// <summary>
        /// Updates the specified unit.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the updated unit.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<Unit> EditAsync(Unit unit, CancellationToken token = default)
        {
            if (unit.Id <= 0)
            {
                throw new ArgumentException("invalid unit id", nameof(unit));
            }

            var wrappedUnit = new UnitWrapper
            {
                Unit = unit.ToApi()
            };

            var jsonModel = await PutAsync($"/api/units/{unit.Id}", wrappedUnit, token);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Creates a unit.
        /// </summary>
        /// <param name="value">The unit to create.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the new unit.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<Unit> CreateAsync(Unit value, CancellationToken token = default)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (string.IsNullOrEmpty(value.Name) || value.Id != 0)
            {
                throw new ArgumentException("invalid property values for unit", nameof(value));
            }

            var wrappedUnit = new UnitWrapper
            {
                Unit = value.ToApi()
            };
            var jsonModel = await PostAsync("/api/units", wrappedUnit, token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }
    }
}
