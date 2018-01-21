namespace Oisys.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The main entry point from the user interface.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Serves the index page.
        /// </summary>
        /// <returns>The index view.</returns>
        public IActionResult Index()
        {
            return this.View();
        }
    }
}