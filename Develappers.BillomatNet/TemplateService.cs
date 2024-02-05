// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Develappers.BillomatNet.Api.Net;
using Develappers.BillomatNet.Queries;
using Develappers.BillomatNet.Types;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Mapping;
using Template = Develappers.BillomatNet.Types.Template;

namespace Develappers.BillomatNet
{
    public class TemplateService : ServiceBase,
        IEntityService<Template, TemplateFilter>
    {
        private const string EntityUrlFragment = "templates";

        /// <summary>
        /// Creates a new instance of <see cref="TemplateService"/>.
        /// </summary>
        /// <param name="httpClient">The http client.</param>

        public TemplateService(IHttpClient httpClient) : base(httpClient)
        {
        }

        /// <summary>
        /// Retrieves a list of all templates.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the list of templates.
        /// </returns>

        public Task<Types.PagedList<Template>> GetListAsync(CancellationToken token = default)
        {
            return GetListAsync(null, token);
        }

        /// <summary>
        /// Retrieves a list of all templates.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the list of templates.
        /// </returns>

        public async Task<Types.PagedList<Template>> GetListAsync(Query<Template, TemplateFilter> query, CancellationToken token = default)
        {
            var jsonModel = await GetListAsync<TemplateListWrapper>($"/api/{EntityUrlFragment}", QueryString.For(query), token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Retrieves a template by it's ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the template.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>

        public async Task<Template> GetByIdAsync(int id, CancellationToken token = default)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid template id", nameof(id));
            }

            var jsonModel = await GetItemByIdAsync<TemplateWrapper>($"/api/{EntityUrlFragment}/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        public Task DeleteAsync(int id, CancellationToken token = default)
        {
            throw new NotImplementedException("This service is not implemented by now. You can help us by contributing to our project on github.");
        }

        Task<Template> IEntityService<Template, TemplateFilter>.CreateAsync(Template model, CancellationToken token)
        {
            throw new NotImplementedException("This service is not implemented by now. You can help us by contributing to our project on github.");
        }

        Task<Template> IEntityService<Template, TemplateFilter>.EditAsync(Template model, CancellationToken token)
        {
            throw new NotImplementedException("This service is not implemented by now. You can help us by contributing to our project on github.");
        }

    }
}
