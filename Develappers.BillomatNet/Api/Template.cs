// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;

namespace Develappers.BillomatNet.Api
{
    internal class Template
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("created")]
        public string Created { get; set; }
        [JsonPropertyName("updated")]
        public string Updated { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("template_type")]
        public string TemplateType { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("is_background_available")]
        public string IsBackgroundAvailable { get; set; }
        [JsonPropertyName("is_default")]
        public string IsDefault { get; set; }
    }
}
