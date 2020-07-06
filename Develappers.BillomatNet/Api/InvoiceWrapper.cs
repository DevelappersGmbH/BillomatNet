using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class InvoiceWrapper
    {
        [JsonProperty("invoice")]
        public Invoice Invoice { get; set; }
    }
}