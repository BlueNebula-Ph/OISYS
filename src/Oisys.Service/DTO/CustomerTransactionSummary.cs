namespace Oisys.Service.DTO
{
    using System;

    /// <summary>
    /// <see cref="CustomerTransactionSummary"/> class.
    /// </summary>
    public class CustomerTransactionSummary : DTOBase
    {
        /// <summary>
        /// Gets or sets property Customer Id.
        /// </summary>
        public int CustomerId { get; set; }

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
