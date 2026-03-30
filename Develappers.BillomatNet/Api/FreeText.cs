// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    public class FreeText
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string FreeTextType { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("intro")]
        public string Intro { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("is_default")]
        public int IsDefault { get; set; }
    }
}
