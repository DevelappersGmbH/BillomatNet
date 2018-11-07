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
            var jsonModel = await GetListAsync<InvoiceListWrapper>("/api/invoices", QueryString.For(query), token);
            return jsonModel.ToDomain();
        }

        public async Task<InvoiceDocument> GetPdfAsync(int id, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetItemByIdAsync<InvoiceDocumentWrapper>($"/api/invoices/{id}/pdf", token);
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
            var jsonModel = await GetItemByIdAsync<InvoiceWrapper>($"/api/invoices/{id}", token);
            return jsonModel.ToDomain();
        }

        public async Task<Types.PagedList<InvoiceItem>> GetItemsAsync(int invoiceId, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetListAsync<InvoiceItemListWrapper>("/api/invoice-items", $"invoice_id={invoiceId}", token);
            return jsonModel.ToDomain();
        }

        public async Task<InvoiceItem> GetItemByIdAsync(int id, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetItemByIdAsync<InvoiceItemWrapper>($"/api/invoice-items/{id}", token);
            return jsonModel.ToDomain();
        }
    }
}