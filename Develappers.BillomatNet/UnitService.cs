using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Helpers;
using Develappers.BillomatNet.Queries;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using Unit = Develappers.BillomatNet.Types.Unit;

namespace Develappers.BillomatNet
{
    public class UnitService : ServiceBase
    {
        public UnitService(Configuration configuration) : base(configuration)
        {
        }

        public Task<Types.PagedList<Unit>> GetListAsync(CancellationToken token = default(CancellationToken))
        {
            return GetListAsync(null, token);
        }

        public async Task<Types.PagedList<Unit>> GetListAsync(Query<Unit, UnitFilter> query, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetListAsync<UnitListWrapper>("/api/units", QueryString.For(query), token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        public async Task<Types.PagedList<Unit>> GetFilteredListAsync(Query<Unit, UnitFilter> query, CancellationToken token = default(CancellationToken))
        {
            return await GetListAsync(query);
        }

        public async Task<Unit> GetByIdAsync(int id, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetItemByIdAsync<UnitWrapper>($"/api/units/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        public Task DeleteAsync(int id, CancellationToken token = default(CancellationToken))
        {
            return DeleteAsync($"/api/units/{id}", token);
        }

        public async Task EditAsync(int id, Unit unit, CancellationToken token = default(CancellationToken))
        {
            var wrappedUnit = new UnitWrapper
            {
                Unit = unit.ToApi()
            };
            await PutAsync($"/api/units/{id}", wrappedUnit, token);
        }

        public async Task CreateAsync(Unit unit, CancellationToken token = default(CancellationToken))
        {
            var wrappedUnit = new UnitWrapper
            {
                Unit = unit.ToApi()
            };
            await PostAsync("/api/units", wrappedUnit, token);
        }
    }
}
