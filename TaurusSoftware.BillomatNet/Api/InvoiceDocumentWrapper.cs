using Newtonsoft.Json;

namespace TaurusSoftware.BillomatNet.Api
{
    internal class InvoiceDocumentWrapper
    {
        [JsonProperty("pdf")]
        public InvoiceDocument Pdf { get; set; }
    }
}