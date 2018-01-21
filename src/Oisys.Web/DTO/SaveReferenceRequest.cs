namespace Oisys.Web.DTO
{
    using System.ComponentModel.DataAnnotations;
    using BlueNebula.Common.DTOs;

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
        /// Gets or sets property Parent Code.
        /// </summary>
        public string ParentCode { get; set; }

        /// <summary>
        /// Gets or sets property ReferenceTypeId.
        /// </summary>
        public int ReferenceTypeId { get; set; }

        /// <summary>
        /// Gets or sets property ParentReference
        /// </summary>
        public SaveReferenceRequest ParentReference { get; set; }
    }
}
