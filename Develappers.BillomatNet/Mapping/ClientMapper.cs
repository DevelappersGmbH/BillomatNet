// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using System.Linq;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Types;
using Client = Develappers.BillomatNet.Types.Client;

namespace Develappers.BillomatNet.Mapping
{
    internal class ClientMapper : IMapper<Api.Client, Types.Client>
    {
        public Client ApiToDomain(Api.Client value)
        {
            if (value == null)
            {
                return null;
            }

            NetGrossSettingsType netGrossSettingsType;

            switch (value.NetGross.ToLowerInvariant())
            {
                case "net":
                    netGrossSettingsType = NetGrossSettingsType.Net;
                    break;
                case "gross":
                    netGrossSettingsType = NetGrossSettingsType.Gross;
                    break;
                case "settings":
                    netGrossSettingsType = NetGrossSettingsType.Settings;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new Client
            {
                Id = int.Parse(value.Id),
                Number = value.Number,
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
                NetGross = netGrossSettingsType
            };
        }

        public Api.Client DomainToApi(Client value)
        {
            throw new NotImplementedException();
        }

        public Client ApiToDomain(ClientWrapper value)
        {
            return ApiToDomain(value?.Client);
        }

        public Types.PagedList<Client> ApiToDomain(ClientList value)
        {
            if (value == null)
            {
                return null;
            }

            return new Types.PagedList<Client>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(ApiToDomain).ToList()
            };
        }

        public Types.PagedList<Client> ApiToDomain(ClientListWrapper value)
        {
            return ApiToDomain(value?.Item);
        }
    }
}
