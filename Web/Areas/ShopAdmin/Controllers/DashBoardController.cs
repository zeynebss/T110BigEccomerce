using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.ShopAdmin.Controllers
{
    [Area(nameof(ShopAdmin))]
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
