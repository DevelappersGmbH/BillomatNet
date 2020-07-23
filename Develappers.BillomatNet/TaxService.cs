using System;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Helpers;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Queries;
using Tax = Develappers.BillomatNet.Types.Tax;

namespace Develappers.BillomatNet
{
    public class TaxService : ServiceBase, IEntityService<Tax, TaxFilter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaxService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public TaxService(Configuration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Retrieves a list of taxes.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the list of taxes.
        /// </returns>
        public async Task<Types.PagedList<Tax>> GetListAsync(CancellationToken token = default)
        {
            var jsonModel = await GetListAsync<TaxListWrapper>("/api/taxes", null, token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        Task<Types.PagedList<Tax>> IEntityService<Tax, TaxFilter>.GetListAsync(Query<Tax, TaxFilter> query, CancellationToken token = default)
        {
            // TODO: implement implicitly and make public
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves a tax item by it's ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the unit.
        /// </returns>
        public async Task<Tax> GetByIdAsync(int id, CancellationToken token = default)
        {
            var jsonModel = await GetItemByIdAsync<TaxWrapper>($"/api/taxes/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Deletes the tax with the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <param name="token">The token.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public Task DeleteAsync(int id, CancellationToken token = default)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid tax id", nameof(id));
            }
            return DeleteAsync($"/api/taxes/{id}", token);
        }

        /// <summary>
        /// Creates an tax.
        /// </summary>
        /// <param name="model">The tax object.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result returns the newly created tax with the ID.
        /// </returns>
        public async Task<Tax> CreateAsync(Tax model, CancellationToken token = default)
        {
            if (model == null || model.Name == "" || model.Name == null)
            {
                throw new ArgumentException();
            }
            if (model.Id != 0)
            {
                throw new ArgumentException("invalid unit id", nameof(model));
            }

            var wrappedModel = new TaxWrapper
            {
                Tax = model.ToApi()
            };
            var result = await PostAsync("/api/taxes", wrappedModel, token);

            return result.ToDomain();
        }

        /// <summary>
        /// Updats the specified tax.
        /// </summary>
        /// <param name="model">The tax.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the updated tax.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<Tax> EditAsync(Tax model, CancellationToken token = default)
        {
            if (model == null || model.Name == "" || model.Name == null)
            {
                throw new ArgumentException();
            }
            if (model.Id <= 0)
            {
                throw new ArgumentException("invalid unit id", nameof(model));
            }
            var wrappedModel = new TaxWrapper
            {
                Tax = model.ToApi()
            };
            var jsonModel = await PutAsync($"/api/taxes/{model.Id}", wrappedModel, token);

            return jsonModel.ToDomain();
        }
    }
}
