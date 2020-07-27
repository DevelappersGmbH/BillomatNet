// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Develappers.BillomatNet.Queries
{
    /// <summary>
    /// Represents the page settings.
    /// </summary>
    public class PagingSettings
    {
        public PagingSettings()
        {
            Page = 1;
            ItemsPerPage = 100;
        }

        /// <summary>
        /// number of the requested page (1 based)
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// items per page
        /// </summary>
        public int ItemsPerPage { get; set; }
    }
}
