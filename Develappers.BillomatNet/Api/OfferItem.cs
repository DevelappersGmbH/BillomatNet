// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;

namespace Develappers.BillomatNet.Api
{
    internal class OfferItem
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("article_id")]
        public string ArticleId { get; set; }

        [JsonPropertyName("offer_id")]
        public string OfferId { get; set; }

        [JsonPropertyName("position")]
        public string Position { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }

        [JsonPropertyName("quantity")]
        public string Quantity { get; set; }

        [JsonPropertyName("unit_price")]
        public string UnitPrice { get; set; }

        [JsonPropertyName("tax_name")]
        public string TaxName { get; set; }

        [JsonPropertyName("tax_rate")]
        public string TaxRate { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("total_gross")]
        public string TotalGross { get; set; }

        [JsonPropertyName("total_net")]
        public string TotalNet { get; set; }

        [JsonPropertyName("reduction")]
        public string Reduction { get; set; }

        [JsonPropertyName("total_gross_unreduced")]
        public string TotalGrossUnreduced { get; set; }

        [JsonPropertyName("total_net_unreduced")]
        public string TotalNetUnreduced { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
