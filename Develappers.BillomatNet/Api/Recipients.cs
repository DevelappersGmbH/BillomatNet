// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class Recipients
    {
        [JsonProperty("to")]
        public List<string> To { get; set; }
        [JsonProperty("cc")]
        public List<string> Cc { get; set; }
        [JsonProperty("bc")]
        public List<string> Bc { get; set; }
    }
}
