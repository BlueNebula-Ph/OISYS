namespace Oisys.Web.DTO
{
    using System;
    using System.Collections.Generic;
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// View model for the Sales Quote entity.
    /// </summary>
    public class SalesQuoteSummary : DTOBase
    {
        /// <summary>
        /// Gets or sets property Quote Number.
        /// </summary>
        public string QuoteNumber { get; set; }

        /// <summary>
        /// Gets or sets property CustomerId.
        /// </summary>
        public int? CustomerId { get; set; }

        /// <summary>
        /// Gets or sets property Customer Name.
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets property Customer Address.
        /// </summary>
        public string CustomerAddress { get; set; }

        /// <summary>
        /// Gets or sets property Date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets property DeliveryFee.
        /// </summary>
        public decimal DeliveryFee { get; set; }

        /// <summary>
        /// Gets or sets property TotalAmount.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets collection of SalesQuoteDetailSummary navigation property.
        /// </summary>
        public IEnumerable<SalesQuoteDetailSummary> Details { get; set; }
    }
}
