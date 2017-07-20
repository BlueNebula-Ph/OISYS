namespace Oisys.Service.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Delivery : ModelBase
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Driver { get; set; }

        public ICollection<DeliveryDetail> Details { get; set; }
    }
}
