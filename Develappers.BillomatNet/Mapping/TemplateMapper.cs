// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Types;
using Template = Develappers.BillomatNet.Types.Template;

namespace Develappers.BillomatNet.Mapping
{
    internal class TemplateMapper
    {
        public Template ApiToDomain(TemplateWrapper value)
        {
            return ApiToDomain(value?.Template);
        }

        public Template ApiToDomain(Api.Template value)
        {
            if (value == null)
            {
                return null;
            }

            var result = new Template
            {
                Id = value.Id.ToInt(),
                Created = value.Created.ToDateTime(),
                Updated = value.Updated.ToDateTime(),
                Name = value.Name,
                Type = value.Type,
                TemplateType = value.TemplateType,
                IsBackgroundAvailable = value.IsBackgroundAvailable.ToBoolean(),
                IsDefault = value.IsDefault.ToBoolean(),
            };
            return result;
        }

        public Types.PagedList<Template> ApiToDomain(TemplateListWrapper value)
        {
            return ApiToDomain(value?.Item);
        }

        public Types.PagedList<Template> ApiToDomain(TemplateList value)
        {
            if (value == null)
            {
                return null;
            }

            return new Types.PagedList<Template>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(ApiToDomain).ToList()
            };
        }
    }
}
