using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    public class InvoiceComment
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("created")]
        public string Created { get; set; }
        [JsonProperty("comment")]
        public string Comment { get; set; }
        [JsonProperty("actionkey")]
        public string ActionKey { get; set; }
        [JsonProperty("public")]
        public string Public { get; set; }
        [JsonProperty("by_client")]
        public string ByClient { get; set; }
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("email_id")]
        public string EmailId { get; set; }
        [JsonProperty("client_id")]
        public string ClientId { get; set; }
        [JsonProperty("invoice_id")]
        public string InvoiceId { get; set; }
    }
}
