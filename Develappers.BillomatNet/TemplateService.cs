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

        public TemplateService(IHttpClient httpClient) : base(httpClient)
        {
        }

        public Task<Types.PagedList<Template>> GetListAsync(CancellationToken token = default)
        {
            return GetListAsync(null, token);
        }

        public async Task<Types.PagedList<Template>> GetListAsync(Query<Template, TemplateFilter> query, CancellationToken token = default)
        {
            var jsonModel = await GetListAsync<TemplateListWrapper>($"/api/{EntityUrlFragment}", QueryString.For(query), token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

    }
}
