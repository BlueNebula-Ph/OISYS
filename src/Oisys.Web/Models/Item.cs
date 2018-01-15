namespace Oisys.Web.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// <see cref="Item"/> class Item/Inventory object.
    /// </summary>
    public class Item : ModelBase
    {
        /// <summary>
        /// Gets or sets property Code.
        /// </summary>
        [Required]
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
        public string Weight { get; set; }

        /// <summary>
        /// Gets or sets property Thickness.
        /// </summary>
        public string Thickness { get; set; }

        /// <summary>
        /// Gets or sets property Unit.
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Gets or sets property CategoryId.
        /// </summary>
        [Required]
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets property Actual quantity or physical quantity of the item.
        /// </summary>
        public decimal? ActualQuantity { get; set; }

        /// <summary>
        /// Gets or sets property Current quantity or Actual quantity - order quantity.
        /// </summary>
        public decimal? CurrentQuantity { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets property IsDeleted.
        /// </summary>
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets property Tag.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets property Category.
        /// </summary>
        public Reference Category { get; set; }

        /// <summary>
        /// Gets or sets the adjustments collection.
        /// </summary>
        public ICollection<Adjustment> Adjustments { get; set; }
    }
}
