// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;

namespace Develappers.BillomatNet.Api
{
    internal class InboxDocument
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("created")]
        public string Created { get; set; }

        [JsonPropertyName("updated")]
        public string Updated { get; set; }

        [JsonPropertyName("filename")]
        public string FileName { get; set; }

        [JsonPropertyName("mimetype")]
        public string MimeType { get; set; }

        [JsonPropertyName("filesize")]
        public string FileSize { get; set; }

        [JsonPropertyName("base64file")]
        public string Base64File { get; set; }

        [JsonPropertyName("page_count")]
        public string PageCount { get; set; }

        [JsonPropertyName("document_type")]
        public string DocumentType { get; set; }

        [JsonPropertyName("metadata")]
        public InboxDocumentMetadata Metadata { get; set; }
    }
}
