// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class ArticleProperty
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("article_id")]
        public int ArticleId { get; set; }

        [JsonProperty("article_property_id")]
        public int ArticlePropertyId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
