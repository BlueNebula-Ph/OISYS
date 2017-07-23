namespace Oisys.Service.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// <see cref="CustomerTransaction"/> class.
    /// </summary>
    public class CustomerTransaction : ModelBase
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
        /// Gets or sets property RunningBalance.
        /// </summary>
        public decimal RunningBalance { get; set; }

        /// <summary>
        /// Gets or sets property Customer.
        /// </summary>
        public Customer Customer { get; set; }
    }
}
