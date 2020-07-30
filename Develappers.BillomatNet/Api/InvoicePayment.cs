using System;
using System.Collections.Generic;
using System.Text;

namespace Develappers.BillomatNet.Api
{
    internal class InvoicePayment
    {
        public string Id { get; set; }
        public string Created { get; set; }
        public string InvoiceId { get; set; }
        public string UserId { get; set; }
        public string Date { get; set; }
        public string Amount { get; set; }
        public string Comment { get; set; }
        public string TransactionPurpose { get; set; }
        public string CurrencyCode { get; set; }
        public string Quote { get; set; }
        public string Type { get; set; }
        public string MarkInvoiceAsPaid { get; set; }
    }
}
