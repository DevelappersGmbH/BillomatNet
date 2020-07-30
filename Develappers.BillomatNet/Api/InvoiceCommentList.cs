using System.Collections.Generic;
using Develappers.BillomatNet.Api.Json;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class InvoiceCommentList : PagedList<InvoiceComment>
    {
        [JsonProperty("invoice-comment")]
        [JsonConverter(typeof(CollectionConverter<InvoiceComment>))]
        public override List<InvoiceComment> List { get; set; }
    }
}
