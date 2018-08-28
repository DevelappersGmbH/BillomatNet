namespace TaurusSoftware.BillomatNet.Types
{
    public class InvoiceItem
    {
        public int Id  { get; set; }
        public int? ArticleId { get; set; }

        public int InvoiceId { get; set; }

        public int Position { get; set; }

        public string Unit { get; set; }

        public float Quantity { get; set; }

        public float UnitPrice { get; set; }

        public string TaxName { get; set; }

        public float? TaxRate { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public float TotalGross { get; set; }

        public float TotalNet { get; set; }

        public IReduction Reduction { get; set; }

        public float TotalGrossUnreduced { get; set; }

        public float TotalNetUnreduced { get; set; }
    }
}