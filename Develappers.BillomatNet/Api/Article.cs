// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    /// <summary>
    /// Represents an article.
    /// </summary>
    internal class Article
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("updated")]
        public string Updated { get; set; }

        [JsonProperty("article_number")]
        public string ArticleNumber { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("number_pre")]
        public string NumberPre { get; set; }

        [JsonProperty("number_length")]
        public string NumberLength { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("sales_price")]
        public string SalesPrice { get; set; }

        [JsonProperty("sales_price2")]
        public string SalesPrice2 { get; set; }

        [JsonProperty("sales_price3")]
        public string SalesPrice3 { get; set; }

        [JsonProperty("sales_price4")]
        public string SalesPrice4 { get; set; }

        [JsonProperty("sales_price5")]
        public string SalesPrice5 { get; set; }

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("unit_id")]
        public string UnitId { get; set; }

        [JsonProperty("tax_id")]
        public string TaxId { get; set; }

        [JsonProperty("purchase_price")]
        public string PurchasePrice { get; set; }

        [JsonProperty("purchase_price_net_gross")]
        public string PurchasePriceNetGross { get; set; }

        [JsonProperty("supplier_id")]
        public string SupplierId { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("cost_center")]
        public string CostCenter { get; set; }
    }
}
