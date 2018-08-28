namespace TaurusSoftware.BillomatNet.Types
{
    public class InvoiceTax
    {
        public string Name { get; set; }

        public float Rate { get; set; }

        public float Amount { get; set; }

        public float AmountPlain { get; set; }

        public float AmountRounded { get; set; }

        public float AmountNet { get; set; }

        public float AmountNetPlain { get; set; }

        public float AmountNetRounded { get; set; }

        public float AmountGross { get; set; }

        public float AmountGrossPlain { get; set; }

        public float AmountGrossRounded { get; set; }
    }
}