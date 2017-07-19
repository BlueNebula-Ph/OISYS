namespace Oisys.Service.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ReferenceType : ModelBase
    {
        [Required]
        public string Code { get; set; }
    }
}
