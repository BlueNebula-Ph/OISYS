namespace Oisys.Service.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ModelBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public int LastUpdatedBy { get; set; }

        [Required]
        public DateTime LastUpdatedDate { get; set; }
    }
}
