using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class Attachment
    {
        [JsonProperty("filename")]
        public string Filename { get; set; }
        [JsonProperty("mimetype")]
        public string Mimetype { get; set; }
        [JsonProperty("base64file")]
        public string Base64File { get; set; }
    }
}
