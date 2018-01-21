namespace Oisys.Web.Models
{
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
        /// Gets or sets ReferenceType navigation property.
        /// </summary>
        public ReferenceType ReferenceType { get; set; }
    }
}
