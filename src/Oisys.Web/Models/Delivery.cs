namespace Oisys.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// <see cref="Delivery"/> class Delivery object.
    /// </summary>
    public class Delivery : ModelBase
    {
        /// <summary>
        /// Gets or sets property Delivery Number.
        /// </summary>
        public int DeliveryNumber { get; set; }

        /// <summary>
        /// Gets or sets property Date.
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets property CustomerId.
        /// </summary>
        [Required]
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets property IsDeleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets property Driver.
        /// </summary>
        public string Driver { get; set; }

        /// <summary>
        /// Gets or sets property TotalAmount.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets property Details.
        /// </summary>
        public ICollection<DeliveryDetail> Details { get; set; }

        /// <summary>
        /// Gets or sets navigation property to customer.
        /// </summary>
        public Customer Customer { get; set; }
    }
}
