namespace MyAutoPartsWebStore.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyAutoPartsStore.Models.ServiceModels.Products;
    using MyAutoPartsStore.Services.ProductServices;
    using MyAutoPartsStore.Services.CategoryServices;
    using MyAutoPartsWebStore.Web.Models.Products;
    using System.Collections.Generic;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly IProductService products;
        private readonly ICategoryService category;

        public HomeController(IProductService products, ICategoryService category)
        {
            this.products = products;
            this.category = category;
        }

        public IActionResult Index(string categoryInput, string SearchTerm = null, string sortingType = null)
        {
            var viewModel = new ProductsSearchQueryModel();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                var products = this.products.ProductSearch<ProductServiceModel>(SearchTerm);

                if (!string.IsNullOrEmpty(sortingType))
                {
                    products = products.Where(x => x.Category == sortingType).ToList();
                }

                viewModel.SearchTerm = SearchTerm;
                viewModel.Category = categoryInput;
                viewModel.Products = products.Where(p => p.IsAllowed);
            }
            else
            {
                viewModel.Products = new List<ProductServiceModel>();
            }

            viewModel.Categories = this.category.AllCategories();

            return View(viewModel);
        }

        public IActionResult Error() => View();
    }
}

