namespace MyAutoPartsWebStore.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyAutoPartsStore.Services.ProductServices;

    public class ProductsController : AdminController
    {
        private readonly IProductService products;

        public ProductsController(IProductService products)
        {
            this.products = products;
        }

        public IActionResult AllProducts()
        {
            var products = this.products.All(isAllowed: false).Products;

            return View(products);
        }

        public IActionResult Approve(int id)
        {
            this.products.Approve(id);

            return RedirectToAction(nameof(AllProducts));
        }
    }
}
