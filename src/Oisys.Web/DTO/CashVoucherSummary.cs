﻿namespace Oisys.Web.DTO
{
    using System;
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// The cash voucher view model
    /// </summary>
    public class CashVoucherSummary : DTOBase
    {
        /// <summary>
        /// Gets or sets the voucher number
        /// </summary>
        public string VoucherNumber { get; set; }

        /// <summary>
        /// Gets or sets the voucher date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets pay to
        /// </summary>
        public string PayTo { get; set; }

        /// <summary>
        /// Gets or sets description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets amount
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets released by
        /// </summary>
        public string ReleasedBy { get; set; }

        /// <summary>
        /// Gets or sets the category name
        /// </summary>
        public string Category { get; set; }
    }
}
