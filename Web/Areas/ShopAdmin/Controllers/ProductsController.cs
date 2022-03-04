using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Web.ViewModels;

namespace Web.Areas.ShopAdmin.Controllers
{
    [Area(nameof(ShopAdmin))]
    public class ProductsController : Controller
    {
        private readonly ProductManager _productManager;
public ProductsController(ProductManager productManager)
        {
            _productManager = productManager;
        }

        // GET: ProductsController
        public IActionResult Index()
        {
            return View(  _productManager.GetAll());
        }

        // GET: ProductsController/Details/5
        public async  Task<IActionResult> Details(int? id)
        {
            if (id.HasValue) return NotFound();
            var selectedProduct=await _productManager.GetById(id.Value);  
            if(selectedProduct == null)  return NotFound(); 
            return View(selectedProduct);
        }

        // GET: ProductsController/Create
        public async Task<IActionResult> Action( int? id)

        {
            ProductActionVM model = new();
            if (id.HasValue)
            {
                var product = await _productManager.GetById(id.Value);
                    if(product == null) return NotFound();
                var currentLanguageRecord = new ProductRecord();
                model.ProductId= product.Id;
                model.CategoryID = product.CategoryId;
                model.Price = product.Price;
                model.Discount = product.Discount;
                model.IsFeatured= product.IsFeatured;
                model.IsSlider = product.IsSlider;
                model.ProductPictureList = product.ProductPictures;
                model.ThumbnailPicture = product.CoverPhotoId;
                model.StockQuantity = product.InStock;
                model.ProductId = product.Id;
                model.CategoryID = product.CategoryId;
                model.Name=currentLanguageRecord.Name;
                model.Description=currentLanguageRecord.Description;
                model.DayProduct = product.IsDay;
                return View(model);


            }
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Action(IFormCollection collection)
        {
            var model=GetProductActionVMfFromForm(collection);  
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

      

   
     

        // GET: ProductsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public  static ProductActionVM GetProductActionVMfFromForm( IFormCollection collection)
        {
            var model = new ProductActionVM
            {
                ProductId = !string.IsNullOrEmpty(collection["ProductId"]) ? int.Parse(collection["ProductId"]) : 0,
                CategoryID = int.Parse(collection[" CategoryID"]),
               Price = decimal.Parse(collection["Price"]),
               IsFeatured = collection["IsFeatured"].Contains("true"),
                IsSlider = collection["IsSlider"].Contains("true"),
                DayProduct = collection[" DayProduct"].Contains("true"),
               ProductPicture=collection["ProductPicture"],
                Discount = !string.IsNullOrEmpty(collection["Discount"]) ? int.Parse(collection["ProductId"]) : 0,
                ThumbnailPicture = !string.IsNullOrEmpty(collection["ThumbnailPicture"])?
                int.Parse(collection["ThumnailPicture"]):0,
               ProductRecordID = !string.IsNullOrEmpty(collection["ProductRecordID"]) ?
                int.Parse(collection["ProductRecordID"]) : 0,
               Name=collection["Name"],
                Description = collection["Description"],
                Summary= collection["Summary"],
            };
            return model;
        }
    }
}
