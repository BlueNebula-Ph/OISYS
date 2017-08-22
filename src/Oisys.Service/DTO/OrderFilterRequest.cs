namespace Oisys.Service.DTO
{
    using System;

    /// <summary>
    /// <see cref="OrderFilterRequest"/> class represents basic Order filter for displaying data.
    /// </summary>
    public class OrderFilterRequest : FilterRequestBase
    {
        /// <summary>
        /// Gets or sets property Code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets property Customer Id.
        /// </summary>
        public int? CustomerId { get; set; }

        /// <summary>
        /// Gets or sets property Order Date.
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Gets or sets property Item.
        /// </summary>
        public int? ItemId { get; set; }
    }
}
