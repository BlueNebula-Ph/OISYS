namespace Oisys.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Oisys.Web.Helpers;

    /// <summary>
    /// <see cref="OrderDetail"/> class represents the child of Order object.
    /// </summary>
    public class OrderDetail : ModelBase, IObjectWithState
    {
        /// <summary>
        /// Gets or sets property OrderId.
        /// </summary>
        [Required]
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets property Quanity.
        /// </summary>
        [Required]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Gets or sets property ItemId.
        /// </summary>
        [Required]
        public int ItemId { get; set; }

        /// <summary>
        /// Gets or sets property DeliveryId.
        /// </summary>
        public int DeliveryId { get; set; }

        /// <summary>
        /// Gets or sets property Price.
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets property TotalPrice.
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Gets or sets Order navigation property.
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// Gets or sets Item navigation property.
        /// </summary>
        public Item Item { get; set; }

        /// <summary>
        /// Gets or sets Delivery navigation property.
        /// </summary>
        public Delivery Delivery { get; set; }

        /// <summary>
        /// Gets or sets the objectstate
        /// </summary>
        [NotMapped]
        public ObjectState State { get; set; }
    }
}
