using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Develappers.BillomatNet.Api
{
    internal class Unit
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("created")]
        public DateTime Created { get; set; }
        [JsonProperty("updated")]
        public DateTime Updated { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
