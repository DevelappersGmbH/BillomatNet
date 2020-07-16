using System;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Helpers;
using Develappers.BillomatNet.Queries;
using Article = Develappers.BillomatNet.Types.Article;
using ArticleProperty = Develappers.BillomatNet.Types.ArticleProperty;
using ArticleTag = Develappers.BillomatNet.Types.ArticleTag;
using TagCloudItem = Develappers.BillomatNet.Types.TagCloudItem;

namespace Develappers.BillomatNet
{
    public class ArticleService : ServiceBase
    {
        public ArticleService(Configuration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Retrieves a list of articles.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns></returns>
        public Task<Types.PagedList<Article>> GetListAsync(CancellationToken token = default(CancellationToken))
        {
            return GetListAsync(null, token);
        }

        /// <summary>
        /// Retrieves a list of all properties.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns></returns>
        public Task<Types.PagedList<ArticleProperty>> GetPropertyListAsync(CancellationToken token = default(CancellationToken))
        {
            return GetPropertyListAsync(null, token);
        }

        /// <summary>
        /// Retrieves a list of all properties.
        /// </summary>
        /// <param name="query">The filter and sort options.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Types.PagedList<ArticleProperty>> GetPropertyListAsync(Query<ArticleProperty, ArticlePropertyFilter> query, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetListAsync<ArticlePropertyListWrapper>("/api/article-property-values", QueryString.For(query), token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Retrieves a list of articles.
        /// </summary>
        /// <param name="query">The filter and sort options.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Types.PagedList<Article>> GetListAsync(Query<Article, ArticleFilter> query, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetListAsync<ArticleListWrapper>("/api/articles", QueryString.For(query), token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Returns an article by it's ID. 
        /// </summary>
        /// <param name="id">The ID of the article.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The article or null if not found.</returns>
        public async Task<Article> GetByIdAsync(int id, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetItemByIdAsync<ArticleWrapper>($"/api/articles/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Returns an article property by it's id. 
        /// </summary>
        /// <param name="id">The id of the article property.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The article property or null if not found.</returns>
        public async Task<ArticleProperty> GetPropertyByIdAsync(int id, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetItemByIdAsync<ArticlePropertyWrapper>($"/api/article-property-values/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Retrieves the article tag cloud.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Types.PagedList<TagCloudItem>> GetTagCloudAsync(CancellationToken token = default(CancellationToken))
        {
            // do we need paging possibilities in parameters? 100 items in tag cloud should be enough, shouldn't it?
            var jsonModel = await GetListAsync<ArticleTagCloudItemListWrapper>("/api/article-tags", null, token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Retrieves the tag list for specific articles.
        /// </summary>
        /// <param name="query">The filter and sort options.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Types.PagedList<ArticleTag>> GetTagListAsync(Query<ArticleTag, ArticleTagFilter> query, CancellationToken token = default(CancellationToken))
        {
            if (query?.Filter == null)
            {
                throw new ArgumentException("filter has to be set", nameof(query));
            }

            var jsonModel = await GetListAsync<ArticleTagListWrapper>("/api/article-tags", QueryString.For(query), token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Returns an article tag by it's id. 
        /// </summary>
        /// <param name="id">The id of the tag property.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The article tag or null if not found.</returns>
        public async Task<ArticleTag> GetTagByIdAsync(int id, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetItemByIdAsync<ArticleTagWrapper>($"/api/article-tags/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }
    }
}