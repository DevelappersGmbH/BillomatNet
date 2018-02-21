using System;
using Newtonsoft.Json;

namespace TaurusSoftware.BillomatNet.Api
{
    internal class Article
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("article_number")]
        public string ArticleNumber { get; set; }

        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("number_pre")]
        public string NumberPre { get; set; }

        [JsonProperty("number_length")]
        public string NumberLength { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("sales_price")]
        public float? SalesPrice { get; set; }

        [JsonProperty("sales_price2")]
        public float? SalesPrice2 { get; set; }

        [JsonProperty("sales_price3")]
        public float? SalesPrice3 { get; set; }

        [JsonProperty("sales_price4")]
        public float? SalesPrice4 { get; set; }

        [JsonProperty("sales_price5")]
        public float? SalesPrice5 { get; set; }

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("unit_id")]
        public int UnitId { get; set; }

        [JsonProperty("tax_id")]
        public int TaxId { get; set; }

        [JsonProperty("purchase_price")]
        public float? PurchasePrice { get; set; }

        [JsonProperty("purchase_price_net_gross")]
        public string PurchasePriceNetGross { get; set; }

        [JsonProperty("supplier_id")]
        public int? SupplierId { get; set; }
    }
}
