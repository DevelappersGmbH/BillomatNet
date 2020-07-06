using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Helpers;
using Develappers.BillomatNet.Queries;
using Develappers.BillomatNet.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unit = Develappers.BillomatNet.Types.Unit;

namespace Develappers.BillomatNet
{
    public class UnitService : ServiceBase
    {
        public UnitService(Configuration configuration) : base(configuration)
        {
        }

        //public async Task<Unit> GetByIdAsync(int id, CancellationToken token = default(CancellationToken))
        //{
        //    var jsonModel = await GetItemByIdAsync<UnitWrapper>($"/api/units/{id}", token).ConfigureAwait(false);
        //    return jsonModel.ToDomain();
        //}
    }
}
