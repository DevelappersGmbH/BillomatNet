using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class SettingsWrapper
    {
        [JsonProperty("settings")]
        public Settings Settings { get; set; }
    }
}
