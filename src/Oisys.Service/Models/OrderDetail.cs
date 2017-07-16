namespace Oisys.Service.Models
{
    public class OrderDetail : ModelBase
    {
        public int OrderId { get; set; }
        public decimal Quantity { get; set; }
        public int ItemId { get; set; }
        public decimal Price { get; set; }
    }
}
