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
        public TaxService(Configuration configuration) : base(configuration)
        {
        }

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

        Task<Tax> IEntityService<Tax, TaxFilter>.CreateAsync(Tax model, CancellationToken token = default)
        {
            // TODO: implement implicitly and make public
            throw new System.NotImplementedException();
        }

        Task<Tax> IEntityService<Tax, TaxFilter>.EditAsync(Tax model, CancellationToken token = default)
        {
            // TODO: implement implicitly and make public
            throw new System.NotImplementedException();
        }
    }
}
