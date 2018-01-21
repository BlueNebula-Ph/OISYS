namespace Oisys.Web.DTO
{
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// <see cref="OrderDetailSummary"/> class represents the child of OrderSummary object.
    /// </summary>
    public class OrderDetailSummary : DTOBase
    {
        /// <summary>
        /// Gets or sets property Quanity.
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Gets or sets property Item.
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Gets or sets property Item.
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        /// Gets or sets property Price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets property total price.
        /// </summary>
        public decimal TotalPrice { get; set; }
    }
}
