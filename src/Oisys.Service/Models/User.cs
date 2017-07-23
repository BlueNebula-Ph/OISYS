namespace Oisys.Service.Models
{
    /// <summary>
    /// <see cref="User"/> class represents User of application.
    /// </summary>
    public class User : ModelBase
    {
        /// <summary>
        /// Gets or sets property Code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets property FirstName.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets property LastName.
        /// </summary>
        public string LastName { get; set; }
    }
}