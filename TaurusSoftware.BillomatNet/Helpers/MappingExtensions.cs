using System;
using System.Collections.Generic;
using System.Linq;
using TaurusSoftware.BillomatNet.Api;
using TaurusSoftware.BillomatNet.Types;
using Account = TaurusSoftware.BillomatNet.Types.Account;
using Client = TaurusSoftware.BillomatNet.Types.Client;
using Quota = TaurusSoftware.BillomatNet.Types.Quota;
using Article = TaurusSoftware.BillomatNet.Types.Article;

namespace TaurusSoftware.BillomatNet.Helpers
{
    internal static class MappingExtensions
    {
        internal static PagedList<Client> ToDomain(this Api.ClientListWrapper value)
        {
            if (value == null)
            {
                return null;
            }

            return value.Item.ToDomain();

        }

        internal static PagedList<Client> ToDomain(this Api.ClientList value)
        {
            if (value == null)
            {
                return null;
            }

            return new PagedList<Client>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(x => x.ToDomain()).ToList()
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

        internal static Client ToDomain(this ClientWrapper value)
        {
            return value?.Client.ToDomain();
        }

        private static Client ToDomain(this Api.Client value)
        {
            if (value == null)
            {
                return null;
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
                ZipCode = value.Zip
            };
        }

        internal static PagedList<Article> ToDomain(this Api.ArticleListWrapper value)
        {
            if (value == null)
            {
                return null;
            }

            return value.Item.ToDomain();

        }

        internal static PagedList<Article> ToDomain(this Api.ArticleList value)
        {
            if (value == null)
            {
                return null;
            }

            return new PagedList<Article>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(x => x.ToDomain()).ToList()
            };
        }

        internal static Article ToDomain(this ArticleWrapper value)
        {
            return value?.Article.ToDomain();
        }

        private static Article ToDomain(this Api.Article value)
        {
            if (value == null)
            {
                return null;
            }

            return new Article
            {
                Id = value.Id,
                Created = value.Created,
                ArticleNumber = value.ArticleNumber,
                CurrencyCode = value.CurrencyCode,
                Description = value.Description,
                Number = value.Number,
                NumberLength = value.NumberLength,
                NumberPre = value.NumberPre,
                PurchasePrice = value.PurchasePrice,
                PurchasePriceNetGross = value.PurchasePriceNetGross,
                SalesPrice = value.SalesPrice,
                SalesPrice2 = value.SalesPrice2,
                SalesPrice3 = value.SalesPrice3,
                SalesPrice4 = value.SalesPrice4,
                SalesPrice5 = value.SalesPrice5,
                SupplierId = value.SupplierId,
                TaxId = value.TaxId,
                Title = value.Title,
                UnitId = value.UnitId

            };
        }

        private static Account ToDomain(this Api.Account value)
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
    }
}
