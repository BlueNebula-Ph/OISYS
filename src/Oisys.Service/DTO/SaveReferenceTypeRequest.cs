namespace Oisys.Service.DTO
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// <see cref="SaveReferenceTypeRequest"/> class Create/Update ReferenceType object.
    /// </summary>
    public class SaveReferenceTypeRequest : DTOBase
    {
        /// <summary>
        /// Gets or sets property Code.
        /// </summary>
        [Required]
        public string Code { get; set; }
    }
}
