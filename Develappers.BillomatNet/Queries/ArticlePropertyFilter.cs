namespace Develappers.BillomatNet.Queries
{
    /// <summary>
    /// Represents the filter for the article property.
    /// </summary>
    public class ArticlePropertyFilter
    {
        public int? ArticleId { get; set; }
        public int? ArticlePropertyId { get; set; }
        public object Value { get; set; }
    }
}