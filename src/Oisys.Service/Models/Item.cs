namespace Oisys.Service.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Item : ModelBase
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public int ItemId { get; set; }
        public decimal MainPrice { get; set; }
        public decimal NEPrice { get; set; }
        public decimal WalkInPrice { get; set; }
        public decimal Weight { get; set; }
        public decimal Thickness { get; set; }
        public string Unit { get; set; }
        public int CategoryId { get; set; }
        public decimal Quantity { get; set; }
    }
}
