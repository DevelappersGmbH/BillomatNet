using System;
using System.Collections.Generic;

namespace TaurusSoftware.BillomatNet.Types
{
    public class Client
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public string ClientNumber { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CountryCode { get; set; }

        public string Note { get; set; }

        public List<int> InvoiceIds { get; set; }

        public List<string> Tags { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsArchived { get; set; }

        public string NumberPrefix { get; set; }

        public int NumberLength { get; set; }

        public string Name { get; set; }

        public string Salutation { get; set; }

        public string Street { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string Mobile { get; set; }

        public string Web { get; set; }

        public string TaxNumber { get; set; }

        public string VatNumber { get; set; }

        public string BankAccountOwner { get; set; }

        public string BankNumber { get; set; }

        public string BankName { get; set; }

        public string BankAccountNumber { get; set; }

        public string BankSwift { get; set; }

        public string BankIban { get; set; }

        public string CurrencyCode { get; set; }
    }
}
