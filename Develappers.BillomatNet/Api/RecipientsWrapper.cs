using System;
using System.Collections.Generic;
using System.Text;
using Develappers.BillomatNet.Api.Json;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class RecipientsWrapper
    {
        [JsonProperty("attachments")]
        [JsonConverter(typeof(CollectionConverter<Attachment>))]
        public List<Recipients> List { get; set; }
    }
}
