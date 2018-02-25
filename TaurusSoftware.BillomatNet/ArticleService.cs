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

        public Task<PagedList<ArticleProperty>> GetPropertyListAsync(CancellationToken token = default(CancellationToken))
        {
            return GetPropertyListAsync(null, token);
        }

        public async Task<PagedList<ArticleProperty>> GetPropertyListAsync(Query<ArticleProperty, ArticlePropertyFilter> query, CancellationToken token = default(CancellationToken))
        {
            var httpClient = new HttpClient(Configuration.BillomatId, Configuration.ApiKey);
            var httpResponse = await httpClient.GetAsync(new Uri("/api/article-property-values", UriKind.Relative), QueryString.For(query), token);
            var jsonModel = JsonConvert.DeserializeObject<ArticlePropertyListWrapper>(httpResponse);
            return jsonModel.ToDomain();
        }

        public async Task<PagedList<Article>> GetListAsync(Query<Article, ArticleFilter> query, CancellationToken token = default(CancellationToken))
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
    }
}