// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;

namespace Develappers.BillomatNet.Api
{
    internal class Tax
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("created")]
        public string Created { get; set; }
        [JsonPropertyName("updated")]
        public string Updated { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("rate")]
        public string Rate { get; set; }
        [JsonPropertyName("is_default")]
        public string IsDefault { get; set; }
    }
}
