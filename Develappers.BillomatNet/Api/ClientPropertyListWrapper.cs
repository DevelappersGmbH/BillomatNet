using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Develappers.BillomatNet.Api
{
    internal class ClientPropertyListWrapper : PagedListWrapper<ClientPropertyList>
    {
        [JsonProperty("client-property-values")]
        public override ClientPropertyList Item { get; set; }
    }
}
