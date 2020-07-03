using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Helpers;
using Develappers.BillomatNet.Queries;
using Invoice = Develappers.BillomatNet.Types.Invoice;
using InvoiceItem = Develappers.BillomatNet.Types.InvoiceItem;
using InvoiceDocument = Develappers.BillomatNet.Types.InvoiceDocument;
using System.Reflection;
using System;
using System.Globalization;
using System.Collections.Generic;
using Develappers.BillomatNet.Types;
using Newtonsoft.Json;

namespace Develappers.BillomatNet
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
        /// Cancels an invoice.
        /// </summary>
        /// <param name="id">The id of the invoice.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns></returns>
        public Task CancelAsync(int id, CancellationToken token = default(CancellationToken))
        {
            return PutAsync<object>($"/api/invoices/{id}/cancel", null, token);
        }

        /// <summary>
        /// Reverses the cancellation of an invoice.
        /// </summary>
        /// <param name="id">The id of the invoice.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns></returns>
        public Task UncancelAsync(int id, CancellationToken token = default(CancellationToken))
        {
            return PutAsync<object>($"/api/invoices/{id}/uncancel", null, token);
        }

        /// <summary>
        /// Completes an invoice.
        /// </summary>
        /// <param name="id">The id of the invoice.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns></returns>
        public Task CompleteAsync(int id, CancellationToken token = default(CancellationToken))
        {
            return CompleteInternalAsync(id, null, token);
        }

        /// <summary>
        /// Completes an invoice.
        /// </summary>
        /// <param name="id">The id of the invoice.</param>
        /// <param name="templateId">The template id.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns></returns>
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

        public async Task PostItemAsync(int clientId, string title, string clientAdress,
            DateTime date, ISupplyDate supplyDate, int dueDays, DateTime dueDate, string label, string intro, string note,
            float totalNet, float totalGross, float totalNetUnreduced, float totalGrossUnreduced,
            List<string> paymentTypes, float itemQuantity, float itemUnitPrice, string itemUnit, string itemTitle, string itemDescription, CancellationToken token = default(CancellationToken))
        {
            var totalBrutto = itemQuantity + itemUnitPrice;

            #region invoice
            var invoice = new Types.Invoice();
            invoice.ClientId = clientId;
            invoice.Title = title;
            invoice.Date = date;
            invoice.SupplyDate = supplyDate;
            invoice.DueDate = dueDate;
            //invoice.DueDays = dueDays; // maybe dcide between dueDays and due Date
            invoice.Address = clientAdress;
            invoice.Label = label;
            invoice.Intro = intro;
            invoice.Note = note;
            //invoice.TotalNet = totalNet;
            //invoice.TotalGross = totalGross;
            //invoice.TotalNetUnreduced = totalNetUnreduced;
            //invoice.TotalGrossUnreduced = totalGrossUnreduced;
            invoice.CurrencyCode = "EUR";
            invoice.NetGross = Types.NetGrossType.GROSS;
            invoice.PaymentTypes = paymentTypes;

            invoice.Quote = 1;
            #endregion

            var invObj = new Api.InvoiceWrapper { Invoice = Helpers.InvoiceMappingExtensions.ToApi(invoice) };
            var result = await PostAsync("/api/invoices", invObj, token);

            var newInvoice = JsonConvert.DeserializeObject<Api.InvoiceWrapper>(result);

            #region invoiceItem
            var invoiceItem = new Types.InvoiceItem();
            //invoiceItem.ArticleId = ;
            invoiceItem.InvoiceId = Convert.ToInt32(newInvoice.Invoice.Id);
            invoiceItem.Position = 1; // needs to be set with variable and not static
            invoiceItem.Unit = itemUnit;
            invoiceItem.Quantity = itemQuantity;
            invoiceItem.UnitPrice = itemUnitPrice;
            invoiceItem.TaxName = "Umsatzsteuer";
            invoiceItem.TaxRate = 19;
            invoiceItem.Title = itemTitle;
            invoiceItem.Description = itemDescription;
            //invoiceItem.TotalNet = invoice.TotalNet;
            //invoiceItem.TotalGross = invoice.TotalGross;
            //invoiceItem.Reduction = 0; needs to be a object (IReduction / PercentReduction or AbsoluteReduction
            //invoiceItem.TotalNetUnreduced = invoice.TotalNetUnreduced;
            //invoiceItem.TotalGrossUnreduced = invoice.TotalGrossUnreduced;
            #endregion
            var invItemObj = new Api.InvoiceItemWrapper { InvoiceItem = Helpers.InvoiceMappingExtensions.ToApi(invoiceItem) };

            await PostAsync("/api/invoice-items", invItemObj, token);
        }
    }
}