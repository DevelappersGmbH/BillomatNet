// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using Microsoft.Extensions.DependencyInjection;
// ReSharper disable UnusedMember.Global

namespace Develappers.BillomatNet
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureBillomatNet(this IServiceCollection services, Configuration config)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            services.AddSingleton(config);
            services.AddScoped<ArticleService>();
            services.AddScoped<ClientService>();
            services.AddScoped<CreditNoteService>();
            services.AddScoped<DeliveryNoteService>();
            services.AddScoped<InvoiceService>();
            services.AddScoped<LetterService>();
            services.AddScoped<OfferService>();
            services.AddScoped<OrderConfirmationService>();
            services.AddScoped<PurchaseInvoiceService>();
            services.AddScoped<ReminderService>();
            services.AddScoped<SettingsService>();
            services.AddScoped<SubscriptionInvoiceService>();
            services.AddScoped<SupplierService>();
            services.AddScoped<TaxService>();
            services.AddScoped<UnitService>();
            return services;
        }
    }
}
