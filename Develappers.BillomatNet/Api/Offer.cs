// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class Offer
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("updated")]
        public string Updated { get; set; }

        [JsonProperty("contact_id")]
        public string ContactId { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("offer_number")]
        public string OfferNumber { get; set; }

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

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

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

        [JsonProperty("customerportal_url")]
        public string CustomerPortalUrl { get; set; }

        [JsonProperty("template_id")]
        public string TemplateId { get; set; }

        [JsonProperty("taxes")]
        public InvoiceTaxWrapper Taxes { get; set; }
    }
}
