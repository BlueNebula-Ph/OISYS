namespace Oisys.Web.DTO
{
    using System.ComponentModel.DataAnnotations;
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// Save request for saving categories
    /// </summary>
    public class SaveCategoryRequest : DTOBase
    {
        /// <summary>
        /// Gets or sets the category name
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}