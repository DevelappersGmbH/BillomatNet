using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Types;
using Account = Develappers.BillomatNet.Types.Account;
using Client = Develappers.BillomatNet.Types.Client;
using ClientProperty = Develappers.BillomatNet.Types.ClientProperty;
using Contact = Develappers.BillomatNet.Types.Contact;
using Quota = Develappers.BillomatNet.Types.Quota;
using TagCloudItem = Develappers.BillomatNet.Types.TagCloudItem;

namespace Develappers.BillomatNet.Helpers
{
    internal static class ClientMappingExtensions
    {
        private static Account ToDomain(this Develappers.BillomatNet.Api.Account value)
        {
            if (value == null)
            {
                return null;
            }

            return new Account
            {
                Id = int.Parse(value.Id),
                Number = value.Number,
                CountryCode = value.CountryCode,
                Email = value.Email,
                FirstName = value.FirstName,
                LastName = value.LastName,
                Note = value.Note,
                Tags = value.Tags.ToStringList(),
                InvoiceIds = value.InvoiceId.ToIntList(),
                CreatedAt = value.Created,
                IsArchived = value.Archived != "0",
                NumberPrefix = value.NumberPre,
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
                Web = value.Www,
                ZipCode = value.Zip,
                Plan = value.Plan,
                Quotas = value.Quotas.ToDomain()
            };
        }

        private static Quota ToDomain(this Api.Quota value)
        {
            if (value == null)
            {
                return null;
            }

            QuotaType type;
            switch (value.Entity.ToLowerInvariant())
            {
                case "documents":
                    type = QuotaType.Documents;
                    break;
                case "clients":
                    type = QuotaType.Clients;
                    break;
                case "articles":
                    type = QuotaType.Articles;
                    break;
                case "storage":
                    type = QuotaType.Storage;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();

            }

            return new Quota
            {
                Available = int.Parse(value.Available),
                Used = int.Parse(value.Used),
                Entity = type
            };
        }

        private static List<Quota> ToDomain(this QuotaWrapper value)
        {
            return value?.Quota?.Select(x => x.ToDomain()).ToList();
        }

        internal static Account ToDomain(this AccountWrapper value)
        {
            return value?.Client.ToDomain();
        }

        internal static Types.PagedList<Client> ToDomain(this ClientListWrapper value)
        {
            return value?.Item.ToDomain();
        }

        internal static Types.PagedList<Contact> ToDomain(this ContactListWrapper value)
        {
            return value?.Item.ToDomain();
        }

        internal static Types.PagedList<Client> ToDomain(this ClientList value)
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
                List = value.List?.Select(x => ToDomain((Api.Client) x)).ToList()
            };
        }

        internal static Types.PagedList<Contact> ToDomain(this ContactList value)
        {
            if (value == null)
            {
                return null;
            }

            return new Types.PagedList<Contact>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(ToDomain).ToList()
            };
        }

        internal static Client ToDomain(this ClientWrapper value)
        {
            return value?.Client.ToDomain();
        }

        internal static Types.PagedList<ClientProperty> ToDomain(this ClientPropertyListWrapper value)
        {
            return value?.Item.ToDomain();
        }

        private static ClientProperty ToDomain(this Api.ClientProperty value)
        {
            if (value == null)
            {
                return null;
            }

            var type = MappingHelpers.ParsePropertyType(value.Type);
            return new ClientProperty
            {
                Id = value.Id,
                ClientPropertyId = value.ClientPropertyId,
                Type = type,
                ClientId = value.ClientId,
                Name = value.Name,
                Value = MappingHelpers.ParsePropertyValue(type, value.Value)
            };
        }

        internal static Types.PagedList<ClientProperty> ToDomain(this ClientPropertyList value)
        {
            if (value == null)
            {
                return null;
            }

            return new Types.PagedList<ClientProperty>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(x => x.ToDomain()).ToList()
            };
        }

        internal static Contact ToDomain(this ContactWrapper value)
        {
            return value?.Contact.ToDomain();
        }

        private static Client ToDomain(this Api.Client value)
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
                Tags = value.Tags.ToStringList(),
                InvoiceIds = value.InvoiceId.ToIntList(),
                CreatedAt = value.Created,
                IsArchived = value.Archived != "0",
                NumberPrefix = value.NumberPre,
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
                Web = value.Www,
                ZipCode = value.Zip,
                NetGross = netGrossSettingsType
            };
        }

        private static Contact ToDomain(this Api.Contact value)
        {
            if (value == null)
            {
                return null;
            }

            return new Contact
            {
                Id = int.Parse(value.Id),
                ClientId = int.Parse(value.ClientId),
                City = value.City,
                CountryCode = value.CountryCode,
                Email = value.Email,
                Fax = value.Fax,
                FirstName = value.FirstName,
                Label = value.Label,
                LastName = value.LastName,
                Mobile = value.Mobile,
                Phone = value.Phone,
                Salutation = value.Salutation,
                State = value.State,
                Street = value.Street,
                Web = value.Www,
                ZipCode = value.ZipCode,
                Created = value.Created,
                Updated = value.Updated
            };
        }


        internal static Types.PagedList<TagCloudItem> ToDomain(this ClientTagCloudItemListWrapper value)
        {
            return value?.Item.ToDomain();
        }

        internal static Types.PagedList<TagCloudItem> ToDomain(this ClientTagCloudItemList value)
        {
            if (value == null)
            {
                return null;
            }

            return new Types.PagedList<TagCloudItem>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(x => x.ToDomain()).ToList()
            };
        }

        internal static Api.Contact ToApi(this Contact value)
        {
            if (value == null)
            {
                return null;
            }

            return new Api.Contact
            {
                Id = value.Id.ToString(),
                ClientId = value.ClientId.ToString(),
                Email = value.Email,
                Fax = value.Fax,
                Label = value.Label,
                Mobile = value.Mobile,
                Www = value.Web,
                FirstName = value.FirstName,
                LastName = value.LastName,
                Street = value.Street,
                ZipCode = value.ZipCode,
                City = value.City,
                State = value.State,
                CountryCode = value.CountryCode,
                Salutation = value.Salutation,
                Phone = value.Phone,
                Created = value.Created,
                Updated = value.Updated
            };
        }
    }
}