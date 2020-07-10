using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class Invoice
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("contact_id")]
        public string ContactId { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("invoice_number")]
        public string InvoiceNumber { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("number_pre")]
        public string NumberPre { get; set; }

        [JsonProperty("number_length")]
        public string NumberLength { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("supply_date")]
        public string SupplyDate { get; set; }

        [JsonProperty("supply_date_type")]
        public string SupplyDateType { get; set; }

        [JsonProperty("due_date")]
        public string DueDate { get; set; }

        [JsonProperty("due_days")]
        public string DueDays { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("discount_rate")]
        public string DiscountRate { get; set; }

        [JsonProperty("discount_date")]
        public string DiscountDate { get; set; }

        [JsonProperty("discount_days")]
        public string DiscountDays { get; set; }

        [JsonProperty("discount_amount")]
        public string DiscountAmount { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("intro")]
        public string Intro { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("total_gross")]
        public string TotalGross { get; set; }

        [JsonProperty("total_net")]
        public string TotalNet { get; set; }

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("quote")]
        public string Quote { get; set; }

        [JsonProperty("net_gross")]
        public string NetGross { get; set; }

        [JsonProperty("reduction")]
        public string Reduction { get; set; }

        [JsonProperty("total_gross_unreduced")]
        public string TotalGrossUnreduced { get; set; }

        [JsonProperty("total_net_unreduced")]
        public string TotalNetUnreduced { get; set; }

        [JsonProperty("paid_amount")]
        public string PaidAmount { get; set; }

        [JsonProperty("open_amount")]
        public string OpenAmount { get; set; }

        [JsonProperty("customerportal_url")]
        public string CustomerPortalUrl { get; set; }

        [JsonProperty("invoice_id")]
        public string InvoiceId { get; set; }

        [JsonProperty("offer_id")]
        public string OfferId { get; set; }

        [JsonProperty("confirmation_id")]
        public string ConfirmationId { get; set; }

        [JsonProperty("recurring_id")]
        public string RecurringId { get; set; }

        [JsonProperty("template_id")]
        public string TemplateId { get; set; }

        [JsonProperty("payment_types")]
        public string PaymentTypes { get; set; }

        [JsonProperty("taxes")]
        public InvoiceTaxWrapper Taxes { get; set; }
    }
}