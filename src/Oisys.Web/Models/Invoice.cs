﻿namespace Oisys.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// <see cref="Invoice"/> class Invoice object.
    /// </summary>
    public class Invoice : ModelBase
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

        /// <summary>
        /// Gets or sets property Orders navigation property.
        /// </summary>
        public ICollection<Order> Orders { get; set; }

        /// <summary>
        /// Gets or sets property Customer.
        /// </summary>
        public Customer Customer { get; set; }
    }
}
