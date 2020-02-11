namespace Develappers.BillomatNet.Types
{
    public class ArticleProperty
    {
        public int Id { get; set; }

        public int ArticleId { get; set; }

        public int ArticlePropertyId { get; set; }

        public PropertyType Type { get; set; }

        public string Name { get; set; }

        public object Value { get; set; }
    }
}