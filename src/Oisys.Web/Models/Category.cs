namespace Oisys.Web.Models
{
    /// <summary>
    /// Entity model for category.
    /// </summary>
    public class Category : ModelBase
    {
        /// <summary>
        /// Gets or sets the category name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the category is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
