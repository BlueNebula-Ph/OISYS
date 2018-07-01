namespace Oisys.Web.DTO
{
    using System;
    using System.Collections.Generic;
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// <see cref="OrderLookup"/> class represents order data for dropdowns.
    /// </summary>
    public class OrderLookup : DTOBase
    {
        /// <summary>
        /// Gets or sets code property.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets date property.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets discount property.
        /// </summary>
        public decimal DiscountPercent { get; set; }

        /// <summary>
        /// Gets or sets total amount property.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets order details property.
        /// </summary>
        public IEnumerable<OrderDetailLookup> Details { get; set; }
    }
}