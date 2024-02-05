// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class Template
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("created")]
        public string Created { get; set; }
        [JsonProperty("updated")]
        public string Updated { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("template_type")]
        public string TemplateType { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("is_background_available")]
        public string IsBackgroundAvailable { get; set; }
        [JsonProperty("is_default")]
        public string IsDefault { get; set; }
    }
}
