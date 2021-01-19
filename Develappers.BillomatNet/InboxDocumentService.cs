// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Api.Net;
using Develappers.BillomatNet.Mapping;
using Develappers.BillomatNet.Queries;
using InboxDocument = Develappers.BillomatNet.Types.InboxDocument;

namespace Develappers.BillomatNet
{
    public class InboxDocumentService : ServiceBase,
        IEntityService<InboxDocument, InboxDocumentFilter>
    {
        private const string EntityUrlFragment = "inbox-documents";

        public InboxDocumentService(Configuration configuration) : base(configuration)
        {
        }

        public InboxDocumentService(Func<IHttpClient> httpClientFactory) : base(httpClientFactory)
        {
        }

        public Task<Types.PagedList<InboxDocument>> GetListAsync(CancellationToken token = default)
        {
            return GetListAsync(null, token);
        }

        public async Task<Types.PagedList<InboxDocument>> GetListAsync(Query<InboxDocument, InboxDocumentFilter> query, CancellationToken token)
        {
            var jsonModel = await GetListAsync<InboxDocumentListWrapper>($"/api/{EntityUrlFragment}", QueryString.For(query), token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        public async Task<InboxDocument> GetByIdAsync(int id, CancellationToken token = default)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid inbox document id", nameof(id));
            }

            var jsonModel = await GetItemByIdAsync<InboxDocumentWrapper>($"/api/{EntityUrlFragment}/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        Task IEntityService<InboxDocument, InboxDocumentFilter>.DeleteAsync(int id, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        Task<InboxDocument> IEntityService<InboxDocument, InboxDocumentFilter>.CreateAsync(InboxDocument model, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        Task<InboxDocument> IEntityService<InboxDocument, InboxDocumentFilter>.EditAsync(InboxDocument model, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
