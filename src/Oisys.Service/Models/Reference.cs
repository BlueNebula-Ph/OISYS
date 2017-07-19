namespace Oisys.Service.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Reference : ModelBase
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public int ReferenceTypeId { get; set; }

        public ReferenceType ReferenceType { get; set; }
    }
}
