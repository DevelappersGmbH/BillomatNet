using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Helpers;
using System.Threading;
using System.Threading.Tasks;
using Settings = Develappers.BillomatNet.Types.Settings;

namespace Develappers.BillomatNet
{
    public class SettingsService : ServiceBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public SettingsService(Configuration configuration) : base(configuration)
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