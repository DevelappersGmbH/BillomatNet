using System;
using System.Collections.Generic;
using System.Text;

namespace Develappers.BillomatNet.Types
{
    public class Recipients
    {
        public List<string> To { get; set; }
        public List<string> Cc { get; set; }
        public List<string> Bc { get; set; }
    }
}
