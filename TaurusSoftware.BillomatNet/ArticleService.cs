using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaurusSoftware.BillomatNet.Api;
using TaurusSoftware.BillomatNet.Api.Net;
using TaurusSoftware.BillomatNet.Helpers;
using TaurusSoftware.BillomatNet.Queries;
using Article = TaurusSoftware.BillomatNet.Types.Article;
using ArticleProperty = TaurusSoftware.BillomatNet.Types.ArticleProperty;
using ArticleTag = TaurusSoftware.BillomatNet.Types.ArticleTag;
using TagCloudItem = TaurusSoftware.BillomatNet.Types.TagCloudItem;

namespace TaurusSoftware.BillomatNet
{
    public class ArticleService : ServiceBase
    {
        public ArticleService(Configuration configuration) : base(configuration)
        {
        }

        public Task<Types.PagedList<Article>> GetListAsync(CancellationToken token = default(CancellationToken))
        {
            return GetListAsync(null, token);
        }

        public Task<Types.PagedList<ArticleProperty>> GetPropertyListAsync(CancellationToken token = default(CancellationToken))
        {
            return GetPropertyListAsync(null, token);
        }

        public async Task<Types.PagedList<ArticleProperty>> GetPropertyListAsync(Query<ArticleProperty, ArticlePropertyFilter> query, CancellationToken token = default(CancellationToken))
        {
            var httpClient = new HttpClient(Configuration.BillomatId, Configuration.ApiKey);
            var httpResponse = await httpClient.GetAsync(new Uri("/api/article-property-values", UriKind.Relative), QueryString.For(query), token);
            var jsonModel = JsonConvert.DeserializeObject<ArticlePropertyListWrapper>(httpResponse);
            return jsonModel.ToDomain();
        }

        public async Task<Types.PagedList<Article>> GetListAsync(Query<Article, ArticleFilter> query, CancellationToken token = default(CancellationToken))
        {
            var httpClient = new HttpClient(Configuration.BillomatId, Configuration.ApiKey);
            var httpResponse = await httpClient.GetAsync(new Uri("/api/articles", UriKind.Relative), QueryString.For(query), token);
            var jsonModel = JsonConvert.DeserializeObject<ArticleListWrapper>(httpResponse);
            return jsonModel.ToDomain();
        }

        public async Task<Article> GetById(int id, CancellationToken token = default(CancellationToken))
        {
            var httpClient = new HttpClient(Configuration.BillomatId, Configuration.ApiKey);
            var httpResponse = await httpClient.GetAsync(new Uri($"/api/articles/{id}", UriKind.Relative), token);
            var jsonModel = JsonConvert.DeserializeObject<ArticleWrapper>(httpResponse);
            return jsonModel.ToDomain();
        }

        public async Task<ArticleProperty> GetPropertyById(int id, CancellationToken token = default(CancellationToken))
        {
            var httpClient = new HttpClient(Configuration.BillomatId, Configuration.ApiKey);
            var httpResponse = await httpClient.GetAsync(new Uri($"/api/article-property-values/{id}", UriKind.Relative), token);
            var jsonModel = JsonConvert.DeserializeObject<ArticlePropertyWrapper>(httpResponse);
            return jsonModel.ToDomain();
        }

        public async Task<Types.PagedList<TagCloudItem>> GetTagCloudAsync(CancellationToken token = default(CancellationToken))
        {
            // do we need paging possibilities in parameters? 100 items in tagcloud should be enough, shouldn't it?
            var httpClient = new HttpClient(Configuration.BillomatId, Configuration.ApiKey);
            var httpResponse = await httpClient.GetAsync(new Uri("/api/article-tags", UriKind.Relative), null, token);
            var jsonModel = JsonConvert.DeserializeObject<ArticleTagCloudItemListWrapper>(httpResponse);
            return jsonModel.ToDomain();
        }

        public async Task<Types.PagedList<ArticleTag>> GetTagListAsync(Query<ArticleTag, ArticleTagFilter> query, CancellationToken token = default(CancellationToken))
        {
            if (query?.Filter == null)
            {
                throw new ArgumentException("filter has to be set", nameof(query));
            }

            var httpClient = new HttpClient(Configuration.BillomatId, Configuration.ApiKey);
            var httpResponse = await httpClient.GetAsync(new Uri("/api/article-tags", UriKind.Relative), QueryString.For(query), token);
            var jsonModel = JsonConvert.DeserializeObject<ArticleTagListWrapper>(httpResponse);
            return jsonModel.ToDomain();
        }

        public async Task<ArticleTag> GetTagById(int id, CancellationToken token = default(CancellationToken))
        {
            var httpClient = new HttpClient(Configuration.BillomatId, Configuration.ApiKey);
            var httpResponse = await httpClient.GetAsync(new Uri($"/api/article-tags/{id}", UriKind.Relative), token);
            var jsonModel = JsonConvert.DeserializeObject<ArticleTagWrapper>(httpResponse);
            return jsonModel.ToDomain();
        }
    }
}