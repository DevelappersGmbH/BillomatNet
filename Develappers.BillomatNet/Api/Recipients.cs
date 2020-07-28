// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class Recipients
    {
        [JsonProperty("to")]
        public string To { get; set; }
        [JsonProperty("cc")]
        public string Cc { get; set; }
        [JsonProperty("bc")]
        public string Bc { get; set; }
    }
}
