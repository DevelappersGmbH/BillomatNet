// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;
using Develappers.BillomatNet.Api.Net;

namespace Develappers.BillomatNet
{
    public class CreditNoteService : ServiceBase
    {
        /// <summary>
        /// Creates a new instance of <see cref="CreditNoteService"/>.
        /// </summary>
        /// <param name="configuration">The service configuration.</param>
        public CreditNoteService(Configuration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="CreditNoteService"/> for unit tests.
        /// </summary>
        /// <param name="httpClientFactory">The function which creates a new <see cref="IHttpClient" /> implementation.</param>
        /// <exception cref="ArgumentNullException">Thrown when the parameter is null.</exception>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        internal CreditNoteService(Func<IHttpClient> httpClientFactory) : base(httpClientFactory)
        {
        }
    }
}
