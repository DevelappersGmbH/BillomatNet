using System;
using System.Collections.Generic;
using System.Text;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Tests.UnitTests.Comparer
{
    public class SupplierEqualityComparer : IEqualityComparer<Supplier>
    {
        public bool Equals(Supplier x, Supplier y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return x.Id == y.Id &&
                x.Created == y.Created &&
                x.Updated == y.Updated &&
                x.Name == y.Name &&
                x.Salutation == y.Salutation &&
                x.FirstName == y.FirstName &&
                x.LastName == y.LastName &&
                x.Street == y.Street &&
                x.Zip == y.Zip &&
                x.City == y.City &&
                x.State == y.State &&
                x.CountryCode == y.CountryCode &&
                x.IsEuCountry == y.IsEuCountry &&
                x.Address == y.Address &&
                x.Phone == y.Phone &&
                x.Fax == y.Fax &&
                x.Mobile == y.Mobile &&
                x.Email == y.Email &&
                x.Www == y.Www &&
                x.TaxNumber == y.TaxNumber &&
                x.VatNumber == y.VatNumber &&
                x.BankAccountOwner == y.BankAccountOwner &&
                x.BankNumber == y.BankNumber &&
                x.BankName == y.BankName &&
                x.BankAccountNumber == y.BankAccountNumber &&
                x.BankSwift == y.BankSwift &&
                x.BankIban == y.BankIban &&
                x.CurrencyCode == y.CurrencyCode &&
                x.Locale == y.Locale &&
                x.Note == y.Note &&
                x.ClientNumber == y.ClientNumber &&
                x.CreditorAccountNumber == y.CreditorAccountNumber &&
                x.CreditorIdentifier == y.CreditorIdentifier &&
                x.CostsGross == y.CostsGross &&
                x.CostsNet == y.CostsNet;
                //TODO: PropertyValue typisiert #224
                //x.SupplierPropertyValues == y.SupplierPropertyValues;
        }

        public int GetHashCode(Supplier obj)
        {
            return obj.Id.GetHashCode() ^
                obj.Created.GetHashCode() ^
                obj.Updated.GetHashCode() ^
                obj.Name.GetHashCode() ^
                obj.Salutation.GetHashCode() ^
                obj.FirstName.GetHashCode() ^
                obj.LastName.GetHashCode() ^
                obj.Street.GetHashCode() ^
                obj.Zip.GetHashCode() ^
                obj.City.GetHashCode() ^
                obj.State.GetHashCode() ^
                obj.CountryCode.GetHashCode() ^
                obj.IsEuCountry.GetHashCode() ^
                obj.Address.GetHashCode() ^
                obj.Phone.GetHashCode() ^
                obj.Fax.GetHashCode() ^
                obj.Mobile.GetHashCode() ^
                obj.Email.GetHashCode() ^
                obj.Www.GetHashCode() ^
                obj.TaxNumber.GetHashCode() ^
                obj.VatNumber.GetHashCode() ^
                obj.BankAccountOwner.GetHashCode() ^
                obj.BankNumber.GetHashCode() ^
                obj.BankName.GetHashCode() ^
                obj.BankAccountNumber.GetHashCode() ^
                obj.BankSwift.GetHashCode() ^
                obj.BankIban.GetHashCode() ^
                obj.CurrencyCode.GetHashCode() ^
                obj.Locale.GetHashCode() ^
                obj.Note.GetHashCode() ^
                obj.ClientNumber.GetHashCode() ^
                obj.CreditorAccountNumber.GetHashCode() ^
                obj.CreditorIdentifier.GetHashCode() ^
                obj.CostsGross.GetHashCode() ^
                obj.CostsNet.GetHashCode() ^
                obj.SupplierPropertyValues.GetHashCode();
        }
    }
}
