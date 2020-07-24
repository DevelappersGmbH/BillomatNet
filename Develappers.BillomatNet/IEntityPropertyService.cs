using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Queries;

namespace Develappers.BillomatNet
{
    internal interface IEntityPropertyService<TProperty, TPropertyFilter>
        where TPropertyFilter : new()
    {
        Task<TProperty> GetPropertyByIdAsync(int id, CancellationToken token = default);
        Task<Types.PagedList<TProperty>> GetPropertyListAsync(CancellationToken token = default);
        Task<Types.PagedList<TProperty>> GetPropertyListAsync(Query<TProperty, TPropertyFilter> query, CancellationToken token = default);
        Task<TProperty> EditPropertyAsync(TProperty model, CancellationToken token = default);
    }
}
