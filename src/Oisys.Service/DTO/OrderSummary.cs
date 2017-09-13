namespace Oisys.Service.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// View model for the order entity.
    /// </summary>
    public class OrderSummary : DTOBase
    {
        /// <summary>
        /// Gets or sets property Code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets property CustomerId.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets property Date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets property DueDate.
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Gets or sets property total amount.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets property Customer navigation property.
        /// </summary>
        public CustomerSummary Customer { get; set; }

        /// <summary>
        /// Gets or sets property Details navigation property.
        /// </summary>
        public IEnumerable<OrderDetailSummary> Details { get; set; }
    }
}
