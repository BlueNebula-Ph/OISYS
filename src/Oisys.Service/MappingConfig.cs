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
            this.CreateMap<SaveCustomerRequest, Customer>()
                .ForMember(d => d.LastUpdatedDate, s => s.MapFrom(o => DateTime.Now))
                .ForMember(d => d.LastUpdatedBy, s => s.MapFrom(o => 1))
                .ForMember(d => d.CreatedBy, s => s.MapFrom(o => 1));

            this.CreateMap<Customer, CustomerSummary>()
                .ForMember(d => d.CityName, s => s.MapFrom(o => o.City.Code))
                .ForMember(d => d.ProvinceName, s => s.MapFrom(o => o.Province.Code));

            // Customer Transaction
            //TODO: Create method to compute running balance
            this.CreateMap<SaveCustomerTrxRequest, CustomerTransaction>();

            this.CreateMap<CustomerTransaction, CustomerTransactionSummary>();

            // ReferenceType
            this.CreateMap<ReferenceType, ReferenceTypeSummary>();

            this.CreateMap<SaveReferenceTypeRequest, ReferenceType>();
        }
    }
}
