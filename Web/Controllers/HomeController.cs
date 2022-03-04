using Microsoft.AspNetCore.Mvc;
using Services;
using System.Diagnostics;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller

    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductManager _productManager;

        public HomeController(ILogger<HomeController> logger, ProductManager productManager)
        {
            _logger = logger;
            _productManager = productManager;
        }

        public async Task<IActionResult> Index( int id)
        {
            IndexVM vm = new() {
                
                ProductFeatured=_productManager.GetAll(p=>p.IsFeatured),
                ProductIsSlider = _productManager.GetAll(p => p.IsSlider)
            };

            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}