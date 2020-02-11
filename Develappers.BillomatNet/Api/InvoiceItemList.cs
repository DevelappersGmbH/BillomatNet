using System.Collections.Generic;
using Develappers.BillomatNet.Api.Json;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class InvoiceItemList : PagedList<InvoiceItem>
    {
        [JsonProperty("invoice-item")]
        [JsonConverter(typeof(CollectionConverter<InvoiceItem>))]
        public override List<InvoiceItem> List { get; set; }
    }
}