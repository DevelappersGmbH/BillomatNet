// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using Develappers.BillomatNet.Api;
using Account = Develappers.BillomatNet.Types.Account;

namespace Develappers.BillomatNet.Mapping
{
    internal class AccountMapper : IMapper<Api.Account, Account>
    {
        public Account ApiToDomain(Api.Account value)
        {
            if (value == null)
            {
                return null;
            }

            return new Account
            {
                Id = int.Parse(value.Id),
                Number = int.Parse(value.Number),
                CountryCode = value.CountryCode,
                Email = value.Email,
                FirstName = value.FirstName,
                LastName = value.LastName,
                Note = value.Note,
                Created = DateTime.Parse(value.Created, CultureInfo.InvariantCulture),
                Archived = value.Archived != "0",
                NumberPre = value.NumberPre,
                NumberLength = int.Parse(value.NumberLength),
                Address = value.Address,
                ClientNumber = value.ClientNumber,
                BankAccountNumber = value.BankAccountNumber,
                BankAccountOwner = value.BankAccountOwner,
                BankIban = value.BankIban,
                BankName = value.BankName,
                BankNumber = value.BankNumber,
                BankSwift = value.BankSwift,
                City = value.City,
                CurrencyCode = value.CurrencyCode,
                Fax = value.Fax,
                Mobile = value.Mobile,
                Name = value.Name,
                Phone = value.Phone,
                Salutation = value.Salutation,
                State = value.State,
                Street = value.Street,
                TaxNumber = value.TaxNumber,
                VatNumber = value.VatNumber,
                Www = value.Www,
                Zip = value.Zip,
                Plan = value.Plan,
                Quotas = new QuotaMapper().ApiToDomain(value.Quotas)
            };
        }

        public Api.Account DomainToApi(Account value)
        {
            throw new NotImplementedException();
        }

        public Account ApiToDomain(AccountWrapper value)
        {
            return ApiToDomain(value?.Client);
        }
    }
}
