using System;
using System.Threading;
using System.Threading.Tasks;
using TaurusSoftware.BillomatNet.Api;
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
            var jsonModel = await GetListAsync<ArticlePropertyListWrapper>("/api/article-property-values", QueryString.For(query), token);
            return jsonModel.ToDomain();
        }

        public async Task<Types.PagedList<Article>> GetListAsync(Query<Article, ArticleFilter> query, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetListAsync<ArticleListWrapper>("/api/articles", QueryString.For(query), token);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Returns an article by it's id. 
        /// </summary>
        /// <param name="id">The id of the article.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The article or null if not found.</returns>
        public async Task<Article> GetById(int id, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetItemByIdAsync<ArticleWrapper>($"/api/articles/{id}", token);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Returns an article property by it's id. 
        /// </summary>
        /// <param name="id">The id of the article property.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The article property or null if not found.</returns>
        public async Task<ArticleProperty> GetPropertyById(int id, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetItemByIdAsync<ArticlePropertyWrapper>($"/api/article-property-values/{id}", token);
            return jsonModel.ToDomain();
        }

        public async Task<Types.PagedList<TagCloudItem>> GetTagCloudAsync(CancellationToken token = default(CancellationToken))
        {
            // do we need paging possibilities in parameters? 100 items in tag cloud should be enough, shouldn't it?
            var jsonModel = await GetListAsync<ArticleTagCloudItemListWrapper>("/api/article-tags", null, token);
            return jsonModel.ToDomain();
        }

        public async Task<Types.PagedList<ArticleTag>> GetTagListAsync(Query<ArticleTag, ArticleTagFilter> query, CancellationToken token = default(CancellationToken))
        {
            if (query?.Filter == null)
            {
                throw new ArgumentException("filter has to be set", nameof(query));
            }

            var jsonModel = await GetListAsync<ArticleTagListWrapper>("/api/article-tags", QueryString.For(query), token);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Returns an article tag by it's id. 
        /// </summary>
        /// <param name="id">The id of the tag property.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The article tag or null if not found.</returns>
        public async Task<ArticleTag> GetTagById(int id, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetItemByIdAsync<ArticleTagWrapper>($"/api/article-tags/{id}", token);
            return jsonModel.ToDomain();
        }
    }
}