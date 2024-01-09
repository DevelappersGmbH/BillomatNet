// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using Develappers.BillomatNet.Api.Net;

namespace Develappers.BillomatNet
{
    public class SubscriptionInvoiceService : ServiceBase
    {
        private const string EntityUrlFragment = "recurrings";

        /// <summary>
        /// Creates a new instance of <see cref="SubscriptionInvoiceService"/>.
        /// </summary>
        /// <param name="httpClient">The http client.</param>
        public SubscriptionInvoiceService(IHttpClient httpClient) : base(httpClient)
        {
        }

        /// <summary>
        /// Gets the portal URL for this entity.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The url to this entity in billomat portal.</returns>
        /// <exception cref="ArgumentException">Thrown when the id is invalid.</exception>
        public string GetPortalUrl(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid subscription id", nameof(id));
            }

            return $"{HttpClient.BaseUrl}app/beta/{EntityUrlFragment}/{id}";
        }
    }
}
