// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Develappers.BillomatNet
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureBillomatNet(this IServiceCollection services, Configuration config)
        {
            services.AddSingleton(config);
            services.AddScoped<Develappers.BillomatNet.ArticleService>();
            services.AddScoped<Develappers.BillomatNet.ClientService>();
            services.AddScoped<Develappers.BillomatNet.CreditNoteService>();
            services.AddScoped<Develappers.BillomatNet.DeliveryNoteService>();
            services.AddScoped<Develappers.BillomatNet.InvoiceService>();
            services.AddScoped<Develappers.BillomatNet.LetterService>();
            services.AddScoped<Develappers.BillomatNet.OfferService>();
            services.AddScoped<Develappers.BillomatNet.OrderConfirmationService>();
            services.AddScoped<Develappers.BillomatNet.PurchaseInvoiceService>();
            services.AddScoped<Develappers.BillomatNet.ReminderService>();
            services.AddScoped<Develappers.BillomatNet.SettingsService>();
            services.AddScoped<Develappers.BillomatNet.SubscriptionInvoiceService>();
            services.AddScoped<Develappers.BillomatNet.SupplierService>();
            services.AddScoped<Develappers.BillomatNet.TaxService>();
            services.AddScoped<Develappers.BillomatNet.UnitService>();
            services.AddSingleton<BillomatUrlBuilder>();
            return services;
        }
    }
}
