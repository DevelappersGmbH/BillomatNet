using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class InvoiceWrapper // change to internal
    {
        [JsonProperty("invoice")]
        public Invoice Invoice { get; set; }
    }
}