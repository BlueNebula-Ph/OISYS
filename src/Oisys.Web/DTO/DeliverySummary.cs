namespace Oisys.Web.DTO
{
    using System;
    using System.Collections.Generic;
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// <see cref="DeliverySummary"/> class DeliverySummary object.
    /// </summary>
    public class DeliverySummary : DTOBase
    {
        /// <summary>
        /// Gets or sets property Code.
        /// </summary>
        public string DeliveryNumber { get; set; }

        /// <summary>
        /// Gets or sets property CustomerName.
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets property Date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets property Driver.
        /// </summary>
        public string Driver { get; set; }

        /// <summary>
        /// Gets or sets property Details.
        /// </summary>
        public IEnumerable<DeliveryDetailSummary> Details { get; set; }
    }
}
