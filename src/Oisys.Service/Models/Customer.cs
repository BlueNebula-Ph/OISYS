namespace Oisys.Service.Models
{
    public class Customer : ModelBase
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public int ProvinceId { get; set; }
        public string Term { get; set; }
        public string Discount { get; set; }
        public string PriceList { get; set; }
    }
}
