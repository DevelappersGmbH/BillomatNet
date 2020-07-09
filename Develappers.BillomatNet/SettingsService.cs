namespace Develappers.BillomatNet
{
    public class SettingsService : ServiceBase
    {
        public SettingsService(Configuration configuration) : base(configuration)
        {
        }

        //public async Task<Settings> GetByIdAsync(int id, CancellationToken token = default(CancellationToken))
        //{
        //    var jsonModel = await GetItemByIdAsync<InvoiceWrapper>($"/api/invoices/{id}", token).ConfigureAwait(false);
        //    return jsonModel.ToDomain();
        //}
    }
}