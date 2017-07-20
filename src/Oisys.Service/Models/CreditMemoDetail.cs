namespace Oisys.Service.Models
{
    public class CreditMemoDetail : ModelBase
    {
        public int CreditMemoId { get; set; }

        public int ItemId { get; set; }

        public decimal Quantity { get; set; }

        public Item Item { get; set; }

        public CreditMemo CreditMemo { get; set; }
    }
}
