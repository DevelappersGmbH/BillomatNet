using System;

namespace TaurusSoftware.BillomatNet.Types
{
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

        public DateTime? SupplyDate { get; set; }

        public string SupplyDateText { get; set; }

        public SupplyDateType SupplyDateType { get; set; }

        public DateTime DueDate { get; set; }

        public int DueDays { get; set; }

        public string Address { get; set; }

        //[JsonProperty("status")]
        //public string Status { get; set; }

        //[JsonProperty("discount_rate")]
        //public string DiscountRate { get; set; }

        //[JsonProperty("discount_date")]
        //public string DiscountDate { get; set; }

        //[JsonProperty("discount_days")]
        //public string DiscountDays { get; set; }

        //[JsonProperty("discount_amount")]
        //public string DiscountAmount { get; set; }

        public string Label { get; set; }

        public string Intro { get; set; }

        public string Note { get; set; }

        public float TotalGross { get; set; }

        public float TotalNet { get; set; }

        public string CurrencyCode { get; set; }

        //[JsonProperty("quote")]
        //public string Quote { get; set; }

        public NetGrossType NetGross { get; set; }

        //[JsonProperty("reduction")]
        //public string Reduction { get; set; }
       
        public float TotalGrossUnreduced { get; set; }

        public float TotalNetUnreduced { get; set; }

        //[JsonProperty("paid_amount")]
        //public string PaidAmount { get; set; }

        //[JsonProperty("open_amount")]
        //public string OpenAmount { get; set; }
    }
}