// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Develappers.BillomatNet.Api
{
    internal class Supplier
    {
        public string Id { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }
        public string Name { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CountryCode { get; set; }
        public string IsEuCountry { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Www { get; set; }
        public string TaxNumber { get; set; }
        public string VatNumber { get; set; }
        public string BankAccountOwner { get; set; }
        public string BankNumber { get; set; }
        public string BankName { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankSwift { get; set; }
        public string BankIban { get; set; }
        public string CurrencyCode { get; set; }
        public string Locale { get; set; }
        public string Note { get; set; }
        public string ClientNumber { get; set; }
        public string CreditorAccountNumber { get; set; }
        public string CreditorIdentifier { get; set; }
        public string CostsGross { get; set; }
        public string CostsNet { get; set; }
        public SupplierPropertyValuesWrapper SupplierPropertyValues { get; set; }
    }
}
