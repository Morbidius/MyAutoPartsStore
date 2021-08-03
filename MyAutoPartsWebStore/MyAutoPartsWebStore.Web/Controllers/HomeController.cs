namespace MyAutoPartsWebStore.Web.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Mvc;
    using MyAutoPartsStore.Data;
    using MyAutoPartsStore.Services.DealersServices;
    using MyAutoPartsStore.Services.ProductServices;
    using MyAutoPartsWebStore.Web.Models;
    using MyAutoPartsWebStore.Web.Models.Products;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly MyAutoPartsStoreDbContext data;
        private readonly IProductService products;
        private readonly IDealerService dealers;
        private readonly IMapper mapper;

        public HomeController(MyAutoPartsStoreDbContext data, IProductService products, IDealerService dealers, IMapper mapper)
        {
            this.data = data;
            this.products = products;
            this.dealers = dealers;
            this.mapper = mapper;
        }

        public IActionResult Index(string category, string SearchTerm = null, string sortingType = null)
        {
            var viewModel = new ProductsSearchQueryModel();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                var products = data.Products.Where(p =>
                    p.Name.ToLower().Contains(SearchTerm.Trim().ToLower()))
                    .OrderByDescending(p => p.Name)
                    .ProjectTo<ProductListingViewModel>(this.mapper.ConfigurationProvider)
                    .ToList();

                if (!string.IsNullOrEmpty(sortingType))
                {
                    products = products.Where(x => x.Category == sortingType).ToList();
                }


                viewModel.SearchTerm = SearchTerm;
                viewModel.Category = category;
                viewModel.Products = products;
            }
            else
            {
                viewModel.Products = new List<ProductListingViewModel>();
            }
            viewModel.Categories = this.products.AllCategories();
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

