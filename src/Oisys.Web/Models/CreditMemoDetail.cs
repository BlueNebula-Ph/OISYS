namespace Oisys.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using BlueNebula.Common.Helpers;

    /// <summary>
    /// <see cref="CreditMemoDetail"/> class CreditMemodetail object.
    /// </summary>
    public class CreditMemoDetail : ModelBase, IObjectWithState
    {
        /// <summary>
        /// Gets or sets property CreditMemoId.
        /// </summary>
        [Required]
        public int CreditMemoId { get; set; }

        /// <summary>
        /// Gets or sets property Quantity.
        /// </summary>
        [Required]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Gets or sets property OrderDetailId.
        /// </summary>
        [Required]
        public int OrderDetailId { get; set; }

        /// <summary>
        /// Gets or sets property OrderDetail.
        /// </summary>
        public OrderDetail OrderDetail { get; set; }

        /// <summary>
        /// Gets or sets property CreditMemo.
        /// </summary>
        public CreditMemo CreditMemo { get; set; }

        /// <summary>
        /// Gets or sets the objectstate property
        /// </summary>
        [NotMapped]
        public ObjectState State { get; set; }
    }
}
