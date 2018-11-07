using Newtonsoft.Json;

namespace TaurusSoftware.BillomatNet.Api
{
    internal class ContactListWrapper : PagedListWrapper<ContactList>
    {
        [JsonProperty("contacts")]
        public override ContactList Item { get; set; }
    }
}