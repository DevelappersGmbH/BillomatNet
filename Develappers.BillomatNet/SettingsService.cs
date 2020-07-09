using Develappers.BillomatNet.Api;
using System.Threading;
using System.Threading.Tasks;

namespace Develappers.BillomatNet
{
    public class SettingsService : ServiceBase
    {
        public SettingsService(Configuration configuration) : base(configuration)
        {
        }

        public async Task<Settings> GetByIdAsync(CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetItemByIdAsync<InvoiceWrapper>($"/api/settings", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }
    }
}