using System;
using System.Collections.Generic;
using System.Text;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Queries
{
    /// <summary>
    /// Represents the filter for the invoice payment.
    /// </summary>
    public class InvoicePaymentFilter
    {
        public int? InvoiceId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public List<PaymentType> Type { get; set; }
        public int? UserId { get; set; }
    }
}
