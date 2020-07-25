// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class ArticlePropertyWrapper
    {
        [JsonProperty("article-property-value")]
        public ArticleProperty ArticleProperty { get; set; }
    }
}
