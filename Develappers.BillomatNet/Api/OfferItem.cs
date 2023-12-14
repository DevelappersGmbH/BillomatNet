// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class OfferItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("article_id")]
        public string ArticleId { get; set; }

        [JsonProperty("offer_id")]
        public string OfferId { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("quantity")]
        public string Quantity { get; set; }

        [JsonProperty("unit_price")]
        public string UnitPrice { get; set; }

        [JsonProperty("tax_name")]
        public string TaxName { get; set; }

        [JsonProperty("tax_rate")]
        public string TaxRate { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("total_gross")]
        public string TotalGross { get; set; }

        [JsonProperty("total_net")]
        public string TotalNet { get; set; }

        [JsonProperty("reduction")]
        public string Reduction { get; set; }

        [JsonProperty("total_gross_unreduced")]
        public string TotalGrossUnreduced { get; set; }

        [JsonProperty("total_net_unreduced")]
        public string TotalNetUnreduced { get; set; }
        [JsonProperty("type")]
        public string OfferItemType { get; set; }
    }
}
