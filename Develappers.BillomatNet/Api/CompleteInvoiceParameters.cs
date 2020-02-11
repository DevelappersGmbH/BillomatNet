using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class CompleteInvoiceParameters
    {
        [JsonProperty("template_id")]
        public int? TemplateId { get; set; }
    }
}