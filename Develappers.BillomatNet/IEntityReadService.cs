// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Queries;

namespace Develappers.BillomatNet
{
    interface IEntityReadService<TModel, TFilter>
        where TFilter : new()
    {
        Task<Types.PagedList<TModel>> GetListAsync(CancellationToken token = default);
        Task<Types.PagedList<TModel>> GetListAsync(Query<TModel, TFilter> query, CancellationToken token = default);
        Task<TModel> GetByIdAsync(int id, CancellationToken token = default);
    }
}
