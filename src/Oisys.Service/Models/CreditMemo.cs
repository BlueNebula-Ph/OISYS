namespace Oisys.Service.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreditMemo : ModelBase
    {
        [Required]
        public string Code { get; set; }

        public DateTime Date { get; set; }

        public string Driver { get; set; }

        public int CustomerId { get; set; }

        public ICollection<CreditMemoDetail> Details { get; set; }

        public Customer Customer { get; set; }
    }
}
