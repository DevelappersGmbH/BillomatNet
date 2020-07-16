using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class UnitWrapper
    {
        [JsonProperty("unit")]
        public Unit Unit { get; set; }
    }
}
