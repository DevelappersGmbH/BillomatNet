using System.Collections.Generic;
using Develappers.BillomatNet.Api.Json;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class TaxList : PagedList<Tax>
    {
        [JsonProperty("tax")]
        [JsonConverter(typeof(CollectionConverter<Tax>))]
        public override List<Tax> List { get; set; }
    }
}