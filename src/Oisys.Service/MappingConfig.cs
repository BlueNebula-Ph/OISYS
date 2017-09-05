namespace Oisys.Service
{
    using System;
    using AutoMapper;
    using Oisys.Service.DTO;
    using Oisys.Service.Models;

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
            // TODO: Change LastUpdatedBy value
            // Customer
            this.CreateMap<Customer, CustomerLookup>();

            this.CreateMap<Customer, CustomerSummary>()
                .ForMember(d => d.CityName, s => s.MapFrom(o => o.City.Code))
                .ForMember(d => d.ProvinceName, s => s.MapFrom(o => o.Province.Code));

            this.CreateMap<SaveCustomerRequest, Customer>()
                .ForMember(d => d.LastUpdatedDate, s => s.MapFrom(o => DateTime.Now))
                .ForMember(d => d.LastUpdatedBy, s => s.MapFrom(o => 1))
                .ForMember(d => d.CreatedBy, s => s.MapFrom(o => 1));

            // Customer Transaction
            // TODO: Create method to compute running balance
            this.CreateMap<SaveCustomerTrxRequest, CustomerTransaction>();

            this.CreateMap<CustomerTransaction, CustomerTransactionSummary>();

            // Delivery
            this.CreateMap<Delivery, DeliverySummary>();

            // Item
            this.CreateMap<Item, ItemLookup>()
                .ForMember(d => d.CodeName, s => s.MapFrom(o => $"{o.Code} - {o.Name}"));

            this.CreateMap<Item, ItemSummary>()
                .ForMember(d => d.CategoryCode, s => s.MapFrom(o => o.Category.Code));

            this.CreateMap<SaveItemRequest, Item>();

            // Order
            this.CreateMap<Order, OrderSummary>();

            this.CreateMap<SaveOrderRequest, Order>();

            // Order Detail
            this.CreateMap<OrderDetail, OrderDetailSummary>();

            this.CreateMap<SaveOrderDetailRequest, OrderDetail>();

            // Reference
            this.CreateMap<Reference, ReferenceLookup>();

            this.CreateMap<Reference, ReferenceSummary>()
                .ForMember(d => d.ParentCode, s => s.MapFrom(o => o.ParentReference.Code))
                .ForMember(d => d.ReferenceTypeCode, s => s.MapFrom(o => o.ReferenceType.Code));

            this.CreateMap<SaveReferenceRequest, Reference>();

            // ReferenceType
            this.CreateMap<ReferenceType, ReferenceTypeSummary>();

            this.CreateMap<SaveReferenceTypeRequest, ReferenceType>();
        }
    }
}
