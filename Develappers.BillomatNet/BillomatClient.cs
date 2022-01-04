// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using Develappers.BillomatNet.Api.Net;

namespace Develappers.BillomatNet
{
    public class BillomatClient
    {
        private readonly IHttpClient _httpClient;
        private readonly Lazy<ClientService> _clientService;
        private readonly Lazy<ActivityFeedService> _activityFeedService;
        private readonly Lazy<DeliveryNoteService> _deliveryNoteService;
        private readonly Lazy<InboxDocumentService> _inboxDocumentService;
        private readonly Lazy<ArticleService> _articleService;
        private readonly Lazy<LetterService> _letterService;
        private readonly Lazy<InvoiceService> _invoiceService;
        private readonly Lazy<OrderConfirmationService> _orderConfirmationService;
        private readonly Lazy<CreditNoteService> _creditNoteService;
        private readonly Lazy<OfferService> _offerService;
        private readonly Lazy<PurchaseInvoiceService> _purchaseInvoiceService;
        private readonly Lazy<ReminderService> _reminderService;
        private readonly Lazy<SettingsService> _settingsService;
        private readonly Lazy<SubscriptionInvoiceService> _subscriptionInvoiceService;
        private readonly Lazy<SupplierService> _supplierService;
        private readonly Lazy<TaxService> _taxService;
        private readonly Lazy<UnitService> _unitService;

        public BillomatClient(Configuration configuration) : this(new HttpClient(configuration.BillomatId, configuration.ApiKey)
        {
            AppId = configuration.AppId,
            AppSecret = configuration.AppSecret
        })
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BillomatClient" /> class.
        /// </summary>
        /// <param name="httpClient">The <see cref="IHttpClient" /> implementation.</param>
        /// <exception cref="ArgumentNullException">Thrown when the parameter is null.</exception>
        /// <remarks>
        /// Used to create a new instance for tests. Should be exposed as internal constructor to create unit tests.
        /// </remarks>
        protected BillomatClient(IHttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _clientService = new Lazy<ClientService>(() => new ClientService(httpClient));
            _activityFeedService = new Lazy<ActivityFeedService>(() => new ActivityFeedService(httpClient));
            _deliveryNoteService = new Lazy<DeliveryNoteService>(() => new DeliveryNoteService(httpClient));
            _inboxDocumentService = new Lazy<InboxDocumentService>(() => new InboxDocumentService(httpClient));
            _articleService = new Lazy<ArticleService>(() => new ArticleService(httpClient));
            _letterService = new Lazy<LetterService>(() => new LetterService(httpClient));
            _invoiceService = new Lazy<InvoiceService>(() => new InvoiceService(httpClient));
            _orderConfirmationService = new Lazy<OrderConfirmationService>(() => new OrderConfirmationService(httpClient));
            _creditNoteService = new Lazy<CreditNoteService>(() => new CreditNoteService(httpClient));
            _offerService = new Lazy<OfferService>(() => new OfferService(httpClient));
            _purchaseInvoiceService = new Lazy<PurchaseInvoiceService>(() => new PurchaseInvoiceService(httpClient));
            _reminderService = new Lazy<ReminderService>(() => new ReminderService(httpClient));
            _settingsService = new Lazy<SettingsService>(() => new SettingsService(httpClient));
            _subscriptionInvoiceService = new Lazy<SubscriptionInvoiceService>(() => new SubscriptionInvoiceService(httpClient));
            _supplierService = new Lazy<SupplierService>(() => new SupplierService(httpClient));
            _taxService = new Lazy<TaxService>(() => new TaxService(httpClient));
            _unitService = new Lazy<UnitService>(() => new UnitService(httpClient));
        }

        public int ApiCallLimit => _httpClient.ApiCallLimit;
        public DateTime ApiCallLimitResetsAt => _httpClient.ApiCallLimitResetsAt;

        public ClientService Clients => _clientService.Value;
        public ActivityFeedService ActivityFeed => _activityFeedService.Value;
        public DeliveryNoteService DeliveryNotes => _deliveryNoteService.Value;
        public InboxDocumentService InboxDocuments => _inboxDocumentService.Value;
        public ArticleService Articles => _articleService.Value;
        public LetterService Letters => _letterService.Value;
        public InvoiceService Invoices => _invoiceService.Value;
        public OrderConfirmationService OrderConfirmations => _orderConfirmationService.Value;
        public CreditNoteService CreditNotes => _creditNoteService.Value;
        public OfferService Offers => _offerService.Value;
        public PurchaseInvoiceService PurchaseInvoices => _purchaseInvoiceService.Value;
        public ReminderService Reminders => _reminderService.Value;
        public SettingsService Settings => _settingsService.Value;
        public SubscriptionInvoiceService SubscriptionInvoices => _subscriptionInvoiceService.Value;
        public SupplierService Suppliers => _supplierService.Value;
        public TaxService Taxes => _taxService.Value;
        public UnitService Units => _unitService.Value;
    }
}
