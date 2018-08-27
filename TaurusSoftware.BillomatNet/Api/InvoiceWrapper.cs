using Newtonsoft.Json;

namespace TaurusSoftware.BillomatNet.Api
{
    internal class InvoiceWrapper
    {
        [JsonProperty("invoice")]
        public Invoice Invoice { get; set; }
    }
}