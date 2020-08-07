// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using Develappers.BillomatNet.Api;
using Supplier = Develappers.BillomatNet.Types.Supplier;

namespace Develappers.BillomatNet.Mapping
{
    internal class SupplierMapper
    {
        public Supplier ApiToDomain(SupplierWrapper value)
        {
            return ApiToDomain(value?.Supplier);
        }
        public Supplier ApiToDomain(Api.Supplier value)
        {
            if (value == null)
            {
                return null;
            }
            var s = new Supplier();
            s.Id = int.Parse(value.Id);
            s.Created = DateTime.Parse(value.Created, CultureInfo.InvariantCulture);
            s.Updated = DateTime.Parse(value.Updated, CultureInfo.InvariantCulture);
            s.Name = value.Name;
            s.Salutation = value.Salutation;
            s.FirstName = value.FirstName;
            s.LastName = value.LastName;
            s.Street = value.Street;
            s.Zip = value.Zip;
            s.City = value.City;
            s.State = value.State;
            s.CountryCode = value.CountryCode;
            s.IsEuCountry = value.IsEuCountry != "0";
            s.Address = value.Address;
            s.Phone = value.Phone;
            s.Fax = value.Fax;
            s.Mobile = value.Mobile;
            s.Email = value.Email;
            s.Www = value.Www;
            s.TaxNumber = value.TaxNumber;
            s.VatNumber = value.VatNumber;
            s.BankAccountOwner = value.BankAccountOwner;
            s.BankNumber = value.BankNumber;
            s.BankName = value.BankName;
            s.BankAccountNumber = value.BankAccountNumber;
            s.BankSwift = value.BankSwift;
            s.BankIban = value.BankIban;
            s.CurrencyCode = value.CurrencyCode;
            s.Locale = value.Locale;
            s.Note = value.Note;
            s.ClientNumber = value.ClientNumber;
            s.CreditorAccountNumber = value.CreditorAccountNumber;
            s.CreditorIdentifier = value.CreditorIdentifier;
            s.CostsGross = float.Parse(value.CostsGross, CultureInfo.InvariantCulture);
            s.CostsNet = float.Parse(value.CostsNet, CultureInfo.InvariantCulture);
            s.SupplierPropertyValues = value.SupplierPropertyValues.ToDomain();
            return s;

            //return new Supplier
            //{
            //    Id = int.Parse(value.Id),
            //    Created = DateTime.Parse(value.Created, CultureInfo.InvariantCulture),
            //    Updated = DateTime.Parse(value.Updated, CultureInfo.InvariantCulture),
            //    Name = value.Name,
            //    Salutation = value.Salutation,
            //    FirstName = value.FirstName,
            //    LastName = value.LastName,
            //    Street = value.Street,
            //    Zip = value.Zip,
            //    City = value.City,
            //    State = value.State,
            //    CountryCode = value.CountryCode,
            //    IsEuCountry = value.IsEuCountry != "0",
            //    Address = value.Address,
            //    Phone = value.Phone,
            //    Fax = value.Fax,
            //    Mobile = value.Mobile,
            //    Email = value.Email,
            //    Www = value.Www,
            //    TaxNumber = value.TaxNumber,
            //    VatNumber = value.VatNumber,
            //    BankAccountOwner = value.BankAccountOwner,
            //    BankNumber = value.BankNumber,
            //    BankName = value.BankName,
            //    BankAccountNumber = value.BankAccountNumber,
            //    BankSwift = value.BankSwift,
            //    BankIban = value.BankIban,
            //    CurrencyCode = value.CurrencyCode,
            //    Locale = value.Locale,
            //    Note = value.Note,
            //    ClientNumber = value.ClientNumber,
            //    CreditorAccountNumber = value.CreditorAccountNumber,
            //    CreditorIdentifier = value.CreditorIdentifier,
            //    CostsGross = float.Parse(value.CostsGross),
            //    CostsNet = float.Parse(value.CostsNet),
            //    SupplierPropertyValues = value.SupplierPropertyValues.ToDomain()
            //};
        }
    }
}
