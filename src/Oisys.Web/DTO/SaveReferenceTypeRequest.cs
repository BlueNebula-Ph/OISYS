namespace Oisys.Web.DTO
{
    using System.ComponentModel.DataAnnotations;
    using BlueNebula.Common.DTOs;

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
