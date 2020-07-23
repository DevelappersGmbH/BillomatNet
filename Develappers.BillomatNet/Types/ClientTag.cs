using System;
using System.Collections.Generic;
using System.Text;

namespace Develappers.BillomatNet.Types
{
    /// <summary>
    /// Represents a client tag.
    /// </summary>
    public class ClientTag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClientId { get; set; }
    }
}
