using Develappers.BillomatNet.Api;
using System.Linq;
using Unit = Develappers.BillomatNet.Types.Unit;

namespace Develappers.BillomatNet.Helpers
{
    internal static class UnitMappingExtensions
    {
        internal static Types.PagedList<Unit> ToDomain(this UnitListWrapper value)
        {
            return value?.Item.ToDomain();
        }

        internal static Types.PagedList<Unit> ToDomain(this UnitList value)
        {
            if (value == null)
            {
                return null;
            }

            return new Types.PagedList<Unit>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(x => ToDomain((Api.Unit)x)).ToList()
            };
        }

        internal static Unit ToDomain(this UnitWrapper value)
        {
            return value?.Unit.ToDomain();
        }

        private static Unit ToDomain(this Api.Unit value)
        {
            if (value == null)
            {
                return null;
            }

            return new Unit
            {
                Id = int.Parse(value.Id),
                Created = value.Created,
                Updated = value.Updated,
                Name = value.Name
            };
        }
    }
}
