using System.Threading;
using System.Threading.Tasks;
using TaurusSoftware.BillomatNet.Api;
using TaurusSoftware.BillomatNet.Helpers;
using TaurusSoftware.BillomatNet.Queries;
using Invoice = TaurusSoftware.BillomatNet.Types.Invoice;
using InvoiceItem = TaurusSoftware.BillomatNet.Types.InvoiceItem;
using InvoiceDocument = TaurusSoftware.BillomatNet.Types.InvoiceDocument;

namespace TaurusSoftware.BillomatNet
{
    public class InvoiceService : ServiceBase
    {
        public InvoiceService(Configuration configuration) : base(configuration)
        {
        }

        public Task<Types.PagedList<Invoice>> GetListAsync(CancellationToken token = default(CancellationToken))
        {
            return GetListAsync(null, token);
        }

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
        /// Returns an invoice by it's id. 
        /// </summary>
        /// <param name="id">The id of the invoice.</param>
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
        /// <param name="id">The id of the invoice.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns></returns>
        public Task DeleteAsync(int id, CancellationToken token = default(CancellationToken))
        {
            return DeleteAsync($"/api/invoices/{id}", token);
        }

        /// <summary>
        /// Completes an invoice.
        /// </summary>
        /// <param name="id">The id of the invoice.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns></returns>
        public Task CompleteAsync(int id, CancellationToken token = default(CancellationToken))
        {
            var model = new CompleteInvoiceWrapper
            {
                Parameters = new CompleteInvoiceParameters()
            };
            return PutAsync($"/api/invoices/{id}/complete", model, token);
        }

        /// <summary>
        /// Completes an invoice.
        /// </summary>
        /// <param name="id">The id of the invoice.</param>
        /// <param name="templateId">The template id.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns></returns>
        public async Task CompleteAsync(int id, int templateId, CancellationToken token = default(CancellationToken))
        {
            var model = new CompleteInvoiceWrapper
            {
                Parameters = new CompleteInvoiceParameters
                {
                    TemplateId = templateId
                }
            };
            await PutAsync($"/api/invoices/{id}/complete", model, token).ConfigureAwait(false);
        }

        public async Task<Types.PagedList<InvoiceItem>> GetItemsAsync(int invoiceId, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetListAsync<InvoiceItemListWrapper>("/api/invoice-items", $"invoice_id={invoiceId}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        public async Task<InvoiceItem> GetItemByIdAsync(int id, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetItemByIdAsync<InvoiceItemWrapper>($"/api/invoice-items/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }
    }
}