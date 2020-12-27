// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class OfferDocument
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("offer_id")]
        public string OfferId { get; set; }

        [JsonProperty("filename")]
        public string FileName { get; set; }

        [JsonProperty("mimetype")]
        public string MimeType { get; set; }

        [JsonProperty("filesize")]
        public string FileSize { get; set; }

        [JsonProperty("base64file")]
        public string Base64File { get; set; }
    }
}
