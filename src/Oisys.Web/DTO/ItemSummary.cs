﻿namespace Oisys.Web.DTO
{
    using System.Collections.Generic;
    using BlueNebula.Common.DTOs;

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
        /// Gets or sets property category id.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets property category name.
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Gets or sets property Quantity.
        /// </summary>
        public decimal ActualQuantity { get; set; }

        /// <summary>
        /// Gets or sets property Quantity.
        /// </summary>
        public decimal CurrentQuantity { get; set; }

        /// <summary>
        /// Gets or sets the adjustments collection.
        /// </summary>
        public IEnumerable<ItemAdjustmentSummary> Adjustments { get; set; }
    }
}
