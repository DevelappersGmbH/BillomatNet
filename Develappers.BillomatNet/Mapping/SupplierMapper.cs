// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Linq;
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

            var result = new Supplier
            {
                Id = value.Id.ToInt(),
                Created = value.Created.ToDateTime(),
                Updated = value.Updated.ToDateTime(),
                Name = value.Name,
                Salutation = value.Salutation,
                FirstName = value.FirstName,
                LastName = value.LastName,
                Street = value.Street,
                Zip = value.Zip,
                City = value.City,
                State = value.State,
                CountryCode = value.CountryCode,
                IsEuCountry = value.IsEuCountry.ToBoolean(),
                Address = value.Address,
                Phone = value.Phone,
                Fax = value.Fax,
                Mobile = value.Mobile,
                Email = value.Email,
                Www = value.Www,
                TaxNumber = value.TaxNumber,
                VatNumber = value.VatNumber,
                BankAccountOwner = value.BankAccountOwner,
                BankNumber = value.BankNumber,
                BankName = value.BankName,
                BankAccountNumber = value.BankAccountNumber,
                BankSwift = value.BankSwift,
                BankIban = value.BankIban,
                CurrencyCode = value.CurrencyCode,
                Locale = value.Locale,
                Note = value.Note,
                ClientNumber = value.ClientNumber,
                CreditorAccountNumber = value.CreditorAccountNumber,
                CreditorIdentifier = value.CreditorIdentifier,
                CostsGross = value.CostsGross.ToFloat(),
                CostsNet = value.CostsNet.ToFloat(),
                SupplierPropertyValues = value.SupplierPropertyValues.ToDomain()
            };
            return result;
        }

        public Types.PagedList<Supplier> ApiToDomain(SupplierListWrapper value)
        {
            return ApiToDomain(value?.Item);
        }

        public Types.PagedList<Supplier> ApiToDomain(SupplierList value)
        {
            if (value == null)
            {
                return null;
            }

            return new Types.PagedList<Supplier>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(ApiToDomain).ToList()
            };
        }
    }
}
