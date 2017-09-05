namespace Oisys.Service.DTO
{
    /// <summary>
    /// View model for the Item entity.
    /// </summary>
    public class ItemSummary : DTOBase
    {
        /// <summary>
        /// Gets or sets property Code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets property Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets property Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets property MainPrice.
        /// </summary>
        public decimal? MainPrice { get; set; }

        /// <summary>
        /// Gets or sets property NEPrice.
        /// </summary>
        public decimal? NEPrice { get; set; }

        /// <summary>
        /// Gets or sets property WalkInPrice.
        /// </summary>
        public decimal? WalkInPrice { get; set; }

        /// <summary>
        /// Gets or sets property Weight.
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// Gets or sets property Thickness.
        /// </summary>
        public decimal Thickness { get; set; }

        /// <summary>
        /// Gets or sets property Unit.
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Gets or sets property CategoryId.
        /// </summary>
        public string CategoryCode { get; set; }

        /// <summary>
        /// Gets or sets property Quantity.
        /// </summary>
        public decimal ActualQuantity { get; set; }

        /// <summary>
        /// Gets or sets property Quantity.
        /// </summary>
        public decimal CurrentQuantity { get; set; }
    }
}
