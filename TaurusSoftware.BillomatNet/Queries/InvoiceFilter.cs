using System;
using System.Collections.Generic;
using TaurusSoftware.BillomatNet.Types;

namespace TaurusSoftware.BillomatNet.Queries
{
    public class InvoiceFilter
    {
        public int? ClientId { get; set; }
        public int? ContactId { get; set; }
        public string InvoiceNumber { get; set; }
        public List<InvoiceStatus> Status { get; set; }
        public List<PaymentType> PaymentType { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string Label { get; set; }
        public string Intro { get; set; }
        public string Note { get; set; }
        public List<string> Tags { get; set; }
        public int? ArticleId { get; set; }
    }
}