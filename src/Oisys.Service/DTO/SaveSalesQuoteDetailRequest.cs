namespace Oisys.Service.DTO
{
    using System.ComponentModel.DataAnnotations;
    using BlueNebula.Common.DTOs;
    using Oisys.Service.Models;

    /// <summary>
    /// <see cref="SalesQuoteDetail"/> class represents the child of SalesQuote object.
    /// </summary>
    public class SaveSalesQuoteDetailRequest : DTOBase
    {
        /// <summary>
        /// Gets or sets property SalesQuoteId.
        /// </summary>
        [Required]
        public int SalesQuoteId { get; set; }

        /// <summary>
        /// Gets or sets property Quantity.
        /// </summary>
        [Required]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Gets or sets property ItemId.
        /// </summary>
        [Required]
        public int ItemId { get; set; }

        /// <summary>
        /// Gets or sets property Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets property Price.
        /// </summary>
        [Required]
        public decimal Price { get; set; }
    }
}
