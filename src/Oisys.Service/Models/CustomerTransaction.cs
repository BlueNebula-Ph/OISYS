namespace Oisys.Service.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CustomerTransaction : ModelBase
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public decimal? Debit { get; set; }

        public decimal? Credit { get; set; }

        public string Description { get; set; }

        public decimal RunningBalance { get; set; }

        public Customer Customer { get; set; }
    }
}
