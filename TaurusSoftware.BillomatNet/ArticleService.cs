using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaurusSoftware.BillomatNet.Api;
using TaurusSoftware.BillomatNet.Api.Net;
using TaurusSoftware.BillomatNet.Helpers;
using TaurusSoftware.BillomatNet.Queries;
using Article = TaurusSoftware.BillomatNet.Types.Article;

namespace TaurusSoftware.BillomatNet
{
    public class ArticleService : ServiceBase
    {
        public ArticleService(Configuration configuration) : base(configuration)
        {
        }

        public Task<PagedList<Article>> GetListAsync(CancellationToken token = default(CancellationToken))
        {
            return GetListAsync(null, token);
        }

        public async Task<PagedList<Article>> GetListAsync(Query<Article, ArticleFilter> query, CancellationToken token = default(CancellationToken))
        {
            var httpClient = new HttpClient(Configuration.BillomatId, Configuration.ApiKey);
            var httpResponse = await httpClient.GetAsync(new Uri("/api/articles", UriKind.Relative), QueryString.For(query), token);
            var jsonModel = JsonConvert.DeserializeObject<ArticleListWrapper>(httpResponse);
            return jsonModel.ToDomain();
        }
    }
}