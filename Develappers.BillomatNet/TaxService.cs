using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Helpers;
using Develappers.BillomatNet.Types;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tax = Develappers.BillomatNet.Types.Tax;

namespace Develappers.BillomatNet
{
    public class TaxService : ServiceBase
    {
        public TaxService(Configuration configuration) : base(configuration)
        {
        }

        public async Task<Types.PagedList<Tax>> GetListAsync(CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetListAsync<TaxListWrapper>("/api/taxes", null, token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        public async Task<Tax> GetByIdAsync(int id, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetItemByIdAsync<TaxWrapper>($"/api/taxes/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }
    }
}
