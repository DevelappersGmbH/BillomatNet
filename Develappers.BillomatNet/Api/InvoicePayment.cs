// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class InvoicePayment
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("created")]
        public string Created { get; set; }
        [JsonProperty("invoice_id")]
        public string InvoiceId { get; set; }
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("amount")]
        public string Amount { get; set; }
        [JsonProperty("comment")]
        public string Comment { get; set; }
        [JsonProperty("transaction_purpose")]
        public string TransactionPurpose { get; set; }
        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }
        [JsonProperty("quote")]
        public string Quote { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("mark_invoice_as_paid")]
        public string MarkInvoiceAsPaid { get; set; }
    }
}
