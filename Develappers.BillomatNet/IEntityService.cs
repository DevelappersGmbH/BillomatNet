using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Queries;

namespace Develappers.BillomatNet
{
    internal interface IEntityService<TModel, TFilter>
        where TFilter : new()
    {
        Task<Types.PagedList<TModel>> GetListAsync(CancellationToken token = default);
        Task<Types.PagedList<TModel>> GetListAsync(Query<TModel, TFilter> query, CancellationToken token = default);
        Task<TModel> GetByIdAsync(int id, CancellationToken token = default);
        Task DeleteAsync(int id, CancellationToken token = default);
        Task<TModel> CreateAsync(TModel model, CancellationToken token = default);
        Task<TModel> EditAsync(TModel model, CancellationToken token = default);
    }
}