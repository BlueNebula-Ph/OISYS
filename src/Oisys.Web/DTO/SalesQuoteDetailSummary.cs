namespace Oisys.Web.DTO
{
    using BlueNebula.Common.DTOs;
    using Oisys.Web.Models;

    /// <summary>
    /// View model for the <see cref="SalesQuoteDetail"/> entity.
    /// </summary>
    public class SalesQuoteDetailSummary : DTOBase
    {
        /// <summary>
        /// Gets or sets property SalesQuoteId.
        /// </summary>
        public int SalesQuoteId { get; set; }

        /// <summary>
        /// Gets or sets property Quantity.
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Gets or sets property ItemId.
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// Gets or sets property Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets property Price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets Item navigation property.
        /// </summary>
        public string Item { get; set; }
    }
}
