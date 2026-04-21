// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;

namespace Develappers.BillomatNet.Api
{
    internal class InvoiceTax
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("rate")]
        public string Rate { get; set; }

        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("amount_plain")]
        public string AmountPlain { get; set; }

        [JsonPropertyName("amount_rounded")]
        public string AmountRounded { get; set; }

        [JsonPropertyName("amount_net")]
        public string AmountNet { get; set; }

        [JsonPropertyName("amount_net_plain")]
        public string AmountNetPlain { get; set; }

        [JsonPropertyName("amount_net_rounded")]
        public string AmountNetRounded { get; set; }

        [JsonPropertyName("amount_gross")]
        public string AmountGross { get; set; }

        [JsonPropertyName("amount_gross_plain")]
        public string AmountGrossPlain { get; set; }

        [JsonPropertyName("amount_gross_rounded")]
        public string AmountGrossRounded { get; set; }
    }
}
