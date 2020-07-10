using Develappers.BillomatNet.Api.Json;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Develappers.BillomatNet.Api
{
    internal class InvoiceItemsWrapper
    {
        [JsonProperty("invoice_items")]
        [JsonConverter(typeof(CollectionConverter<InvoiceTax>))]
        public List<InvoiceItem> List { get; set; }
    }
}
