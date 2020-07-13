using System;

namespace Develappers.BillomatNet.Types
{
    /// <summary>
    /// Represents the unit.
    /// </summary>
    public class Unit
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Name { get; set; }
    }
}
