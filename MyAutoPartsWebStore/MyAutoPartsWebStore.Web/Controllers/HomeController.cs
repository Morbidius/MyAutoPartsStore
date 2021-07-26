﻿namespace MyAutoPartsWebStore.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyAutoPartsStore.Data;
    using MyAutoPartsWebStore.Web.Models;
    using MyAutoPartsWebStore.Web.Models.Products;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly MyAutoPartsStoreDbContext data;

        public HomeController(MyAutoPartsStoreDbContext data)
        {
            this.data = data;
        }

        public IActionResult Index(string category, string SearchTerm = null, string sortingType = null)
        {
            var viewModel = new ProductsSearchQueryModel();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                var products = data.Products.Where(p =>
                    p.Name.ToLower().Contains(SearchTerm.Trim().ToLower()))
                    .OrderByDescending(p => p.Name)
                    .Select(p => new ProductListingViewModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        Weight = p.Weight,
                        SizeCapacity = p.SizeCapacity,
                        ImageUrl = p.ImageUrl,
                        Category = p.Category.Name
                    }).ToList();

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
            viewModel.Categories = GetProductCategories();
            return View(viewModel);
        }

        private IEnumerable<ProductCategoryViewModel> GetProductCategories()
            => this.data
            .Categories
            .Select(p => new ProductCategoryViewModel
            {
                Id = p.Id,
                Name = p.Name,
            })
            .ToList();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

