namespace Oisys.Service.DTO
{
    /// <summary>
    /// <see cref="OrderDetailSummary"/> class represents the child of OrderSummary object.
    /// </summary>
    public class OrderDetailSummary : DTOBase
    {
        /// <summary>
        /// Gets or sets property OrderId.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets property Quanity.
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Gets or sets property ItemId.
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// Gets or sets property DeliveryId.
        /// </summary>
        public int DeliveryId { get; set; }

        /// <summary>
        /// Gets or sets property Price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets OrderSummary navigation property.
        /// </summary>
        public OrderSummary Order { get; set; }

        /// <summary>
        /// Gets or sets ItemSummary navigation property.
        /// </summary>
        public ItemSummary Item { get; set; }

        /// <summary>
        /// Gets or sets Delivery navigation property.
        /// </summary>
        public DeliverySummary Delivery { get; set; }
    }
}
