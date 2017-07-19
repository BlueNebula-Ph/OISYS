namespace Oisys.Service.Models
{
    public class SalesQuoteDetail : ModelBase
    {
        public int SalesQuoteId { get; set; }
        public decimal Quantity { get; set; }
        public int ItemId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
