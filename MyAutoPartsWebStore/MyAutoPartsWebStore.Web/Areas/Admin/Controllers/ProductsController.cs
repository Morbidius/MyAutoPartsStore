namespace MyAutoPartsWebStore.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyAutoPartsStore.Models.ViewModels.Products;
    using MyAutoPartsStore.Services.ProductServices;

    public class ProductsController : AdminController
    {
        private readonly IProductService products;

        public ProductsController(IProductService products)
        {
            this.products = products;
        }

        public IActionResult AllProducts([FromQuery] AllProductsQueryModel query)
        {
            var queryResult = this.products.All(
                query.Name,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllProductsQueryModel.ProductsPerPage);

            var products = this.products.All(isAllowed: false).Products;

            query.TotalProducts = queryResult.TotalProducts;
            query.Products = queryResult.Products;

            return View(products);
        }

        public IActionResult Approve(int id)
        {
            this.products.Approve(id);

            return RedirectToAction(nameof(AllProducts));
        }
    }
}
