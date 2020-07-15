using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Helpers;
using Develappers.BillomatNet.Queries;
using System;
using System.Threading;
using System.Threading.Tasks;
using Unit = Develappers.BillomatNet.Types.Unit;

namespace Develappers.BillomatNet
{
    public class UnitService : ServiceBase, IEntityService<Unit, UnitFilter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public UnitService(Configuration configuration) : base(configuration)
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
        /// Retrieves a unit item by it's id.
        /// </summary>
        /// <param name="id">The id.</param>
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
        /// Deletes the unit with the given id..
        /// </summary>
        /// <param name="id">The identifier.</param>
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
        /// <param name="unit">The unit to create.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the new unit.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<Unit> CreateAsync(Unit unit, CancellationToken token = default)
        {
            if (unit == null || unit.Name == "" || unit.Name == null)
            {
                throw new ArgumentException();
            }
            if (unit.Id != 0)
            {
                throw new ArgumentException("invalid unit id", nameof(unit));
            }

            var wrappedUnit = new UnitWrapper
            {
                Unit = unit.ToApi()
            };
            var jsonModel = await PostAsync("/api/units", wrappedUnit, token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }
    }
}
