﻿namespace Oisys.Web.DTO
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// The request for saving cash vouchers
    /// </summary>
    public class SaveCashVoucherRequest : DTOBase
    {
        /// <summary>
        /// Gets or sets the voucher number
        /// </summary>
        [Required]
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
        /// Gets or sets category
        /// </summary>
        public string Category { get; set; }
    }
}