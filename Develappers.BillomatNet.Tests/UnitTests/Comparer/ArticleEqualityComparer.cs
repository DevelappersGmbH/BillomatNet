using System.Collections.Generic;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Tests.UnitTests.Comparer
{
    public class ArticleEqualityComparer : IEqualityComparer<Article>
    {
        public bool Equals(Article x, Article y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return x.Id == y.Id &&
                   x.Created == y.Created &&
                   x.Updated == y.Updated &&
                   x.UnitId == y.UnitId &&
                   x.ArticleNumber == y.ArticleNumber &&
                   x.Number == y.Number &&
                   x.NumberLength == y.NumberLength &&
                   x.NumberPrefix == y.NumberPrefix &&
                   x.Title == y.Title &&
                   x.Description == y.Description &&
                   x.SalesPrice == y.SalesPrice &&
                   x.SalesPrice2 == y.SalesPrice2 &&
                   x.SalesPrice3 == y.SalesPrice3 &&
                   x.SalesPrice4 == y.SalesPrice4 &&
                   x.SalesPrice5 == y.SalesPrice5 &&
                   x.CurrencyCode == y.CurrencyCode &&
                   x.TaxId == y.TaxId &&
                   x.PurchasePrice == y.PurchasePrice &&
                   x.PurchasePriceNetGross == y.PurchasePriceNetGross;
        }

        public int GetHashCode(Article obj)
        {
            return obj.Id.GetHashCode() ^
                   obj.Created.GetHashCode() ^
                   obj.Updated.GetHashCode() ^
                   obj.UnitId.GetHashCode() ^
                   obj.ArticleNumber.GetHashCode() ^
                   obj.Number.GetHashCode() ^
                   obj.NumberLength.GetHashCode() ^
                   obj.NumberPrefix.GetHashCode() ^
                   obj.Title.GetHashCode() ^
                   obj.Description.GetHashCode() ^
                   obj.SalesPrice.GetHashCode() ^
                   obj.SalesPrice2.GetHashCode() ^
                   obj.SalesPrice3.GetHashCode() ^
                   obj.SalesPrice4.GetHashCode() ^
                   obj.SalesPrice5.GetHashCode() ^
                   obj.CurrencyCode.GetHashCode() ^
                   obj.TaxId.GetHashCode() ^
                   obj.PurchasePrice.GetHashCode() ^
                   obj.PurchasePriceNetGross.GetHashCode();
        }
    }
}
