// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class ArticleListWrapper : PagedListWrapper<ArticleList>
    {
        [JsonProperty("articles")]
        public override ArticleList Item { get; set; }
    }
}
