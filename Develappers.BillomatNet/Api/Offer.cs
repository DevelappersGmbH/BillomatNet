// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Develappers.BillomatNet.Api
{
    internal class Offer
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

        [JsonPropertyName("offer_number")]
        public string OfferNumber { get; set; }

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

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

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

        [JsonPropertyName("customerportal_url")]
        public string CustomerPortalUrl { get; set; }

        [JsonPropertyName("template_id")]
        public string TemplateId { get; set; }

        [JsonPropertyName("taxes")]
        public InvoiceTaxWrapper Taxes { get; set; }
    }
}
