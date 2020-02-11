using System.Collections.Generic;
using Develappers.BillomatNet.Api.Json;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class ContactList : PagedList<Contact>
    {
        [JsonProperty("contact")]
        [JsonConverter(typeof(CollectionConverter<Contact>))]
        public override List<Contact> List { get; set; }
    }
}