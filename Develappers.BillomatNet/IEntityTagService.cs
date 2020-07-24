using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Queries;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet
{
    internal interface IEntityTagService<TTag, TTagFilter>
        where TTagFilter : new()
    {
        Task<PagedList<TagCloudItem>> GetTagCloudAsync(CancellationToken token = default);
        Task<PagedList<TTag>> GetTagListAsync(Query<TTag, TTagFilter> query, CancellationToken token = default);
        Task<TTag> GetTagByIdAsync(int id, CancellationToken token = default);
        Task DeleteTagAsync(int id, CancellationToken token = default);
        Task<TTag> CreateTagAsync(ArticleTag model, CancellationToken token = default);
    }
}
