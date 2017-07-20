namespace Oisys.Service.Models
{
    using System.ComponentModel.DataAnnotations;

    public class DeliveryDetail : ModelBase
    {
        [Required]
        public int DeliveryId { get; set; }

        [Required]
        public int OrderDetailId { get; set; }

        public Delivery Delivery { get; set; }

        public OrderDetail OrderDetail { get; set; }
    }
}
