// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Net;
using System.Net.Http;

namespace Develappers.BillomatNet.Tests.UnitTests
{
    public class ExceptionFactory
    {
        private static HttpRequestException CreateWebException(HttpStatusCode statusCode)
        {
            try
            {
                var response = new HttpResponseMessage(statusCode);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException exception)
            {
                return exception;
            }
            return new HttpRequestException("", null, statusCode);
        }


        public static HttpRequestException CreateNotFoundException()
        {
            return CreateWebException(HttpStatusCode.NotFound);
        }

        public static HttpRequestException CreateNotAuthorizedException()
        {
            return CreateWebException(HttpStatusCode.Unauthorized);
        }
    }
}
