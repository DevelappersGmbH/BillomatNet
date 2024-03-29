﻿// Licensed to the .NET Foundation under one or more agreements.
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

        public InboxDocumentService(IHttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<Types.PagedList<InboxDocument>> GetListAsync(CancellationToken token = default)
        {
            var jsonModel = await GetListAsync<InboxDocumentListWrapper>($"/api/{EntityUrlFragment}", QueryString.For((Query<InboxDocument, InboxDocumentFilter>)null), token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        Task<Types.PagedList<InboxDocument>> IEntityService<InboxDocument, InboxDocumentFilter>.GetListAsync(Query<InboxDocument, InboxDocumentFilter> query, CancellationToken token)
        {
            throw new NotSupportedException("not supported by api");
        }

        /// <summary>
        /// Returns an inbox document by it's ID. 
        /// </summary>
        /// <param name="id">The ID of the inbox document.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the inbox document.</returns>
        public async Task<InboxDocument> GetByIdAsync(int id, CancellationToken token = default)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid inbox document id", nameof(id));
            }

            var jsonModel = await GetItemByIdAsync<InboxDocumentWrapper>($"/api/{EntityUrlFragment}/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Deletes an inbox document.
        /// </summary>
        /// <param name="id">The ID of the inbox document.</param>
        /// <param name="token">The cancellation token.</param>
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
                throw new ArgumentException("invalid inbox document id", nameof(id));
            }
            return DeleteAsync($"/api/{EntityUrlFragment}/{id}", token);
        }

        Task<InboxDocument> IEntityService<InboxDocument, InboxDocumentFilter>.CreateAsync(InboxDocument model, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        Task<InboxDocument> IEntityService<InboxDocument, InboxDocumentFilter>.EditAsync(InboxDocument model, CancellationToken token)
        {
            throw new NotSupportedException("not supported by api");
        }
    }
}
