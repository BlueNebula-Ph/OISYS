 namespace Oisys.Web.DTO
{
    using System;
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// <see cref="InvoiceFilterRequest"/> class represents basic invoice filters for displaying data.
    /// </summary>
    public class InvoiceFilterRequest : FilterRequestBase
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
