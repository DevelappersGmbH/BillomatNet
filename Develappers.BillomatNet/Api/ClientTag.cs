using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Develappers.BillomatNet.Api
{
    internal class ClientTag
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("client_id")]
        public int ClientId { get; set; }
    }
}
