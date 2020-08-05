// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using System.Linq;
using Develappers.BillomatNet.Api;
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

            return new Client
            {
                Id = int.Parse(value.Id),
                Created = DateTime.Parse(value.Created, CultureInfo.InvariantCulture),
                Archived = value.Archived != "0",
                ClientNumber = value.ClientNumber,
                Number = int.Parse(value.Number),
                NumberPre = value.NumberPre,
                NumberLength = int.Parse(value.NumberLength),
                Name = value.Name,
                Salutation = value.Salutation,
                FirstName = value.FirstName,
                LastName = value.LastName,
                Street = value.Street,
                Zip = value.Zip,
                City = value.City,
                State = value.State,
                CountryCode = value.CountryCode,
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
                EnableCustomerportal = value.EnableCustomerportal != "0",
                CustomerportalUrl = value.CustomerportalUrl,
                SepaMandate = value.SepaMandate,
                SepaMandateDate = value.SepaMandateDate.ToOptionalDateTime(),
                TaxRule = value.TaxRule.ToTaxRuleType(),
                NetGross = value.NetGross.ToNetGrossSettingsType(),
                DefaultPaymentTypes = value.DefaultPaymentTypes.Split(',').ToList().Select(x => x.ToPaymentType()).ToList(),
                Reduction = value.Reduction.ToOptionalFloat(),
                DiscountRateType = value.DiscountRateType.ToDiscountType(),
                DiscountRate = value.DiscountRate.ToOptionalFloat(),
                DiscountDaysType = value.DiscountDaysType.ToDiscountType(),
                DicountDays = value.DicountDays.ToOptionalInt(),
                DueDaysType = value.DueDaysType.ToDiscountType(),
                DueDays = value.DueDays.ToOptionalInt(),
                ReminderDueDaysType = value.ReminderDueDaysType.ToDiscountType(),
                ReminderDueDays = value.ReminderDueDays.ToOptionalInt(),
                OfferValidityDaysType = value.OfferValidityDaysType.ToDiscountType(),
                OfferValidityDays = value.OfferValidityDays.ToOptionalInt(),
                CurrencyCode = value.CurrencyCode,
                PriceGroup = value.PriceGroup.ToOptionalInt(),
                DebitorAccountNumber = value.DebitorAccountNumber.ToOptionalInt(),
                DunningRun = value.DunningRun != "0",
                Note = value.Note,
                RevenueGross =value.RevenueGross.ToOptionalFloat(),
                RevenueNet = value.RevenueNet.ToOptionalFloat()
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
