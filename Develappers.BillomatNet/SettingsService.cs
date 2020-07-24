using System;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Helpers;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Api.Net;
using Settings = Develappers.BillomatNet.Types.Settings;

namespace Develappers.BillomatNet
{
    public class SettingsService : ServiceBase
    {
        /// <summary>
        /// Creates a new instance of <see cref="SettingsService"/>.
        /// </summary>
        /// <param name="configuration">The service configuration.</param>
        public SettingsService(Configuration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="SettingsService"/> for unit tests.
        /// </summary>
        /// <param name="httpClientFactory">The function which creates a new <see cref="IHttpClient" /> implementation.</param>
        /// <exception cref="ArgumentNullException">Thrown when the parameter is null.</exception>
        internal SettingsService(Func<IHttpClient> httpClientFactory) : base(httpClientFactory)
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
