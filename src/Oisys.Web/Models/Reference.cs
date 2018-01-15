namespace Oisys.Web.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// <see cref="Reference"/> class represents common values that would be used throughout the application.
    /// </summary>
    public class Reference : ModelBase
    {
        /// <summary>
        /// Gets or sets property Code.
        /// </summary>
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets property ReferenceTypeId.
        /// </summary>
        [Required]
        public int ReferenceTypeId { get; set; }

        /// <summary>
        /// Gets or sets property ParentId.
        /// </summary>
        public int? ParentReferenceId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets property IsDeleted.
        /// </summary>
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets ReferenceType navigation property.
        /// </summary>
        public ReferenceType ReferenceType { get; set; }

        /// <summary>
        /// Gets or sets Parent navigation property.
        /// </summary>
        public Reference ParentReference { get; set; }
    }
}
