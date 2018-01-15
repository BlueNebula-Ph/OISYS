namespace Oisys.Web.DTO
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// <see cref="SaveCustomerTrxRequest"/> class.
    /// </summary>
    public class SaveCustomerTrxRequest : DTOBase
    {
        /// <summary>
        /// Gets or sets property Customer Id.
        /// </summary>
        [Required]
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets property Date.
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets property Debit.
        /// </summary>
        public decimal? Debit { get; set; }

        /// <summary>
        /// Gets or sets property Credit.
        /// </summary>
        public decimal? Credit { get; set; }

        /// <summary>
        /// Gets or sets property Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets property Transaction type.
        /// </summary>
        [Required]
        public string TransactionType { get; set; }
    }
}
