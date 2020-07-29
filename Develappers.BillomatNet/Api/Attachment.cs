using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class Attachment
    {
        [JsonProperty("filename")]
        public string FileName { get; set; }
        [JsonProperty("mimetype")]
        public string MimeType { get; set; }
        [JsonProperty("base64file")]
        public string Base64File { get; set; }
    }
}
