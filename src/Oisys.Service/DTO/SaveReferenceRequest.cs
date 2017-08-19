namespace Oisys.Service.DTO
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// <see cref="SaveReferenceRequest"/> class Create/Update Reference object.
    /// </summary>
    public class SaveReferenceRequest : DTOBase
    {
        /// <summary>
        /// Gets or sets property Code.
        /// </summary>
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets property ParentId.
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Gets or sets property ReferenceTypeId.
        /// </summary>
        public int ReferenceTypeId { get; set; }
    }
}
