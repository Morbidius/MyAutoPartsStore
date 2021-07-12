namespace MyAutoPartsWebStore.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyAutoPartsStore.Data;
    using MyAutoPartsWebStore.Web.Models.Products;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductsController : Controller
    {
        private readonly MyAutoPartsStoreDbContext data;

        public ProductsController(MyAutoPartsStoreDbContext data)
        {
            this.data = data;
        }

        public IActionResult Add() => View(new AddProductViewModel
        {
            Categories = this.GetProductCategories()
        });

        [HttpPost]
        public IActionResult Add(AddProductViewModel product)
        {
            return View();
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
    }
}
