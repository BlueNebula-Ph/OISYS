namespace Oisys.Web.DTO
{
    using System;
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// <see cref="CashVoucherFilterRequest"/> class represents basic cash voucher filters for displaying data.
    /// </summary>
    public class CashVoucherFilterRequest : FilterRequestBase
    {
        /// <summary>
        /// Gets or sets property Cach Voucher DateFrom.
        /// </summary>
        public DateTime? DateFrom { get; set; }

        /// <summary>
        /// Gets or sets property CashVoucher DateTo.
        /// </summary>
        public DateTime? DateTo { get; set; }
    }
}
