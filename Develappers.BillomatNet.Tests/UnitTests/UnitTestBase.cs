// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using Develappers.BillomatNet.Api.Net;
using Xunit;

namespace Develappers.BillomatNet.Tests.UnitTests
{
    [Trait(Traits.Category, Traits.Categories.UnitTest)]
    public abstract class UnitTestBase<T> where T : ServiceBase
    {
        protected T GetSystemUnderTest(IHttpClient httpClient)
        {
            if (httpClient == null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            Func<IHttpClient> httpClientFactory = () => httpClient;
            return (T)Activator.CreateInstance(typeof(T), httpClientFactory);
        }
    }
}
