namespace Oisys.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using BlueNebula.Common.Helpers;

    /// <summary>
    /// <see cref="CreditMemo"/> class CreditMemo object.
    /// </summary>
    public class CreditMemo : ModelBase, IObjectWithState
    {
        /// <summary>
        /// Gets or sets property Code.
        /// </summary>
        [Required]
        public int Code { get; set; }

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
        /// Gets or sets a value indicating whether gets or sets property IsDeleted.
        /// </summary>
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets property Details.
        /// </summary>
        public ICollection<CreditMemoDetail> Details { get; set; }

        /// <summary>
        /// Gets or sets property Customer.
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Gets or sets the objectstate property
        /// </summary>
        [NotMapped]
        public ObjectState State { get; set; }
    }
}
