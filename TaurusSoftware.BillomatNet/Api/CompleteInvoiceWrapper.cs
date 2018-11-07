using Newtonsoft.Json;

namespace TaurusSoftware.BillomatNet.Api
{
    internal class CompleteInvoiceWrapper
    {
        [JsonProperty("complete")]
        public CompleteInvoiceParameters Parameters { get; set; }
    }
}
