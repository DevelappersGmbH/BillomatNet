// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Types;
using Quota = Develappers.BillomatNet.Types.Quota;

namespace Develappers.BillomatNet.Mapping
{
    internal class QuotaMapper : IMapper<Api.Quota, Quota>
    {
        public List<Quota> ApiToDomain(QuotaWrapper value)
        {
            return value?.Quota?.Select(ApiToDomain).ToList();
        }

        public Quota ApiToDomain(Api.Quota value)
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
                Available = long.Parse(value.Available),
                Used = long.Parse(value.Used),
                Entity = type
            };
        }

        public Api.Quota DomainToApi(Quota value)
        {
            throw new NotImplementedException();
        }
    }
}
