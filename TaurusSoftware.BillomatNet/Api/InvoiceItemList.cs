using System.Collections.Generic;
using Newtonsoft.Json;
using TaurusSoftware.BillomatNet.Api.Json;

namespace TaurusSoftware.BillomatNet.Api
{
    internal class InvoiceItemList : PagedList<InvoiceItem>
    {
        [JsonProperty("invoice-item")]
        [JsonConverter(typeof(CollectionConverter<InvoiceItem>))]
        public override List<InvoiceItem> List { get; set; }
    }
}