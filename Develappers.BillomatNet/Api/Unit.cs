// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Newtonsoft.Json;
using System;

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
