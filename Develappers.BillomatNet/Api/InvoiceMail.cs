// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;

namespace Develappers.BillomatNet.Api
{
    internal class InvoiceMail
    {
        [JsonPropertyName("from")]
        public string From { get; set; }
        [JsonPropertyName("recipients")]
        public Recipients Recipients { get; set; }
        [JsonPropertyName("subject")]
        public string Subject { get; set; }
        [JsonPropertyName("body")]
        public string Body { get; set; }
        [JsonPropertyName("attachments")]
        public AttachmentsWrapper Attachments { get; set; }
    }
}
