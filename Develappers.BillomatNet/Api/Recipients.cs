// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Develappers.BillomatNet.Api
{
    internal class Recipients
    {
        [JsonPropertyName("to")]
        public List<string> To { get; set; }
        [JsonPropertyName("cc")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string> Cc { get; set; }
        [JsonPropertyName("bcc")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string> Bcc { get; set; }
    }
}
