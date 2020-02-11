namespace Develappers.BillomatNet.Api
{
    internal abstract class PagedListWrapper<T>
    {
        public abstract T Item { get; set; }
    }
}