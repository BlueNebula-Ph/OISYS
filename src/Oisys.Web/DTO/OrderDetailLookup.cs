﻿namespace Oisys.Web.DTO
{
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// <see cref="OrderDetailLookup"/> represents order details in lookup data.
    /// </summary>
    public class OrderDetailLookup : DTOBase
    {
        /// <summary>
        /// Gets or sets item id property.
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// Gets or sets item name property.
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// Gets or sets quantity property.
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Gets or sets unit property.
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Gets or sets price property.
        /// </summary>
        public decimal Price { get; set; }
    }
}
