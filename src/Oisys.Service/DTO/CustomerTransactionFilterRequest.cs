namespace Oisys.Service.DTO
{
    using System;

    /// <summary>
    /// <see cref="CustomerTransactionFilterRequest"/> class represents basic filter for customer transactions.
    /// </summary>
    public class CustomerTransactionFilterRequest : FilterRequestBase
    {
        /// <summary>
        /// Gets or sets property Customer Id
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets property Date From
        /// </summary>
        public DateTime? DateFrom { get; set; }

        /// <summary>
        /// Gets or sets property Date To
        /// </summary>
        public DateTime? DateTo { get; set; }
    }
}
