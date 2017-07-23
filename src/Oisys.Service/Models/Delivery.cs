namespace Oisys.Service.Models
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
        [Required]
        public string Driver { get; set; }

        /// <summary>
        /// Gets or sets property Details.
        /// </summary>
        public ICollection<DeliveryDetail> Details { get; set; }
    }
}
