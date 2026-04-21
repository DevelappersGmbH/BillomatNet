// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;

namespace Develappers.BillomatNet.Api
{
    /// <summary>
    /// Represents an article.
    /// </summary>
    internal class Article
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("created")]
        public string Created { get; set; }

        [JsonPropertyName("updated")]
        public string Updated { get; set; }

        [JsonPropertyName("article_number")]
        public string ArticleNumber { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("number_pre")]
        public string NumberPre { get; set; }

        [JsonPropertyName("number_length")]
        public string NumberLength { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("sales_price")]
        public string SalesPrice { get; set; }

        [JsonPropertyName("sales_price2")]
        public string SalesPrice2 { get; set; }

        [JsonPropertyName("sales_price3")]
        public string SalesPrice3 { get; set; }

        [JsonPropertyName("sales_price4")]
        public string SalesPrice4 { get; set; }

        [JsonPropertyName("sales_price5")]
        public string SalesPrice5 { get; set; }

        [JsonPropertyName("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonPropertyName("unit_id")]
        public string UnitId { get; set; }

        [JsonPropertyName("tax_id")]
        public string TaxId { get; set; }

        [JsonPropertyName("purchase_price")]
        public string PurchasePrice { get; set; }

        [JsonPropertyName("purchase_price_net_gross")]
        public string PurchasePriceNetGross { get; set; }

        [JsonPropertyName("supplier_id")]
        public string SupplierId { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("cost_center")]
        public string CostCenter { get; set; }
    }
}
