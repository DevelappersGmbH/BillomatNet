// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Develappers.BillomatNet.Api.Net
{
    internal static class HeaderNames
    {
        public const string ApiKey = "X-BillomatApiKey";
        public const string AppId = "X-AppId";
        public const string AppSecret = "X-AppSecret";

        public const string LimitRemaining = "X-Rate-Limit-Remaining";
        public const string LimitReset = "X-Rate-Limit-Reset";
    }
}
