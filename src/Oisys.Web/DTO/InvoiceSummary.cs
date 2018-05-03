namespace Oisys.Web.DTO
{
    using BlueNebula.Common.DTOs;
    using System;

    /// <summary>
    /// View model for the Invoice entity.
    /// </summary>
    public class InvoiceSummary : DTOBase
    {
        /// <summary>
        /// Gets or sets property CustomerId.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets property Code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets property Date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets property TotalAmount.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets property Customer.
        /// </summary>
        public string CustomerName { get; set; }
    }
}
