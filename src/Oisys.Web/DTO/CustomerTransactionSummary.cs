namespace Oisys.Web.DTO
{
    using System;
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// <see cref="CustomerTransactionSummary"/> class.
    /// </summary>
    public class CustomerTransactionSummary : DTOBase
    {
        /// <summary>
        /// Gets or sets property Customer.
        /// </summary>
        public string Customer { get; set; }

        /// <summary>
        /// Gets or sets property Date.
        /// </summary>
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
        /// Gets or sets property Transaction type.
        /// </summary>
        public string TransactionType { get; set; }
    }
}
