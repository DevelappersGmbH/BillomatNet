using System.Collections.Generic;
using Develappers.BillomatNet.Api.Json;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class UnitList : PagedList<Unit>
    {
        [JsonProperty("unit")]
        [JsonConverter(typeof(CollectionConverter<Unit>))]
        public override List<Unit> List { get; set; }
    }
}
