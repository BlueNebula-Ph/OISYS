namespace Oisys.Service.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// <see cref="SaveSalesQuoteRequest"/> class Create/Update SalesQuote object.
    /// </summary>
    public class SaveSalesQuoteRequest : DTOBase
    {
        /// <summary>
        /// Gets or sets property Code.
        /// </summary>
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets property CustomerId.
        /// </summary>
        public int? CustomerId { get; set; }

        /// <summary>
        /// Gets or sets property Date.
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets property DeliveryFee.
        /// </summary>
        [Required]
        public decimal DeliveryFee { get; set; }

        /// <summary>
        /// Gets or sets collection of SalesQuoteDetails navigation property.
        /// </summary>
        public IEnumerable<SaveSalesQuoteDetailRequest> Details { get; set; }
    }
}
