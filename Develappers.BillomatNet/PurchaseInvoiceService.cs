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
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet
{
    public class PurchaseInvoiceService : ServiceBase,
        IEntityService<PurchaseInvoice, PurchaseInvoiceFilter>
    {
        private const string EntityUrlFragment = "incomings";

        /// <summary>
        /// Creates a new instance of <see cref="PurchaseInvoiceService"/>.
        /// </summary>
        /// <param name="httpClient">The http client.</param>
        public PurchaseInvoiceService(IHttpClient httpClient) : base(httpClient)
        {
        }

        /// <summary>
        /// Gets the portal URL for this entity.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The url to this entity in billomat portal.</returns>
        /// <exception cref="ArgumentException">Thrown when the id is invalid.</exception>
        public string GetPortalUrl(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid incoming invoice id", nameof(id));
            }

            return $"{HttpClient.BaseUrl}app/beta/{EntityUrlFragment}/{id}";
        }

        /// <summary>
        /// Retrieves a list of all incoming invoices.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the list of incoming invoices.
        /// </returns>
        public Task<Types.PagedList<PurchaseInvoice>> GetListAsync(CancellationToken token = default)
        {
            return GetListAsync(null, token);
        }

        /// <summary>
        /// Retrieves a list of all incoming invoices.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the list of incoming invoices.
        /// </returns>
        public async Task<Types.PagedList<PurchaseInvoice>> GetListAsync(Query<PurchaseInvoice, PurchaseInvoiceFilter> query, CancellationToken token)
        {
            var jsonModel = await GetListAsync<IncomingListWrapper>($"/api/{EntityUrlFragment}", QueryString.For(query), token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Retrieves an purchase invoice by it's ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the supplier.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<PurchaseInvoice> GetByIdAsync(int id, CancellationToken token = default)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid purchase invoice id", nameof(id));
            }

            var jsonModel = await GetItemByIdAsync<IncomingWrapper>($"/api/{EntityUrlFragment}/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Gets the PDF as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the purchase invoice document.
        /// </returns>
        public async Task<PurchaseInvoiceDocument> GetPdfAsync(int id, CancellationToken token = default)
        {
            var jsonModel = await GetItemByIdAsync<IncomingDocumentWrapper>($"/api/{EntityUrlFragment}/{id}/pdf", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        Task IEntityService<PurchaseInvoice, PurchaseInvoiceFilter>.DeleteAsync(int id, CancellationToken token)
        {
            throw new NotImplementedException("This service is not implemented by now. You can help us by contributing to our project on github.");
        }

        Task<PurchaseInvoice> IEntityService<PurchaseInvoice, PurchaseInvoiceFilter>.CreateAsync(PurchaseInvoice model, CancellationToken token)
        {
            throw new NotImplementedException("This service is not implemented by now. You can help us by contributing to our project on github.");
        }

        Task<PurchaseInvoice> IEntityService<PurchaseInvoice, PurchaseInvoiceFilter>.EditAsync(PurchaseInvoice model, CancellationToken token)
        {
            throw new NotImplementedException("This service is not implemented by now. You can help us by contributing to our project on github.");
        }
    }
}
