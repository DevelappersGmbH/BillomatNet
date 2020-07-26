// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Net;
using FakeItEasy;

namespace Develappers.BillomatNet.Tests.UnitTests
{
    public class ExceptionFactory
    {
        private static WebException CreateWebException(HttpStatusCode statusCode)
        {
            var webResponse = A.Fake<HttpWebResponse>();
            A.CallTo(() => webResponse.StatusCode).Returns(statusCode);
            return new WebException("", null, WebExceptionStatus.ProtocolError, webResponse);
        }


        public static WebException CreateNotFoundException()
        {
            return CreateWebException(HttpStatusCode.NotFound);
        }

        public static WebException CreateNotAuthorizedException()
        {
            return CreateWebException(HttpStatusCode.Unauthorized);
        }
    }
}
