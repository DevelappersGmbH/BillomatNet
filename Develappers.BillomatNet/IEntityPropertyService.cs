// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Queries;

namespace Develappers.BillomatNet
{
    internal interface IEntityPropertyService<TProperty, TPropertyFilter>
        where TPropertyFilter : new()
    {
        Task<TProperty> GetPropertyByIdAsync(int id, CancellationToken token = default);
        Task<Types.PagedList<TProperty>> GetPropertyListAsync(CancellationToken token = default);
        Task<Types.PagedList<TProperty>> GetPropertyListAsync(Query<TProperty, TPropertyFilter> query, CancellationToken token = default);
        Task<TProperty> EditPropertyAsync(TProperty model, CancellationToken token = default);
    }
}
