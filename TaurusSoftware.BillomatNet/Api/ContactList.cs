using System.Collections.Generic;
using Newtonsoft.Json;
using TaurusSoftware.BillomatNet.Api.Json;

namespace TaurusSoftware.BillomatNet.Api
{
    internal class ContactList : PagedList<Contact>
    {
        [JsonProperty("contact")]
        [JsonConverter(typeof(CollectionConverter<Contact>))]
        public override List<Contact> List { get; set; }
    }
}