// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;

namespace Develappers.BillomatNet
{
    public class BillomatUrlBuilder
    {
        private readonly Configuration _config;

        public BillomatUrlBuilder(Configuration config)
        {
            _config = config;
        }

        public string GetClientUrl(int id)
        {
            return GetEntityUrl("clients", id);
        }

        public string GetInvoiceUrl(int id)
        {
            return GetEntityUrl("invoices", id);
        }

        public string GetOfferUrl(int id)
        {
            return GetEntityUrl("offers", id);
        }

        private string GetEntityUrl(string type, int id)
        {
            if (id <= 0)
                return null;
            return $"https://{_config.BillomatId}.billomat.net/app/{type}/show/entityId/{id}";
        }
    }
}
