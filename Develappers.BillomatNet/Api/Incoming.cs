// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class Incoming
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("updated")]
        public string Updated { get; set; }

        [JsonProperty("supplier_id")]
        public string SupplierId { get; set; }

        [JsonProperty("client_number")]
        public string ClientNumber { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("due_date")]
        public string DueDate { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

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

        [JsonProperty("paid_amount")]
        public string PaidAmount { get; set; }

        [JsonProperty("open_amount")]
        public string OpenAmount { get; set; }

        [JsonProperty("expense_account_number")]
        public string ExpenseAccountNumber { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("page_count")]
        public string PageCount { get; set; }
    }
}
