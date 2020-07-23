using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Api.Net;
using Develappers.BillomatNet.Helpers;
using Develappers.BillomatNet.Queries;
using Article = Develappers.BillomatNet.Types.Article;
using ArticleProperty = Develappers.BillomatNet.Types.ArticleProperty;
using ArticleTag = Develappers.BillomatNet.Types.ArticleTag;
using TagCloudItem = Develappers.BillomatNet.Types.TagCloudItem;

namespace Develappers.BillomatNet
{
    public class ArticleService : ServiceBase, IEntityService<Article, ArticleFilter>
    {
        public ArticleService(Configuration configuration) : base(configuration)
        {
        }

        internal ArticleService(Func<IHttpClient> httpClientFactory) : base(httpClientFactory)
        {
        }

        /// <summary>
        /// Retrieves a list of articles.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns></returns>
        public Task<Types.PagedList<Article>> GetListAsync(CancellationToken token = default)
        {
            return GetListAsync(null, token);
        }

        /// <summary>
        /// Retrieves a list of articles.
        /// </summary>
        /// <param name="query">The filter and sort options.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Types.PagedList<Article>> GetListAsync(Query<Article, ArticleFilter> query, CancellationToken token = default)
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
        public async Task<Article> GetByIdAsync(int id, CancellationToken token = default)
        {
            var jsonModel = await GetItemByIdAsync<ArticleWrapper>($"/api/articles/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Creates an article.
        /// </summary>
        /// <param name="model">The article to create.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the new article.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<Article> CreateAsync(Article model, CancellationToken token = default)
        {
            if (model == null)
            {
                throw new ArgumentException("article or a value of the article is null", nameof(model));
            }
            if (model.Id != 0)
            {
                throw new ArgumentException("invalid article id", nameof(model));
            }

            var wrappedModel = new ArticleWrapper
            {
                Article = model.ToApi()
            };
            var jsonModel = await PostAsync("/api/articles", wrappedModel, token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Updates the specified article.
        /// </summary>
        /// <param name="model">The article.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the updated article.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<Article> EditAsync(Article model, CancellationToken token = default)
        {
            if (model.Id <= 0)
            {
                throw new ArgumentException("invalid article id", nameof(model));
            }

            var wrappedModel = new ArticleWrapper
            {
                Article = model.ToApi()
            };

            var jsonModel = await PutAsync($"/api/articles/{model.Id}", wrappedModel, token);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Deletes the article with the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public Task DeleteAsync(int id, CancellationToken token = default)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid article id", nameof(id));
            }
            return DeleteAsync($"/api/articles/{id}", token);
        }


        /// <summary>
        /// Retrieves a list of all properties.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns></returns>
        public Task<Types.PagedList<ArticleProperty>> GetPropertyListAsync(CancellationToken token = default)
        {
            return GetPropertyListAsync(null, token);
        }

        /// <summary>
        /// Retrieves a list of all properties.
        /// </summary>
        /// <param name="query">The filter and sort options.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Types.PagedList<ArticleProperty>> GetPropertyListAsync(Query<ArticleProperty, ArticlePropertyFilter> query, CancellationToken token = default)
        {
            var jsonModel = await GetListAsync<ArticlePropertyListWrapper>("/api/article-property-values", QueryString.For(query), token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Returns an article property by it's id. 
        /// </summary>
        /// <param name="id">The id of the article property.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The article property or null if not found.</returns>
        public async Task<ArticleProperty> GetPropertyByIdAsync(int id, CancellationToken token = default)
        {
            var jsonModel = await GetItemByIdAsync<ArticlePropertyWrapper>($"/api/article-property-values/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Creates / Edits an article property.
        /// </summary>
        /// <param name="model">The article property.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the new article property.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<ArticleProperty> EditArticlePropertyAsync(ArticleProperty model, CancellationToken token = default)
        {
            if (model == null || model.ArticleId <= 0 || model.ArticlePropertyId <= 0 || model.Value == null)
            {
                throw new ArgumentException("any parameter was not set", nameof(model));
            }

            var wrappedModel = new ArticlePropertyWrapper
            {
                ArticleProperty = model.ToApi()
            };
            try
            {
                var jsonModel = await PostAsync("/api/article-property-values", wrappedModel, token).ConfigureAwait(false);
                return jsonModel.ToDomain();
            }
            catch (WebException wex)
                when (wex.Status == WebExceptionStatus.ProtocolError && (wex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ArgumentException("wrong input parameter", nameof(model), wex);
            }
        }

        /// <summary>
        /// Retrieves the article tag cloud.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Types.PagedList<TagCloudItem>> GetTagCloudAsync(CancellationToken token = default)
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
        public async Task<Types.PagedList<ArticleTag>> GetTagListAsync(Query<ArticleTag, ArticleTagFilter> query, CancellationToken token = default)
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
        public async Task<ArticleTag> GetTagByIdAsync(int id, CancellationToken token = default)
        {
            var jsonModel = await GetItemByIdAsync<ArticleTagWrapper>($"/api/article-tags/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Deletes the article tag with the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public Task DeleteTagAsync(int id, CancellationToken token = default)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid article tag id", nameof(id));
            }
            return DeleteAsync($"/api/article-tags/{id}", token);
        }

        /// <summary>
        /// Creates an article tag.
        /// </summary>
        /// <param name="model">The article tag to create.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the new article tag.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<ArticleTag> CreateTagAsync(ArticleTag model, CancellationToken token = default)
        {
            if (model == null || model.ArticleId == 0 || model.Name == null)
            {
                throw new ArgumentException("article tag or a value of the article tag is null", nameof(model));
            }
            if (model.Id != 0)
            {
                throw new ArgumentException("invalid article tag id", nameof(model));
            }

            var wrappedModel = new ArticleTagWrapper
            {
                ArticleTag = model.ToApi()
            };
            var jsonModel = await PostAsync("/api/article-tags", wrappedModel, token);
            return jsonModel.ToDomain();
        }
    }
}
