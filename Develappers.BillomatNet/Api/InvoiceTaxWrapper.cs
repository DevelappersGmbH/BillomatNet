using System.Collections.Generic;
using Develappers.BillomatNet.Api.Json;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class InvoiceTaxWrapper
    {
        [JsonProperty("tax")]
        [JsonConverter(typeof(CollectionConverter<InvoiceTax>))]
        public List<InvoiceTax> List { get; set; }
    }
}