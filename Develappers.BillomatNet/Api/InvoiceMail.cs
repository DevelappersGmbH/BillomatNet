// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class InvoiceMail
    {
        [JsonProperty("from")]
        public string From { get; set; }
        [JsonProperty("recipients")]
        public Recipients Recipients { get; set; }
        [JsonProperty("subject")]
        public string Subject { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
        [JsonProperty("attachments")]
        public AttachmentsWrapper Attachments { get; set; }
    }
}
