namespace Oisys.Web.DTO
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// <see cref="SaveInvoiceRequest"/> class Create/Update SaveInvoiceRequest object.
    /// </summary>
    public class SaveInvoiceRequest : DTOBase
    {
        /// <summary>
        /// Gets or sets property CustomerId.
        /// </summary>
        [Required]
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets property Code.
        /// </summary>
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets property Date.
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets property TotalAmount.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets property IsDeleted.
        /// </summary>
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
