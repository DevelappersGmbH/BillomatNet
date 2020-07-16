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
        public async Task<Types.PagedList<Tax>> GetListAsync(CancellationToken token = default(CancellationToken))
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

        Task IEntityService<Tax, TaxFilter>.DeleteAsync(int id, CancellationToken token = default)
        {
            // TODO: implement implicitly and make public
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Creates an tax.
        /// </summary>
        /// <param name="invoice">The tax object.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result returns the newly created tax with the ID.
        /// </returns>
        public async Task<Tax> CreateAsync(Tax model, CancellationToken token = default)
        {
            var wrappedModel = new TaxWrapper
            {
                Tax = model.ToApi()
            };
            var result = await PostAsync("/api/taxes", wrappedModel, token);

            return result.ToDomain();
        }

        Task<Tax> IEntityService<Tax, TaxFilter>.EditAsync(Tax model, CancellationToken token = default)
        {
            // TODO: implement implicitly and make public
            throw new System.NotImplementedException();
        }
    }
}
