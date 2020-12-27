// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Api.Net;
using Develappers.BillomatNet.Mapping;
using Develappers.BillomatNet.Queries;
using Invoice = Develappers.BillomatNet.Types.Invoice;
using InvoiceDocument = Develappers.BillomatNet.Types.InvoiceDocument;
using InvoiceItem = Develappers.BillomatNet.Types.InvoiceItem;
using InvoiceMail = Develappers.BillomatNet.Types.InvoiceMail;
using InvoiceComment = Develappers.BillomatNet.Types.InvoiceComment;
using InvoicePayment = Develappers.BillomatNet.Types.InvoicePayment;
using InvoiceTag = Develappers.BillomatNet.Types.InvoiceTag;
using TagCloudItem = Develappers.BillomatNet.Types.TagCloudItem;

namespace Develappers.BillomatNet
{
    public class InvoiceService : ServiceBase,
        IEntityService<Invoice, InvoiceFilter>
    {
        private readonly Configuration _configuration;
        private const string EntityUrlFragment = "invoices";

        /// <summary>
        /// Creates a new instance of <see cref="InvoiceService"/>.
        /// </summary>
        /// <param name="configuration">The service configuration.</param>
        public InvoiceService(Configuration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Creates a new instance of <see cref="InvoiceService"/> for unit tests.
        /// </summary>
        /// <param name="httpClientFactory">The function which creates a new <see cref="IHttpClient" /> implementation.</param>
        /// <exception cref="ArgumentNullException">Thrown when the parameter is null.</exception>
        internal InvoiceService(Func<IHttpClient> httpClientFactory) : base(httpClientFactory)
        {
        }

        /// <summary>
        /// Retrieves a list of all invoices.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The invoice list or null if not found.</returns>
        public Task<Types.PagedList<Invoice>> GetListAsync(CancellationToken token = default)
        {
            return GetListAsync(null, token);
        }

        /// <summary>
        /// Retrieves a list of invoices appropriate to the filter.
        /// </summary>
        /// <param name="query">The filter with the property and value</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The invoice list or null if not found.</returns>
        public async Task<Types.PagedList<Invoice>> GetListAsync(Query<Invoice, InvoiceFilter> query, CancellationToken token = default)
        {
            var jsonModel = await GetListAsync<InvoiceListWrapper>($"/api/{EntityUrlFragment}", QueryString.For(query), token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Returns an invoice by it's ID. 
        /// </summary>
        /// <param name="id">The ID of the invoice.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The invoice or null if not found.</returns>
        public async Task<Invoice> GetByIdAsync(int id, CancellationToken token = default)
        {
            var jsonModel = await GetItemByIdAsync<InvoiceWrapper>($"/api/{EntityUrlFragment}/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Returns and invoice by it's ID.
        /// </summary>
        /// <param name="id">The ID of the invoice.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The invoice or null if not found.</returns>
        public async Task<InvoiceItem> GetItemByIdAsync(int id, CancellationToken token = default)
        {
            var jsonModel = await GetItemByIdAsync<InvoiceItemWrapper>($"/api/invoice-items/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Creates an invoice.
        /// </summary>
        /// <param name="value">The invoice object.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result returns the newly created invoice with the ID.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<Invoice> CreateAsync(Invoice value, CancellationToken token = default)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.ClientId == 0)
            {
                throw new ArgumentException("invalid client id", nameof(value));
            }

            if (value.Quote <= 0f)
            {
                throw new ArgumentException("invalid quote", nameof(value));
            }

            if (value.Date == DateTime.MinValue)
            {
                throw new ArgumentException("invalid date", nameof(value));
            }

            if (value.Id != 0)
            {
                throw new ArgumentException("invalid invoice id", nameof(value));
            }

            var wrappedModel = new InvoiceWrapper
            {
                Invoice = value.ToApi()
            };

            var result = await PostAsync($"/api/{EntityUrlFragment}", wrappedModel, token);

            return result.ToDomain();
        }

        /// <summary>
        /// Creates / Edits an invoice property.
        /// </summary>
        /// <param name="value">The invoice property.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the new invoice property.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<Invoice> EditAsync(Invoice value, CancellationToken token = default)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.ClientId == 0)
            {
                throw new ArgumentException("invalid client id", nameof(value));
            }

            if (value.Quote <= 0f)
            {
                throw new ArgumentException("invalid quote", nameof(value));
            }

            if (value.Date == DateTime.MinValue)
            {
                throw new ArgumentException("invalid date", nameof(value));
            }

            if (value.Id <= 0)
            {
                throw new ArgumentException("invalid invoice id", nameof(value));
            }

            var wrappedModel = new InvoiceWrapper
            {
                Invoice = value.ToApi()
            };

            try
            {
                var jsonModel = await PutAsync($"/api/{EntityUrlFragment}/{value.Id}", wrappedModel, token).ConfigureAwait(false);
                return jsonModel.ToDomain();
            }
            catch (WebException wex)
                when (wex.Status == WebExceptionStatus.ProtocolError && (wex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ArgumentException("wrong input parameter", nameof(value), wex);
            }
        }

        /// <summary>
        /// Cancels an invoice.
        /// </summary>
        /// <param name="id">The ID of the invoice.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task CancelAsync(int id, CancellationToken token = default)
        {
            return PutAsync<object>($"/api/{EntityUrlFragment}/{id}/cancel", null, token);
        }

        /// <summary>
        /// Reverses the cancellation of an invoice.
        /// </summary>
        /// <param name="id">The ID of the invoice.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task UncancelAsync(int id, CancellationToken token = default)
        {
            return PutAsync<object>($"/api/{EntityUrlFragment}/{id}/uncancel", null, token);
        }

        /// <summary>
        /// Completes an invoice.
        /// </summary>
        /// <param name="id">The id of the invoice.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task CompleteAsync(int id, CancellationToken token = default)
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
        public Task CompleteAsync(int id, int templateId, CancellationToken token = default)
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
            await PutAsync<object, CompleteInvoiceWrapper>($"/api/{EntityUrlFragment}/{id}/complete", model, token).ConfigureAwait(false);
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
        public Task DeleteAsync(int id, CancellationToken token = default)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid invoice id", nameof(id));
            }
            return DeleteAsync($"/api/{EntityUrlFragment}/{id}", token);
        }


        /// <summary>
        /// Retrieves a list of the items (articles) used in the invoice.
        /// </summary>
        /// <param name="invoiceId">The ID of the invoice.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The invoice items list or null if not found.</returns>
        public async Task<Types.PagedList<InvoiceItem>> GetItemsAsync(int invoiceId, CancellationToken token = default)
        {
            var jsonModel = await GetListAsync<InvoiceItemListWrapper>("/api/invoice-items", $"invoice_id={invoiceId}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Creates an invoice item.
        /// </summary>
        /// <param name="value">The invoice item.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result returns the newly created invoice with the ID.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<InvoiceItem> CreateItemAsync(InvoiceItem value, CancellationToken token = default)
        {
            if (value == null || value.InvoiceId <= 0)
            {
                throw new ArgumentException("invoice item or a value of the invoice item is null", nameof(value));
            }
            if (value.Id != 0)
            {
                throw new ArgumentException("invalid invoice item id", nameof(value));
            }
            var wrappedModel = new InvoiceItemWrapper
            {
                InvoiceItem = value.ToApi()
            };
            try
            {
                var result = await PostAsync("/api/invoice-items", wrappedModel, token);
                return result.ToDomain();
            }
            catch (WebException wex)
                when (wex.Status == WebExceptionStatus.ProtocolError && (wex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ArgumentException("wrong input parameter", nameof(value), wex);
            }
        }

        /// <summary>
        /// Edits an invoice item.
        /// </summary>
        /// <param name="value">The invoice item.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the new invoice item.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<InvoiceItem> EditItemAsync(InvoiceItem value, CancellationToken token = default)
        {
            if (value == null || value.InvoiceId <= 0)
            {
                throw new ArgumentException("invoice item or a value of the invoice item is null", nameof(value));
            }
            if (value.Id <= 0)
            {
                throw new ArgumentException("invalid invoice item id", nameof(value));
            }

            var wrappedModel = new InvoiceItemWrapper
            {
                InvoiceItem = value.ToApi()
            };
            try
            {
                var jsonModel = await PutAsync($"/api/invoice-items/{value.Id}", wrappedModel, token).ConfigureAwait(false);
                return jsonModel.ToDomain();
            }
            catch (WebException wex)
                when (wex.Status == WebExceptionStatus.ProtocolError && (wex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ArgumentException("wrong input parameter", nameof(value), wex);
            }
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

        public async Task<InvoiceDocument> GetPdfAsync(int id, CancellationToken token = default)
        {
            var jsonModel = await GetItemByIdAsync<InvoiceDocumentWrapper>($"/api/{EntityUrlFragment}/{id}/pdf", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
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
                throw new ArgumentException("invalid invoice id", nameof(id));
            }

            return $"https://{_configuration.BillomatId}.billomat.net/app/{EntityUrlFragment}/show/entityId/{id}";
        }

        /// <summary>
        /// Sends the Invoice as E-Mail to the client.
        /// </summary>
        /// <param name="id">The ID of the invoice.</param>
        /// <param name="model">The mail.</param>
        /// <param name="token">The token</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public Task SendMailAsync(int id, InvoiceMail model, CancellationToken token = default)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (model.Recipients == null)
            {
                throw new ArgumentException("email or a value of the email is null", nameof(model));
            }
            if (id <= 0)
            {
                throw new ArgumentException("invalid invoice id", nameof(id));
            }
            var wrappedModel = new InvoiceMailWrapper
            {
                InvoiceMail = model.ToApi()
            };
            return PostAsync($"/api/{EntityUrlFragment}/{id}/email", wrappedModel, token);
        }

        /// <summary>
        /// Retrieves a list of invoice comments appropriate to the filter.
        /// </summary>
        /// <param name="query">The filter.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the filtered list of invoice comment.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<Types.PagedList<InvoiceComment>> GetCommentListAsync(Query<InvoiceComment, InvoiceCommentFilter> query, CancellationToken token = default)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            if (query.Filter?.InvoiceId.GetValueOrDefault(0) <= 0)
            {
                throw new ArgumentException("a required value of the filter invalid", nameof(query));
            }
            var jsonModel = await GetListAsync<InvoiceCommentListWrapper>("/api/invoice-comments", QueryString.For(query), token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Retrieves an invoice comment by it's ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the invoice comment.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<InvoiceComment> GetCommentByIdAsync(int id, CancellationToken token = default)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid invoice comment id", nameof(id));
            }
            var jsonModel = await GetItemByIdAsync<InvoiceCommentWrapper>($"/api/invoice-comments/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Creates an invoice comment.
        /// </summary>
        /// <param name="value">The invoice comment.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result returns the newly created invoice comment with the ID.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<InvoiceComment> CreateCommentAsync(InvoiceComment value, CancellationToken token = default)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (value.InvoiceId == 0 || string.IsNullOrEmpty(value.Comment) || value.Id != 0)
            {
                throw new ArgumentException("invalid property values for invoice comment", nameof(value));
            }
            var wrappedModel = new InvoiceCommentWrapper
            {
                InvoiceComment = value.ToApi()
            };
            try
            {
                var result = await PostAsync("/api/invoice-comments", wrappedModel, token);
                return result.ToDomain();
            }
            catch (WebException wex)
                when (wex.Status == WebExceptionStatus.ProtocolError && (wex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ArgumentException("wrong input parameter", nameof(value), wex);
            }

        }

        /// <summary>
        /// Deletes an invoice comment.
        /// </summary>
        /// <param name="id">The ID of the invoice tag.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public Task DeleteCommentAsync(int id, CancellationToken token = default)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid invoice comment id", nameof(id));
            }
            return DeleteAsync($"/api/invoice-comments/{id}", token);
        }

        /// <summary>
        /// Retrieves a list of all invoice payments.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the invoice payment list.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public Task<Types.PagedList<InvoicePayment>> GetPaymentListAsync(CancellationToken token = default)
        {
            return GetPaymentListAsync(null, token);
        }

        /// <summary>
        /// Retrieves a list of invoice payments appropriate to the filter.
        /// </summary>
        /// <param name="query">The filter with the property and value</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the invoice payment list.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<Types.PagedList<InvoicePayment>> GetPaymentListAsync(Query<InvoicePayment, InvoicePaymentFilter> query, CancellationToken token = default)
        {
            var jsonModel = await GetListAsync<InvoicePaymentListWrapper>("/api/invoice-payments", QueryString.For(query), token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Retrieves an invoice payment by it's ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the invoice payment.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<InvoicePayment> GetPaymentByIdAsync(int id, CancellationToken token = default)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid invoice comment id", nameof(id));
            }
            var jsonModel = await GetItemByIdAsync<InvoicePaymentWrapper>($"/api/invoice-payments/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Creates an invoice payment.
        /// </summary>
        /// <param name="value">The invoice payment.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result returns the newly created invoice payment with the ID.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<InvoicePayment> CreatePaymentAsync(InvoicePayment value, CancellationToken token = default)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (value.InvoiceId == 0 || value.Amount <= 0 || value.Id != 0)
            {
                throw new ArgumentException("invalid property values for invoice payment", nameof(value));
            }
            var wrappedModel = new InvoicePaymentWrapper
            {
                InvoicePayment = value.ToApi()
            };
            try
            {
                var result = await PostAsync("/api/invoice-payments", wrappedModel, token);
                return result.ToDomain();
            }
            catch (WebException wex)
                when (wex.Status == WebExceptionStatus.ProtocolError && (wex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ArgumentException("wrong input parameter", nameof(value), wex);
            }
        }

        /// <summary>
        /// Deletes an invoice payment.
        /// </summary>
        /// <param name="id">The ID of the invoice payment.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public Task DeletePaymentAsync(int id, CancellationToken token = default)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid invoice payment id", nameof(id));
            }
            return DeleteAsync($"/api/invoice-payments/{id}", token);
        }

        /// <summary>
        /// Retrieves the invoice tag cloud.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the paged list of tag cloud items.
        /// </returns>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<Types.PagedList<TagCloudItem>> GetTagCloudAsync(CancellationToken token = default)
        {
            var jsonModel = await GetListAsync<InvoiceTagCloudItemListWrapper>("/api/invoice-tags", null, token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Retrieves a list of invoice tags appropriate to the filter.
        /// </summary>
        /// <param name="query">The filter with the property and value</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the invoice tag list.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<Types.PagedList<InvoiceTag>> GetTagListAsync(Query<InvoiceTag, InvoiceTagFilter> query, CancellationToken token = default)
        {
            if (query?.Filter == null)
            {
                throw new ArgumentException("filter has to be set", nameof(query));
            }

            var jsonModel = await GetListAsync<InvoiceTagListWrapper>("/api/invoice-tags", QueryString.For(query), token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Retrieves an invoice tag by it's ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the invoice tag.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<InvoiceTag> GetTagByIdAsync(int id, CancellationToken token = default)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid invoice tag id", nameof(id));
            }
            var jsonModel = await GetItemByIdAsync<InvoiceTagWrapper>($"/api/invoice-tags/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Creates an invoice tag.
        /// </summary>
        /// <param name="value">The invoice tag.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result returns the newly created invoice tag with the ID.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the parameter check fails.</exception>
        /// <exception cref="NotAuthorizedException">Thrown when not authorized to access this resource.</exception>
        /// <exception cref="NotFoundException">Thrown when the resource url could not be found.</exception>
        public async Task<InvoiceTag> CreateTagAsync(InvoiceTag value, CancellationToken token = default)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (value.InvoiceId == 0 || string.IsNullOrEmpty(value.Name) || value.Id != 0)
            {
                throw new ArgumentException("invalid property values for invoice payment", nameof(value));
            }
            var wrappedModel = new InvoiceTagWrapper
            {
                InvoiceTag = value.ToApi()
            };
            try
            {
                var result = await PostAsync("/api/invoice-tags", wrappedModel, token);
                return result.ToDomain();
            }
            catch (WebException wex)
                when (wex.Status == WebExceptionStatus.ProtocolError && (wex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ArgumentException("wrong input parameter", nameof(value), wex);
            }
        }

        /// <summary>
        /// Deletes an invoice tag.
        /// </summary>
        /// <param name="id">The ID of the invoice tag.</param>
        /// <param name="token">The cancellation token.</param>
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
                throw new ArgumentException("invalid invoice tag id", nameof(id));
            }
            return DeleteAsync($"/api/invoice-tags/{id}", token);
        }
    }
}
