namespace Oisys.Service.DTO
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;
    using Oisys.Service.Helpers;
    using Oisys.Service.Models;

    /// <summary>
    /// <see cref="OrderDetail"/> class represents the child of Order object.
    /// </summary>
    public class SaveOrderDetailRequest : DTOBase, IObjectWithState
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
        public int? DeliveryId { get; set; }

        /// <summary>
        /// Gets or sets property Price.
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the TotalPrice. Quantity * Price.
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether order detail is deleted.
        /// </summary>
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the state property
        /// </summary>
        [JsonIgnore]
        public ObjectState State { get; set; }
    }
}
