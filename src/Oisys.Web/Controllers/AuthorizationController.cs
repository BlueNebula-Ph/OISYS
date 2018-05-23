namespace Oisys.Web.Controllers
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Oisys.Web.Configuration;
    using Oisys.Web.DTO;
    using Oisys.Web.Models;

    /// <summary>
    /// The <see cref="AuthorizationController"/> manages security tokens for client applications.
    /// </summary>
    [Route("auth")]
    [Produces("application/json")]
    public class AuthorizationController : Controller
    {
        private readonly OisysDbContext context;
        private readonly IPasswordHasher<ApplicationUser> hasher;
        private readonly IOptions<AuthOptions> authOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationController"/> class.
        /// </summary>
        /// <param name="context">The database context</param>
        /// <param name="hasher">The password hasher</param>
        /// <param name="authOptions">The authentication configuration</param>
        public AuthorizationController(
            OisysDbContext context,
            IPasswordHasher<ApplicationUser> hasher,
            IOptions<AuthOptions> authOptions)
        {
            this.context = context;
            this.hasher = hasher;
            this.authOptions = authOptions;
        }

        /// <summary>
        /// Generates a Json Web Token
        /// </summary>
        /// <param name="model">The login request model</param>
        /// <returns>An object containing the JWT</returns>
        [HttpPost("connect/token")]
        [Produces("application/json")]
        public async Task<IActionResult> Token([FromForm]LoginRequest model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            // Validate User
            var user = await this.context.Users.FirstOrDefaultAsync(a => a.Username == model.Username);
            if (user != null)
            {
                if (this.hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Success)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, model.Username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.GivenName, $"{user.Firstname} {user.Lastname}"),
                        new Claim("accessRights", user.AccessRights),
                    };

                    var signingCred = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.authOptions.Value.Key)), SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: this.authOptions.Value.Issuer,
                        audience: this.authOptions.Value.Audience,
                        claims: claims,
                        expires: DateTime.UtcNow.AddDays(60),
                        notBefore: DateTime.UtcNow,
                        signingCredentials: signingCred);

                    return this.Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expirationDate = token.ValidTo,
                    });
                }

                return this.BadRequest(new { error = "Incorrect username or password." });
            }

            return this.BadRequest(new { error = "Unable to login. User not found." });
        }
    }
}
