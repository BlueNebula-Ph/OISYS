namespace Oisys.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ManagementController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}