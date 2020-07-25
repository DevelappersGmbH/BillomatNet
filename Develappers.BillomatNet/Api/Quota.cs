// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class Quota
    {
        [JsonProperty("entity")]
        public string Entity { get; set; }

        [JsonProperty("available")]
        public string Available { get; set; }

        [JsonProperty("used")]
        public string Used { get; set; }
    }
}
