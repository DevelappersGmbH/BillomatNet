using System;
using System.Collections.Generic;
using System.Linq;
using TaurusSoftware.BillomatNet.Api;
using TaurusSoftware.BillomatNet.Types;
using Account = TaurusSoftware.BillomatNet.Types.Account;
using Client = TaurusSoftware.BillomatNet.Types.Client;
using Quota = TaurusSoftware.BillomatNet.Types.Quota;
using Article = TaurusSoftware.BillomatNet.Types.Article;
using ArticleProperty = TaurusSoftware.BillomatNet.Types.ArticleProperty;
using ArticleTag = TaurusSoftware.BillomatNet.Types.ArticleTag;
using TagCloudItem = TaurusSoftware.BillomatNet.Types.TagCloudItem;

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

        internal static PagedList<ArticleProperty> ToDomain(this Api.ArticlePropertyListWrapper value)
        {
            if (value == null)
            {
                return null;
            }

            return value.Item.ToDomain();

        }

        internal static PagedList<TagCloudItem> ToDomain(this Api.ArticleTagCloudItemListWrapper value)
        {
            if (value == null)
            {
                return null;
            }

            return value.Item.ToDomain();

        }

        internal static PagedList<ArticleTag> ToDomain(this Api.ArticleTagListWrapper value)
        {
            if (value == null)
            {
                return null;
            }

            return value.Item.ToDomain();

        }

        internal static PagedList<ArticleProperty> ToDomain(this Api.ArticlePropertyList value)
        {
            if (value == null)
            {
                return null;
            }

            return new PagedList<ArticleProperty>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(x => x.ToDomain()).ToList()
            };
        }

        internal static PagedList<TagCloudItem> ToDomain(this Api.ArticleTagCloudItemList value)
        {
            if (value == null)
            {
                return null;
            }

            return new PagedList<TagCloudItem>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(x => x.ToDomain()).ToList()
            };
        }
        

        internal static PagedList<ArticleTag> ToDomain(this Api.ArticleTagList value)
        {
            if (value == null)
            {
                return null;
            }

            return new PagedList<ArticleTag>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(x => x.ToDomain()).ToList()
            };
        }

        private static PropertyType ParsePropertyType(string value)
        {
            switch (value)
            {
                case "CHECKBOX":
                    return PropertyType.Checkbox;
                case "TEXTAREA":
                    return PropertyType.Textarea;
                default:
                    return PropertyType.Textfield;
            }
        }

        private static object ParsePropertyValue(PropertyType type, string value)
        {
            if (type == PropertyType.Checkbox)
            {
                return value != "0";
            }

            return value;
        }

        private static ArticleProperty ToDomain(this Api.ArticleProperty value)
        {
            if (value == null)
            {
                return null;
            }

            PropertyType type = ParsePropertyType(value.Type);
            return new ArticleProperty
            {
                Id = value.Id,
                ArticlePropertyId = value.ArticlePropertyId,
                Type = type,
                ArticleId = value.ArticleId,
                Name = value.Name,
                Value = ParsePropertyValue(type, value.Value)
            };
        }

        private static ArticleTag ToDomain(this Api.ArticleTag value)
        {
            if (value == null)
            {
                return null;
            }

            return new ArticleTag
            {
                Id = value.Id,
                ArticleId = value.ArticleId,
                Name = value.Name
            };
        }

        private static TagCloudItem ToDomain(this Api.TagCloudItem value)
        {
            if (value == null)
            {
                return null;
            }

            return new TagCloudItem
            {
                Id = value.Id,
                Count = value.Count,
                Name = value.Name
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

        internal static ArticleProperty ToDomain(this ArticlePropertyWrapper value)
        {
            return value?.ArticleProperty.ToDomain();
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
