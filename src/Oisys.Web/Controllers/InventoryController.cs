namespace Oisys.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class InventoryController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}