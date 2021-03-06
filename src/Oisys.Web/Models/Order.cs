﻿namespace Oisys.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using BlueNebula.Common.Helpers;

    /// <summary>
    /// <see cref="Order"/> class represents the Order object.
    /// </summary>
    public class Order : ModelBase, IObjectWithState
    {
        /// <summary>
        /// Gets or sets property Code.
        /// </summary>
        [Required]
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets property CustomerId.
        /// </summary>
        [Required]
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets property Date.
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets property DueDate.
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Gets or sets property DiscountPercent.
        /// </summary>
        public decimal DiscountPercent { get; set; }

        /// <summary>
        /// Gets or sets property DiscountAmount.
        /// </summary>
        public decimal DiscountAmount { get; set; }

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
        /// Gets or sets property Customer navigation property.
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Gets or sets property Details navigation property.
        /// </summary>
        public ICollection<OrderDetail> Details { get; set; }

        /// <summary>
        /// Gets or sets the objectstate property
        /// </summary>
        [NotMapped]
        public ObjectState State { get; set; }
    }
}