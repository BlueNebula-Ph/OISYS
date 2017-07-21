namespace Oisys.Service.Models
{
    using System.ComponentModel.DataAnnotations;

    public class OrderDetail : ModelBase
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public decimal Price { get; set; }

        public Order Order { get; set; }
    }
}
