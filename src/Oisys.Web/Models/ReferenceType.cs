namespace Oisys.Web.Models
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
    }
}
