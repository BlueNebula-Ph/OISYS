namespace Oisys.Web
{
    using System;
    using AutoMapper;
    using Oisys.Web.DTO;
    using Oisys.Web.Models;

    /// <summary>
    /// <see cref="MappingConfig"/> class Mapping configuration.
    /// </summary>
    public class MappingConfig : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingConfig"/> class.
        /// </summary>
        public MappingConfig()
        {
            // Adjustment
            this.CreateMap<Adjustment, ItemAdjustmentSummary>()
                .ForMember(d => d.Item, s => s.MapFrom(o => o.Item.Name))
                .ForMember(d => d.AdjustmentType, s => s.MapFrom(o => $"{o.AdjustmentType} - {o.QuantityType}"));

            // Category
            this.CreateMap<Category, CategorySummary>();
            this.CreateMap<Category, CategoryLookup>();
            this.CreateMap<SaveCategoryRequest, Category>();

            // City
            this.CreateMap<City, CitySummary>()
                .ForMember(d => d.ProvinceName, s => s.MapFrom(o => o.Province.Name));
            this.CreateMap<SaveCityRequest, City>();

            // Credit Memo
            this.CreateMap<CreditMemo, CreditMemoSummary>();
            this.CreateMap<SaveCreditMemoRequest, CreditMemo>();
            this.CreateMap<CreditMemoDetail, CreditMemoDetailSummary>()
                .ForMember(d => d.Item, s => s.MapFrom(o => o.Item.Name));
            this.CreateMap<SaveCreditMemoDetailRequest, CreditMemoDetail>();

            // TODO: Change LastUpdatedBy value
            // Customer
            this.CreateMap<Customer, CustomerLookup>();

            this.CreateMap<Customer, CustomerSummary>()
                .ForMember(d => d.CityName, s => s.MapFrom(o => o.City.Name))
                .ForMember(d => d.ProvinceName, s => s.MapFrom(o => o.Province.Name));

            this.CreateMap<SaveCustomerRequest, Customer>()
                .ForMember(d => d.LastUpdatedDate, s => s.MapFrom(o => DateTime.Now))
                .ForMember(d => d.LastUpdatedBy, s => s.MapFrom(o => 1))
                .ForMember(d => d.CreatedBy, s => s.MapFrom(o => 1));

            // Customer Transaction
            // TODO: Create method to compute running balance
            this.CreateMap<SaveCustomerTrxRequest, CustomerTransaction>();

            this.CreateMap<CustomerTransaction, CustomerTransactionSummary>()
                .ForMember(d => d.Customer, s => s.MapFrom(o => o.Customer.Name));

            // Delivery
            this.CreateMap<Delivery, DeliverySummary>();

            this.CreateMap<SaveDeliveryRequest, Delivery>();

            // Item
            this.CreateMap<Item, ItemLookup>()
                .ForMember(d => d.CodeName, s => s.MapFrom(o => o.Name));

            this.CreateMap<Item, ItemSummary>()
                .ForMember(d => d.CategoryCode, s => s.MapFrom(o => o.Category.Name));

            this.CreateMap<SaveItemRequest, Item>();

            // Order
            this.CreateMap<Order, OrderSummary>()
                .ForMember(d => d.ProvinceName, s => s.MapFrom(o => o.Customer.Province.Name));

            this.CreateMap<SaveOrderRequest, Order>();

            // Order Detail
            this.CreateMap<OrderDetail, OrderDetailSummary>()
                .ForMember(d => d.Item, s => s.MapFrom(o => o.Item.Name))
                .ForMember(d => d.Unit, s => s.MapFrom(o => o.Item.Unit));

            this.CreateMap<SaveOrderDetailRequest, OrderDetail>();

            // Province
            this.CreateMap<Province, ProvinceSummary>();
            this.CreateMap<Province, ProvinceLookup>();
            this.CreateMap<SaveProvinceRequest, Province>();

            // Reference
            this.CreateMap<Reference, ReferenceLookup>();

            this.CreateMap<Reference, ReferenceSummary>()
                .ForMember(d => d.ReferenceTypeCode, s => s.MapFrom(o => o.ReferenceType.Code));

            this.CreateMap<SaveReferenceRequest, Reference>();

            // ReferenceType
            this.CreateMap<ReferenceType, ReferenceTypeSummary>();

            this.CreateMap<SaveReferenceTypeRequest, ReferenceType>();

            // Sales Quote
            this.CreateMap<SalesQuote, SalesQuoteSummary>()
                .ForMember(d => d.CustomerName, s => s.MapFrom(o => o.Customer.Name))
                .ForMember(d => d.CustomerAddress, s => s.MapFrom(o => $"{o.Customer.Address}, {o.Customer.City.Name} {o.Customer.Province.Name}"));

            this.CreateMap<SaveSalesQuoteRequest, SalesQuote>();

            // Sales Quote Detail
            this.CreateMap<SalesQuoteDetail, SalesQuoteDetailSummary>()
                .ForMember(d => d.Item, s => s.MapFrom(o => o.Item.Name));

            this.CreateMap<SaveSalesQuoteDetailRequest, SalesQuoteDetail>();

            // User
            this.CreateMap<User, UserSummary>()
                .ForMember(d => d.Admin, o => o.MapFrom(s => s.AccessRights.Contains("admin")))
                .ForMember(d => d.CanView, o => o.MapFrom(s => s.AccessRights.Contains("canView")))
                .ForMember(d => d.CanWrite, o => o.MapFrom(s => s.AccessRights.Contains("canWrite")))
                .ForMember(d => d.CanDelete, o => o.MapFrom(s => s.AccessRights.Contains("canDelete")));

            this.CreateMap<SaveUserRequest, User>()
                .ForMember(d => d.PasswordHash, o => o.Ignore());
        }
    }
}
