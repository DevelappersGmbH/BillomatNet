using System.Collections.Generic;

namespace Develappers.BillomatNet.Queries
{
    public class ArticleFilter
    {
        public string Title { get; set; }
        public string ArticleNumber { get; set; }
        public string Description { get; set; }
        public string CurrencyCode { get; set; }
        public int? UnitId { get; set; }
        public int? SupplierId { get; set; }
        public List<string> Tags { get; set; }
    }
}