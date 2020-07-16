using System;

namespace Develappers.BillomatNet.Types
{
    /// <summary>
    /// Represents a contact
    /// </summary>
    public class Contact
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public string Email { get; set; }

        public string Fax { get; set; }

        public string Label { get; set; }

        public string Mobile { get; set; }

        public string Web { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Street { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string CountryCode { get; set; }

        public string Salutation { get; set; }

        public string Phone { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
    }
}