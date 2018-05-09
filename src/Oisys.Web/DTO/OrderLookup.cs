namespace Oisys.Web.DTO
{
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
        /// Gets or sets order details property.
        /// </summary>
        public IEnumerable<OrderDetailLookup> Details { get; set; }
    }
}
