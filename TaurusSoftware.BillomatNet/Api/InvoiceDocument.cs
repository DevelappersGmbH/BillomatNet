using Newtonsoft.Json;

namespace TaurusSoftware.BillomatNet.Api
{
    internal class InvoiceDocument
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("invoice_id")]
        public string InvoiceId { get; set; }

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