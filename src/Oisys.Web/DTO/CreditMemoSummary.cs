﻿namespace Oisys.Web.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// View model for the credit memo entity.
    /// </summary>
    public class CreditMemoSummary : DTOBase
    {
        /// <summary>
        /// Gets or sets property Code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets property Date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets property Driver.
        /// </summary>
        public string Driver { get; set; }

        /// <summary>
        /// Gets or sets property Customer.
        /// </summary>
        public string Customer { get; set; }

        /// <summary>
        /// Gets the total amount property.
        /// </summary>
        public decimal TotalAmount
        {
            get { return this.Details.Sum(a => a.TotalPrice); }
        }

        /// <summary>
        /// Gets or sets property Details.
        /// </summary>
        public ICollection<CreditMemoDetailSummary> Details { get; set; }
    }
}
