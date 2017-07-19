using System.ComponentModel.DataAnnotations;

namespace Oisys.Service.Models
{
    public class Customer : ModelBase
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

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
        public string Term { get; set; }

        [Required]
        public string Discount { get; set; }

        [Required]
        public string PriceList { get; set; }

        public Reference City { get; set; }

        public Reference Province { get; set; }
    }
}
