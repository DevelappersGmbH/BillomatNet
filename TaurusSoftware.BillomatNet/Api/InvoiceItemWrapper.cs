using Newtonsoft.Json;

namespace TaurusSoftware.BillomatNet.Api
{
    internal class InvoiceItemWrapper
    {
        [JsonProperty("invoice-item")]
        public InvoiceItem InvoiceItem { get; set; }
    }
}