﻿using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaurusSoftware.BillomatNet.Api;
using TaurusSoftware.BillomatNet.Api.Net;
using TaurusSoftware.BillomatNet.Helpers;
using TaurusSoftware.BillomatNet.Queries;
using Invoice = TaurusSoftware.BillomatNet.Types.Invoice;
using InvoiceDocument = TaurusSoftware.BillomatNet.Types.InvoiceDocument;

namespace TaurusSoftware.BillomatNet
{
    public class InvoiceService : ServiceBase
    {
        public InvoiceService(Configuration configuration) : base(configuration)
        {
        }

        public Task<PagedList<Invoice>> GetListAsync(CancellationToken token = default(CancellationToken))
        {
            return GetListAsync(null, token);
        }

        public async Task<PagedList<Invoice>> GetListAsync(Query<Invoice, InvoiceFilter> query, CancellationToken token = default(CancellationToken))
        {
            var httpClient = new HttpClient(Configuration.BillomatId, Configuration.ApiKey);
            var httpResponse = await httpClient.GetAsync(new Uri("/api/invoices", UriKind.Relative), QueryString.For(query), token);
            var jsonModel = JsonConvert.DeserializeObject<InvoiceListWrapper>(httpResponse);
            return jsonModel.ToDomain();
        }

        public async Task<InvoiceDocument> GetPdfAsync(int id, CancellationToken token = default(CancellationToken))
        {
            var httpClient = new HttpClient(Configuration.BillomatId, Configuration.ApiKey);
            var httpResponse = await httpClient.GetAsync(new Uri($"/api/invoices/{id}/pdf", UriKind.Relative), token);
            var jsonModel = JsonConvert.DeserializeObject<InvoiceDocumentWrapper>(httpResponse);
            return jsonModel.ToDomain();
        }
     }
}