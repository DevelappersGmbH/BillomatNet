// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Api.Net;
using Develappers.BillomatNet.Mapping;
using Develappers.BillomatNet.Queries;
using Article = Develappers.BillomatNet.Types.Article;
using ArticleProperty = Develappers.BillomatNet.Types.ArticleProperty;
using ArticleTag = Develappers.BillomatNet.Types.ArticleTag;
using TagCloudItem = Develappers.BillomatNet.Types.TagCloudItem;

namespace Develappers.BillomatNet
{
    public class ArticleService : ServiceBase,
        IEntityService<Article, ArticleFilter>,
        IEntityPropertyService<ArticleProperty, ArticlePropertyFilter>,
        IEntityTagService<ArticleTag, ArticleTagFilter>
    {
        private readonly Configuration _configuration;
        private const string EntityUrlFragment = "articles";
        private const string EntityTagsUrlFragment = "article-tags";

        /// <summary>
        /// Creates a new instance of <see cref="ArticleService"/>.
        /// </summary>
        /// <param name="configuration">The service configuration.</param>
        public ArticleService(Configuration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Creates a new instance of <see cref="ArticleService"/> for unit tests.
        /// </summary>
        /// <param name="httpClientFactory">The function which creates a new <see cref="IHttpClient" /> implementation.</param>
        /// <exception cref="ArgumentNullException">Thrown when the parameter is null.</exception>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
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
            var jsonModel = await GetListAsync<ArticleListWrapper>($"/api/{EntityUrlFragment}", QueryString.For(query), token).ConfigureAwait(false);
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
            var jsonModel = await GetItemByIdAsync<ArticleWrapper>($"/api/{EntityUrlFragment}/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Creates an article.
        /// </summary>
        /// <param name="value">The article to create.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the new article.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<Article> CreateAsync(Article value, CancellationToken token = default)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (value.Id != 0)
            {
                throw new ArgumentException("invalid article id", nameof(value));
            }

            var wrappedModel = new ArticleWrapper
            {
                Article = value.ToApi()
            };

            var jsonModel = await PostAsync($"/api/{EntityUrlFragment}", wrappedModel, token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Updates the specified article.
        /// </summary>
        /// <param name="value">The article.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the updated article.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<Article> EditAsync(Article value, CancellationToken token = default)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.Id <= 0)
            {
                throw new ArgumentException("invalid article id", nameof(value));
            }

            var wrappedModel = new ArticleWrapper
            {
                Article = value.ToApi()
            };

            var jsonModel = await PutAsync($"/api/{EntityUrlFragment}/{value.Id}", wrappedModel, token);
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
            return DeleteAsync($"/api/{EntityUrlFragment}/{id}", token);
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
        /// <param name="value">The article property.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the new article property.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<ArticleProperty> EditPropertyAsync(ArticleProperty value, CancellationToken token = default)
        {
            if (value == null || value.ArticleId <= 0 || value.ArticlePropertyId <= 0 || value.Value == null)
            {
                throw new ArgumentException("any parameter was not set", nameof(value));
            }

            var wrappedModel = new ArticlePropertyWrapper
            {
                ArticleProperty = value.ToApi()
            };
            try
            {
                var jsonModel = await PostAsync("/api/article-property-values", wrappedModel, token).ConfigureAwait(false);
                return jsonModel.ToDomain();
            }
            catch (WebException wex)
                when (wex.Status == WebExceptionStatus.ProtocolError && (wex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ArgumentException("wrong input parameter", nameof(value), wex);
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
            var jsonModel = await GetListAsync<ArticleTagCloudItemListWrapper>($"/api/{EntityTagsUrlFragment}", null, token).ConfigureAwait(false);
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

            var jsonModel = await GetListAsync<ArticleTagListWrapper>($"/api/{EntityTagsUrlFragment}", QueryString.For(query), token).ConfigureAwait(false);
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
            var jsonModel = await GetItemByIdAsync<ArticleTagWrapper>($"/api/{EntityTagsUrlFragment}/{id}", token).ConfigureAwait(false);
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
            return DeleteAsync($"/api/{EntityTagsUrlFragment}/{id}", token);
        }

        /// <summary>
        /// Creates an article tag.
        /// </summary>
        /// <param name="value">The article tag to create.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the new article tag.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<ArticleTag> CreateTagAsync(ArticleTag value, CancellationToken token = default)
        {
            if (value == null || value.ArticleId == 0 || value.Name == null)
            {
                throw new ArgumentException("article tag or a value of the article tag is null", nameof(value));
            }
            if (value.Id != 0)
            {
                throw new ArgumentException("invalid article tag id", nameof(value));
            }

            var wrappedModel = new ArticleTagWrapper
            {
                ArticleTag = value.ToApi()
            };
            var jsonModel = await PostAsync($"/api/{EntityTagsUrlFragment}", wrappedModel, token);
            return jsonModel.ToDomain();
        }

        public string GetPortalUrl(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid article id", nameof(id));
            }

            return $"https://{_configuration.BillomatId}.billomat.net/app/{EntityUrlFragment}/show/entityId/{id}";
        }
    }
}
