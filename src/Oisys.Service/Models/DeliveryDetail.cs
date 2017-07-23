namespace Oisys.Service.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// <see cref="DeliveryDetail"/> class DeliveryDetail object.
    /// </summary>
    public class DeliveryDetail : ModelBase
    {
        /// <summary>
        /// Gets or sets property DeliveryId.
        /// </summary>
        [Required]
        public int DeliveryId { get; set; }

        /// <summary>
        /// Gets or sets property OrderDetailId.
        /// </summary>
        [Required]
        public int OrderDetailId { get; set; }

        /// <summary>
        /// Gets or sets property Delivery.
        /// </summary>
        public Delivery Delivery { get; set; }

        /// <summary>
        /// Gets or sets property OrderDetail.
        /// </summary>
        public OrderDetail OrderDetail { get; set; }
    }
}
