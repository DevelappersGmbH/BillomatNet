using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Helpers;
using Develappers.BillomatNet.Queries;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Invoice = Develappers.BillomatNet.Types.Invoice;
using InvoiceDocument = Develappers.BillomatNet.Types.InvoiceDocument;
using InvoiceItem = Develappers.BillomatNet.Types.InvoiceItem;

namespace Develappers.BillomatNet
{
    public class InvoiceService : ServiceBase, IEntityService<Invoice, InvoiceFilter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public InvoiceService(Configuration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Retrieves a list of all invoices.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The invoice list or null if not found.</returns>
        public Task<Types.PagedList<Invoice>> GetListAsync(CancellationToken token = default(CancellationToken))
        {
            return GetListAsync(null, token);
        }

        /// <summary>
        /// Retrieves a list of invoices appropriate to the filter.
        /// </summary>
        /// <param name="query">The filter with the property and value</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The invoice list or null if not found.</returns>
        public async Task<Types.PagedList<Invoice>> GetListAsync(Query<Invoice, InvoiceFilter> query, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetListAsync<InvoiceListWrapper>("/api/invoices", QueryString.For(query), token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        public async Task<InvoiceDocument> GetPdfAsync(int id, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetItemByIdAsync<InvoiceDocumentWrapper>($"/api/invoices/{id}/pdf", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Returns an invoice by it's ID. 
        /// </summary>
        /// <param name="id">The ID of the invoice.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The invoice or null if not found.</returns>
        public async Task<Invoice> GetByIdAsync(int id, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetItemByIdAsync<InvoiceWrapper>($"/api/invoices/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Deletes an invoice.
        /// </summary>
        /// <param name="id">The ID of the invoice.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public Task DeleteAsync(int id, CancellationToken token = default(CancellationToken))
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid invoice id", nameof(id));
            }
            return DeleteAsync($"/api/invoices/{id}", token);
        }

        /// <summary>
        /// Deletes an invoice item.
        /// </summary>
        /// <param name="id">The ID of the invoice item.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public Task DeleteInvoiceItemAsync(int id, CancellationToken token = default)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid invoice item id", nameof(id));
            }
            return DeleteAsync($"/api/invoice-items/{id}", token);
        }

        /// <summary>
        /// Cancels an invoice.
        /// </summary>
        /// <param name="id">The ID of the invoice.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task CancelAsync(int id, CancellationToken token = default(CancellationToken))
        {
            return PutAsync<object>($"/api/invoices/{id}/cancel", null, token);
        }

        /// <summary>
        /// Reverses the cancellation of an invoice.
        /// </summary>
        /// <param name="id">The ID of the invoice.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task UncancelAsync(int id, CancellationToken token = default(CancellationToken))
        {
            return PutAsync<object>($"/api/invoices/{id}/uncancel", null, token);
        }

        /// <summary>
        /// Completes an invoice.
        /// </summary>
        /// <param name="id">The id of the invoice.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task CompleteAsync(int id, CancellationToken token = default(CancellationToken))
        {
            return CompleteInternalAsync(id, null, token);
        }

        /// <summary>
        /// Completes an invoice.
        /// </summary>
        /// <param name="id">The ID of the invoice.</param>
        /// <param name="templateId">The template ID.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task CompleteAsync(int id, int templateId, CancellationToken token = default(CancellationToken))
        {
            return CompleteInternalAsync(id, templateId, token);
        }

        private async Task CompleteInternalAsync(int id, int? templateId, CancellationToken token)
        {
            var model = new CompleteInvoiceWrapper
            {
                Parameters = new CompleteInvoiceParameters
                {
                    TemplateId = templateId
                }
            };
            await PutAsync<object, CompleteInvoiceWrapper>($"/api/invoices/{id}/complete", model, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves a list of the items (articles) used in the invoice.
        /// </summary>
        /// <param name="invoiceId">The ID of the incoice with the items.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The invoice items list or null if not found.</returns>
        public async Task<Types.PagedList<InvoiceItem>> GetItemsAsync(int invoiceId, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetListAsync<InvoiceItemListWrapper>("/api/invoice-items", $"invoice_id={invoiceId}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Returns and invoice by it's ID.
        /// </summary>
        /// <param name="id">The ID of the invoice.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The invoice or null if not found.</returns>
        public async Task<InvoiceItem> GetItemByIdAsync(int id, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetItemByIdAsync<InvoiceItemWrapper>($"/api/invoice-items/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Creates an invoice.
        /// </summary>
        /// <param name="model">The invoice object.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result returns the newly created invoice with the ID.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<Invoice> CreateAsync(Invoice model, CancellationToken token = default(CancellationToken))
        {
            if (model == null || model.ClientId == 0 || model.Quote < 1 || model.Date == DateTime.MinValue)
            {
                throw new ArgumentException("invoice or a value of the invoice is null", nameof(model));
            }
            if (model.Id != 0)
            {
                throw new ArgumentException("invalid invoice id", nameof(model));
            }
            var wrappedModel = new InvoiceWrapper
            {
                Invoice = model.ToApi()
            };
            var result = await PostAsync("/api/invoices", wrappedModel, token);

            return result.ToDomain();
        }

        /// <summary>
        /// Creates an invoice item.
        /// </summary>
        /// <param name="model">The invoice item.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result returns the newly created invoice with the ID.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<InvoiceItem> CreateAsync(InvoiceItem model, CancellationToken token = default)
        {
            if (model == null || model.InvoiceId <= 0)
            {
                throw new ArgumentException("invoice item or a value of the invoice item is null", nameof(model));
            }
            if (model.Id != 0)
            {
                throw new ArgumentException("invalid invoice item id", nameof(model));
            }
            var wrappedModel = new InvoiceItemWrapper
            {
                InvoiceItem = model.ToApi()
            };
            try
            {
                var result = await PostAsync("/api/invoice-items", wrappedModel, token);
                return result.ToDomain();
            }
            catch (WebException wex)
                when (wex.Status == WebExceptionStatus.ProtocolError && (wex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ArgumentException($"wrong input parameter", nameof(model), wex);
            }
        }

        /// <summary>
        /// Creates / Edits an invoice property.
        /// </summary>
        /// <param name="model">The invoice property.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the new invoice property.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<Invoice> EditAsync(Invoice model, CancellationToken token = default)
        {
            if (model == null || model.ClientId == 0 || model.Quote < 1 || model.Date == DateTime.MinValue)
            {
                throw new ArgumentException("invoice or a value of the invoice is null", nameof(model));
            }
            if (model.Id <= 0)
            {
                throw new ArgumentException("invalid invoice id", nameof(model));
            }

            var wrappedModel = new InvoiceWrapper
            {
                Invoice = model.ToApi()
            };
            try
            {
                var jsonModel = await PutAsync($"/api/invoices/{model.Id}", wrappedModel, token).ConfigureAwait(false);
                return jsonModel.ToDomain();
            }
            catch (WebException wex)
                when (wex.Status == WebExceptionStatus.ProtocolError && (wex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ArgumentException($"wrong input parameter", nameof(model), wex);
            }
        }

        /// <summary>
        /// Edits an invoice item.
        /// </summary>
        /// <param name="model">The invoice item.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the new invoice item.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<InvoiceItem> EditAsync(InvoiceItem model, CancellationToken token = default)
        {
            if (model == null || model.InvoiceId <=0)
            {
                throw new ArgumentException("invoice item or a value of the invoice item is null", nameof(model));
            }
            if (model.Id <= 0)
            {
                throw new ArgumentException("invalid invoice item id", nameof(model));
            }

            var wrappedModel = new InvoiceItemWrapper
            {
                InvoiceItem = model.ToApi()
            };
            try
            {
                var jsonModel = await PutAsync($"/api/invoice-items/{model.Id}", wrappedModel, token).ConfigureAwait(false);
                return jsonModel.ToDomain();
            }
            catch (WebException wex)
                when (wex.Status == WebExceptionStatus.ProtocolError && (wex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ArgumentException($"wrong input parameter", nameof(model), wex);
            }
        }
    }
}