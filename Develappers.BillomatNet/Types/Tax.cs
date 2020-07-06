using System;
namespace Develappers.BillomatNet.Types
{
    public class Tax
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Name { get; set; }
        public float Rate { get; set; }
        public bool IsDefault { get; set; }
    }
}