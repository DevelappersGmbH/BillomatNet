// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;

namespace Develappers.BillomatNet.Api
{
    internal class Invoice
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("created")]
        public string Created { get; set; }

        [JsonPropertyName("updated")]
        public string Updated { get; set; }

        [JsonPropertyName("contact_id")]
        public string ContactId { get; set; }

        [JsonPropertyName("client_id")]
        public string ClientId { get; set; }

        [JsonPropertyName("invoice_number")]
        public string InvoiceNumber { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("number_pre")]
        public string NumberPre { get; set; }

        [JsonPropertyName("number_length")]
        public string NumberLength { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("supply_date")]
        public string SupplyDate { get; set; }

        [JsonPropertyName("supply_date_type")]
        public string SupplyDateType { get; set; }

        [JsonPropertyName("due_date")]
        public string DueDate { get; set; }

        [JsonPropertyName("due_days")]
        public string DueDays { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("discount_rate")]
        public string DiscountRate { get; set; }

        [JsonPropertyName("discount_date")]
        public string DiscountDate { get; set; }

        [JsonPropertyName("discount_days")]
        public string DiscountDays { get; set; }

        [JsonPropertyName("discount_amount")]
        public string DiscountAmount { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("intro")]
        public string Intro { get; set; }

        [JsonPropertyName("note")]
        public string Note { get; set; }

        [JsonPropertyName("total_gross")]
        public string TotalGross { get; set; }

        [JsonPropertyName("total_net")]
        public string TotalNet { get; set; }

        [JsonPropertyName("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonPropertyName("quote")]
        public string Quote { get; set; }

        [JsonPropertyName("net_gross")]
        public string NetGross { get; set; }

        [JsonPropertyName("reduction")]
        public string Reduction { get; set; }

        [JsonPropertyName("total_gross_unreduced")]
        public string TotalGrossUnreduced { get; set; }

        [JsonPropertyName("total_net_unreduced")]
        public string TotalNetUnreduced { get; set; }

        [JsonPropertyName("paid_amount")]
        public string PaidAmount { get; set; }

        [JsonPropertyName("open_amount")]
        public string OpenAmount { get; set; }

        [JsonPropertyName("customerportal_url")]
        public string CustomerPortalUrl { get; set; }

        [JsonPropertyName("invoice_id")]
        public string InvoiceId { get; set; }

        [JsonPropertyName("offer_id")]
        public string OfferId { get; set; }

        [JsonPropertyName("confirmation_id")]
        public string ConfirmationId { get; set; }

        [JsonPropertyName("recurring_id")]
        public string RecurringId { get; set; }

        [JsonPropertyName("free_text_id")]
        public string FreeTextId { get; set; }

        [JsonPropertyName("template_id")]
        public string TemplateId { get; set; }

        [JsonPropertyName("payment_types")]
        public string PaymentTypes { get; set; }

        [JsonPropertyName("taxes")]
        public InvoiceTaxWrapper Taxes { get; set; }

        [JsonPropertyName("invoice_items")]
        public InvoiceItemsWrapper InvoiceItems { get; set; }
    }
}
