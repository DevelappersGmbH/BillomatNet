using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class CompleteInvoiceWrapper
    {
        [JsonProperty("complete")]
        public CompleteInvoiceParameters Parameters { get; set; }
    }
}
