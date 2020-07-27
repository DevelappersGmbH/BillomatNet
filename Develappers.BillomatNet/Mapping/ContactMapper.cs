// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Linq;
using Develappers.BillomatNet.Api;
using Contact = Develappers.BillomatNet.Types.Contact;

namespace Develappers.BillomatNet.Mapping
{
    internal class ContactMapper : IMapper<Api.Contact, Contact>
    {
        public Contact ApiToDomain(Api.Contact value)
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

        public Api.Contact DomainToApi(Contact value)
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

        internal Types.PagedList<Contact> ApiToDomain(ContactList value)
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
                List = value.List?.Select(ApiToDomain).ToList()
            };
        }

        public Types.PagedList<Contact> ApiToDomain(ContactListWrapper value)
        {
            return ApiToDomain(value?.Item);
        }

        public Contact ApiToDomain(ContactWrapper value)
        {
            return ApiToDomain(value?.Contact);
        }
    }
}
