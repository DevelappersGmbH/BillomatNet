using System.Collections.Generic;
using Develappers.BillomatNet.Api.Json;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class AttachmentsWrapper
    {
        [JsonProperty("attachments")]
        [JsonConverter(typeof(CollectionConverter<Attachment>))]
        public List<Attachment> List { get; set; }
    }
}
