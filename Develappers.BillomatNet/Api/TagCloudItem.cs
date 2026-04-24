// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;
using Develappers.BillomatNet.Api.Json;

namespace Develappers.BillomatNet.Api
{
    internal class TagCloudItem
    {
        [JsonPropertyName("id")]
        [JsonConverter(typeof(StringToIntConverter))]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("count")]
        [JsonConverter(typeof(StringToIntConverter))]
        public int Count { get; set; }
    }
}
