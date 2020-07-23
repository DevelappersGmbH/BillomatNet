using System;
using System.Collections.Generic;

namespace Develappers.BillomatNet.Types
{
    /// <summary>
    /// Represents an invoice.
    /// </summary>
    public class Invoice
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string CustomerPortalUrl { get; set; }
        public int? InvoiceId { get; set; }
        public int? OfferId { get; set; }
        public int? ConfirmationId { get; set; }
        public int? RecurringId { get; set; }
        public int? TemplateId { get; set; }
        public DateTime Created { get; set; }
        public int? ContactId { get; set; }
        public string InvoiceNumber { get; set; }
        public int? Number { get; set; }
        public string NumberPre { get; set; }
        public int NumberLength { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public ISupplyDate SupplyDate { get; set; }
        public SupplyDateType? SupplyDateType { get; set; }
        public DateTime DueDate { get; set; }
        public int DueDays { get; set; }
        public string Address { get; set; }
        public InvoiceStatus Status { get; set; }
        public float DiscountRate { get; set; }
        public DateTime? DiscountDate { get; set; }
        public int? DiscountDays { get; set; }
        public float? DiscountAmount { get; set; }
        public string Label { get; set; }
        public string Intro { get; set; }
        public string Note { get; set; }
        public float TotalGross { get; set; }
        public float TotalNet { get; set; }
        public string CurrencyCode { get; set; }
        public float Quote { get; set; }
        public NetGrossType NetGross { get; set; }
        public IReduction Reduction { get; set; }
        public float TotalGrossUnreduced { get; set; }
        public float TotalNetUnreduced { get; set; }
        public List<string> PaymentTypes { get; set; }
        public List<InvoiceTax> Taxes { get; set; }
        public float PaidAmount { get; set; }
        public float OpenAmount { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }
    }
}