using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class InvoiceItemWrapper
    {
        [JsonProperty("invoice-item")]
        public InvoiceItem InvoiceItem { get; set; }
    }
}