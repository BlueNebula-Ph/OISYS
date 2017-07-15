namespace Oisys.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}