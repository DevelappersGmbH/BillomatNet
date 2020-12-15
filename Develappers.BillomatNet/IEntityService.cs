// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Queries;

namespace Develappers.BillomatNet
{
    internal interface IEntityService<TModel, TFilter> : IEntityReadService<TModel, TFilter>
        where TFilter : new()
    {
        Task DeleteAsync(int id, CancellationToken token = default);
        Task<TModel> CreateAsync(TModel model, CancellationToken token = default);
        Task<TModel> EditAsync(TModel model, CancellationToken token = default);
    }
}
