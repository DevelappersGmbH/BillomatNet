using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class TaxWrapper
    {
        [JsonProperty("tax")]
        public Tax Tax { get; set; }
    }
}