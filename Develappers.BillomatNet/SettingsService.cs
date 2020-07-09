using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Helpers;
using System.Threading;
using System.Threading.Tasks;
using Settings = Develappers.BillomatNet.Types.Settings;

namespace Develappers.BillomatNet
{
    public class SettingsService : ServiceBase
    {
        public SettingsService(Configuration configuration) : base(configuration)
        {
        }

        public async Task<Settings> GetAsync(CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetItemByIdAsync<SettingsWrapper>($"/api/settings/", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }
    }
}