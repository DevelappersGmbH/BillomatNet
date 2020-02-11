using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class InvoiceDocumentWrapper
    {
        [JsonProperty("pdf")]
        public InvoiceDocument Pdf { get; set; }
    }
}