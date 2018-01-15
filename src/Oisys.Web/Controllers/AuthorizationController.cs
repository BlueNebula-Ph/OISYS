namespace Oisys.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using AspNet.Security.OpenIdConnect.Primitives;
    using AspNet.Security.OpenIdConnect.Server;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// <see cref="AuthorizationController"/> handles token requests.
    /// </summary>
    [Produces("application/json")]
    public class AuthorizationController : Controller
    {
        /// <summary>
        /// Validates user credentials and returns a token if user is valid.
        /// </summary>
        /// <param name="request">The token request</param>
        /// <returns>Token object</returns>
        [HttpPost("connect/token")]
        public IActionResult GetToken(OpenIdConnectRequest request)
        {
            if (request.IsPasswordGrantType())
            {
                // Validate the user credentials here
                if (request.Username != "SampleUser" || request.Password != "SamplePassword")
                {
                    return this.Forbid(OpenIdConnectServerDefaults.AuthenticationScheme);
                }

                var identity = new ClaimsIdentity(
                    OpenIdConnectServerDefaults.AuthenticationScheme,
                    OpenIdConnectConstants.Claims.Name,
                    OpenIdConnectConstants.Claims.Role);

                identity.AddClaim(new Claim(
                    OpenIdConnectConstants.Claims.Subject,
                    "SubjectId",
                    OpenIdConnectConstants.Destinations.AccessToken));

                identity.AddClaim(new Claim(
                    OpenIdConnectConstants.Claims.Name,
                    "SampleUser",
                    OpenIdConnectConstants.Destinations.AccessToken));

                var principal = new ClaimsPrincipal(identity);

                return this.SignIn(principal, OpenIdConnectServerDefaults.AuthenticationScheme);
            }

            throw new InvalidOperationException("The requested grant type is not supported.");
        }
    }
}
