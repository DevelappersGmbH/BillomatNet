// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Api.Net;
using Develappers.BillomatNet.Mapping;
using Settings = Develappers.BillomatNet.Types.Settings;

namespace Develappers.BillomatNet
{
    public class SettingsService : ServiceBase
    {
        /// <summary>
        /// Creates a new instance of <see cref="SettingsService"/>.
        /// </summary>
        /// <param name="httpClient">The http client.</param>
        public SettingsService(IHttpClient httpClient) : base(httpClient)
        {
        }

        /// <summary>
        /// Returns the settings.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The settings or null if not found.</returns>
        public async Task<Settings> GetAsync(CancellationToken token = default)
        {
            var jsonModel = await GetItemByIdAsync<SettingsWrapper>("/api/settings/", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }
    }
}
