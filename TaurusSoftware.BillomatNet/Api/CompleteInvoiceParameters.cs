using Newtonsoft.Json;

namespace TaurusSoftware.BillomatNet.Api
{
    internal class CompleteInvoiceParameters
    {
        [JsonProperty("template_id")]
        public int? TemplateId { get; set; }
    }
}