namespace Oisys.Service.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Item : ModelBase
    {
        [Required]
        public string Code { get; set; }

        public string Description { get; set; }

        public decimal MainPrice { get; set; }

        public decimal NEPrice { get; set; }

        public decimal WalkInPrice { get; set; }

        public decimal Weight { get; set; }

        public decimal Thickness { get; set; }

        public string Unit { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        public Reference Category { get; set; }
    }
}
