namespace Oisys.Service.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// <see cref="CreditMemo"/> class CreditMemo object.
    /// </summary>
    public class CreditMemo : ModelBase
    {
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
        /// Gets or sets property Driver.
        /// </summary>
        public string Driver { get; set; }

        /// <summary>
        /// Gets or sets property CustomerId.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets property Details.
        /// </summary>
        public ICollection<CreditMemoDetail> Details { get; set; }

        /// <summary>
        /// Gets or sets property Customer.
        /// </summary>
        public Customer Customer { get; set; }
    }
}
