namespace Oisys.Service.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CreditMemoDetail : ModelBase
    {
        [Required]
        public int CreditMemoId { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        public Item Item { get; set; }

        public CreditMemo CreditMemo { get; set; }
    }
}
