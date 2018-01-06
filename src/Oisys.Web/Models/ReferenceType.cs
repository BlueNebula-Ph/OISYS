namespace Oisys.Service.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// <see cref="ReferenceType"/> class represents common values types that would be used throughout the application.
    /// </summary>
    public class ReferenceType : ModelBase
    {
        /// <summary>
        /// Gets or sets a value indicating whether gets or sets property Code.
        /// </summary>
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets property IsDeleted.
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
