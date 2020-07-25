// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class ClientProperty
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("client_id")]
        public int ClientId { get; set; }
        [JsonProperty("client_property_id")]
        public int ClientPropertyId { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
