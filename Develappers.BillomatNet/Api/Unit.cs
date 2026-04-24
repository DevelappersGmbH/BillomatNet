// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json.Serialization;

namespace Develappers.BillomatNet.Api
{
    internal class Unit
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("created")]
        public DateTime Created { get; set; }
        [JsonPropertyName("updated")]
        public DateTime Updated { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
