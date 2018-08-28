using System.Collections.Generic;
using Newtonsoft.Json;
using TaurusSoftware.BillomatNet.Api.Json;

namespace TaurusSoftware.BillomatNet.Api
{
    internal class InvoiceTaxWrapper
    {
        [JsonProperty("tax")]
        [JsonConverter(typeof(CollectionConverter<InvoiceTax>))]
        public List<InvoiceTax> List { get; set; }
    }
}