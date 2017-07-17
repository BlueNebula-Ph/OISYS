namespace Oisys.Service.Models
{
    public class Inventory : ModelBase
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public int ItemId { get; set; }
        public decimal MainPrice { get; set; }
        public decimal NEPrice { get; set; }
        public decimal WalkInPrice { get; set; }
        public decimal Weight { get; set; }
        public decimal Thickness { get; set; }
    }
}
