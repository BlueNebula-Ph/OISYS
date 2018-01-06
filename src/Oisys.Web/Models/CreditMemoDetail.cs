namespace Oisys.Service.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// <see cref="CreditMemoDetail"/> class CreditMemodetail object.
    /// </summary>
    public class CreditMemoDetail : ModelBase
    {
        /// <summary>
        /// Gets or sets property CreditMemoId.
        /// </summary>
        [Required]
        public int CreditMemoId { get; set; }

        /// <summary>
        /// Gets or sets property ItemId.
        /// </summary>
        [Required]
        public int ItemId { get; set; }

        /// <summary>
        /// Gets or sets property Quantity.
        /// </summary>
        [Required]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Gets or sets property Item.
        /// </summary>
        public Item Item { get; set; }

        /// <summary>
        /// Gets or sets property CreditMemo.
        /// </summary>
        public CreditMemo CreditMemo { get; set; }
    }
}
