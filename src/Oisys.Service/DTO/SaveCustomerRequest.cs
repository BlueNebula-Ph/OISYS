namespace Oisys.Service.DTO
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// <see cref="SaveCustomerRequest"/> class Create/Update Customer object.
    /// </summary>
    public class SaveCustomerRequest : DTOBase
    {
        /// <summary>
        /// Gets or sets property Code.
        /// </summary>
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets property Name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets property Email.
        /// </summary>
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets property Phone Number.
        /// </summary>
        [DataType(DataType.PhoneNumber)]
        [Required]
        public string ContactNumber { get; set; }

        /// <summary>
        /// Gets or sets property Contact Person.
        /// </summary>
        [Required]
        public string ContactPerson { get; set; }

        /// <summary>
        /// Gets or sets property Address.
        /// </summary>
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets property City.
        /// </summary>
        [Required]
        public int CityId { get; set; }

        /// <summary>
        /// Gets or sets property Province.
        /// </summary>
        [Required]
        public int ProvinceId { get; set; }

        /// <summary>
        /// Gets or sets property Terms.
        /// </summary>
        [Required]
        public string Terms { get; set; }

        /// <summary>
        /// Gets or sets property Discount.
        /// </summary>
        [Required]
        public string Discount { get; set; }

        /// <summary>
        /// Gets or sets property Price List.
        /// </summary>
        [Required]
        public string PriceList { get; set; }
    }
}
