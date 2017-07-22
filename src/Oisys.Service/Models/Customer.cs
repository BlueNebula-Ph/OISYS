namespace Oisys.Service.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class Customer : ModelBase
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required]
        public string ContactNumber { get; set; }

        [Required]
        public string ContactPerson { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public int CityId { get; set; }

        [Required]
        public int ProvinceId { get; set; }

        [Required]
        public string Terms { get; set; }

        [Required]
        public string Discount { get; set; }

        [Required]
        public string PriceList { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public Reference City { get; set; }

        public Reference Province { get; set; }

        public ICollection<CustomerTransaction> Transactions { get; set; }
    }
}
