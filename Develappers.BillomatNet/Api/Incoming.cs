// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;

namespace Develappers.BillomatNet.Api
{
    internal class Incoming
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("created")]
        public string Created { get; set; }

        [JsonPropertyName("updated")]
        public string Updated { get; set; }

        [JsonPropertyName("supplier_id")]
        public string SupplierId { get; set; }

        [JsonPropertyName("client_number")]
        public string ClientNumber { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("due_date")]
        public string DueDate { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }

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

        [JsonPropertyName("paid_amount")]
        public string PaidAmount { get; set; }

        [JsonPropertyName("open_amount")]
        public string OpenAmount { get; set; }

        [JsonPropertyName("expense_account_number")]
        public string ExpenseAccountNumber { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("page_count")]
        public string PageCount { get; set; }
    }
}
