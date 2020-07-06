using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class InvoiceTax // change to internal
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("rate")]
        public string Rate { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("amount_plain")]
        public string AmountPlain { get; set; }

        [JsonProperty("amount_rounded")]
        public string AmountRounded { get; set; }

        [JsonProperty("amount_net")]
        public string AmountNet { get; set; }

        [JsonProperty("amount_net_plain")]
        public string AmountNetPlain { get; set; }

        [JsonProperty("amount_net_rounded")]
        public string AmountNetRounded { get; set; }

        [JsonProperty("amount_gross")]
        public string AmountGross { get; set; }

        [JsonProperty("amount_gross_plain")]
        public string AmountGrossPlain { get; set; }

        [JsonProperty("amount_gross_rounded")]
        public string AmountGrossRounded { get; set; }
    }
}