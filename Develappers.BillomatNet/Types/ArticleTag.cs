namespace Develappers.BillomatNet.Types
{
    /// <summary>
    /// Represents an article tag.
    /// </summary>
    public class ArticleTag
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ArticleId { get; set; }
    }
}
