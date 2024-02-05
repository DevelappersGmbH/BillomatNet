// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Queries
{
    internal static class QueryString
    {
        internal static string For(Query<Client, ClientFilter> value)
        {
            return new ClientQueryStringBuilder().BuildFor(value);
        }

        internal static string For(Query<Supplier, SupplierFilter> value)
        {
            return new SupplierQueryStringBuilder().BuildFor(value);
        }

        internal static string For(Query<Template, TemplateFilter> value)
        {
            return new TemplateQueryStringBuilder().BuildFor(value);
        }

        internal static string For(Query<ClientTag, ClientTagFilter> value)
        {
            return new ClientTagQueryStringBuilder().BuildFor(value);
        }

        internal static string For(Query<ClientProperty, ClientPropertyFilter> value)
        {
            return new ClientPropertyQueryStringBuilder().BuildFor(value);
        }

        internal static string For(Query<ArticleTag, ArticleTagFilter> value)
        {
            return new ArticleTagQueryStringBuilder().BuildFor(value);
        }

        internal static string For(Query<Offer, OfferFilter> value)
        {
            return new OfferQueryStringBuilder().BuildFor(value);
        }

        internal static string For(Query<Invoice, InvoiceFilter> value)
        {
            return new InvoiceQueryStringBuilder().BuildFor(value);
        }

        internal static string For(Query<InvoiceComment, InvoiceCommentFilter> value)
        {
            return new InvoiceCommentQueryStringBuilder().BuildFor(value);
        }

        internal static string For(Query<InvoicePayment, InvoicePaymentFilter> value)
        {
            return new InvoicePaymentQueryStringBuilder().BuildFor(value);
        }

        internal static string For(Query<InvoiceTag, InvoiceTagFilter> value)
        {
            return new InvoiceTagQueryStringBuilder().BuildFor(value);
        }

        internal static string For(Query<Article, ArticleFilter> value)
        {
            return new ArticleQueryStringBuilder().BuildFor(value);
        }

        internal static string For(Query<ArticleProperty, ArticlePropertyFilter> value)
        {
            return new ArticlePropertyQueryStringBuilder().BuildFor(value);
        }

        internal static string For(Query<Unit, UnitFilter> value)
        {
            return new UnitQueryStringBuilder().BuildFor(value);
        }

        public static string For(Query<PurchaseInvoice, PurchaseInvoiceFilter> value)
        {
            return new IncomingQueryStringBuilder().BuildFor(value);
        }

        public static string For(Query<InboxDocument, InboxDocumentFilter> value)
        {
            return new InboxDocumentQueryStringBuilder().BuildFor(value);
        }
    }
}
