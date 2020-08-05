// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

namespace Develappers.BillomatNet.Types
{
    /// <summary>
    /// Represents a client.
    /// </summary>
    public class Client
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public bool Archived { get; set; }
        public string ClientNumber { get; set; }
        public int Number { get; set; }
        public string NumberPre { get; set; }
        public int NumberLength { get; set; }
        public string Name { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CountryCode { get; set; }
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
        public bool EnableCustomerportal { get; set; }
        public string CustomerportalUrl { get; set; }
        public string SepaMandate { get; set; }
        public DateTime? SepaMandateDate { get; set; }
        public TaxRuleType TaxRule { get; set; }
        public NetGrossSettingsType NetGross { get; set; }
        public List<PaymentType> DefaultPaymentTypes { get; set; }
        public float? Reduction { get; set; }
        public DiscountType DiscountRateType { get; set; }
        public float? DiscountRate { get; set; }
        public DiscountType DiscountDaysType { get; set; }
        public int? DicountDays { get; set; }
        public DiscountType DueDaysType { get; set; }
        public int? DueDays { get; set; }
        public DiscountType ReminderDueDaysType { get; set; }
        public int? ReminderDueDays { get; set; }
        public DiscountType OfferValidityDaysType { get; set; }
        public int? OfferValidityDays { get; set; }
        public string CurrencyCode { get; set; }
        public int? PriceGroup { get; set; }
        public int? DebitorAccountNumber { get; set; }
        public bool DunningRun { get; set; }
        public string Note { get; set; }
        public float? RevenueGross { get; set; }
        public float? RevenueNet { get; set; }
    }
}
