namespace Oisys.Web.DTO
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The view model used to login to the system
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
