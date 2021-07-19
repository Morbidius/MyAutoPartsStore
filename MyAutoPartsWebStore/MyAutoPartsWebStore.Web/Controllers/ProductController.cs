namespace MyAutoPartsWebStore.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using MyAutoPartsStore.Data;
    using MyAutoPartsStore.Data.Models;
    using MyAutoPartsWebStore.Web.Models.Products;

    public class ProductController : Controller
    {
        private readonly MyAutoPartsStoreDbContext data;

        public ProductController(MyAutoPartsStoreDbContext data)
        {
            this.data = data;
        }

        public IActionResult Add() => View(new AddProductViewModel()
        {
            Categories = GetProductCategories()
        });

        [HttpPost]
        public IActionResult Add(AddProductViewModel product)
        {
            if (!this.data.Categories.Any(c => c.Id == product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                product.Categories = this.GetProductCategories();

                return View(product);
            };

            var newProduct = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                SizeCapacity = product.SizeCapacity,
                Weight = product.Weight,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
            };

            this.data.Products.Add(newProduct);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Delete(int? id = null)
        {
            if (id == null || id <= 0) return BadRequest();

            var productToDel = data.Products.FirstOrDefault(x => x.Id == id);

            if (productToDel == null) return NotFound();

            var viewModel = new DeleteProductViewModel()
            {
                Id = productToDel.Id,
                Name = productToDel.Name,
                Image = productToDel.ImageUrl,
            };
            return View(viewModel);
        }

        //ppc trqbva tova da ne kazva viewModel a FormModel shtoto ne e za view a za forma
        //trqbva da stane DeleteProductFormModel
        //tui to
        [HttpPost]
        public IActionResult Delete(DeleteProductViewModel viewProduct, int? id = null)
        {
            if (id == null || id <= 0) return BadRequest();

            if (!ModelState.IsValid) return View(viewProduct);

            var product = this.data
                .Products
                .FirstOrDefault(p => p.Id == viewProduct.Id);

            if (product == null) return NotFound();

            this.data.Remove(product);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Description(int? id = null)
        {
            if (id == null || id <= 0) return BadRequest();

            var details = this.data
                .Products
                .Where(p => p.Id == id)
                .Select(p => new DetailsViewModel
                {
                    Id = p.Id,
                    Description = p.Description,
                    Name = p.Name,
                    Price = p.Price,
                    SizeCapacity = p.SizeCapacity,
                    Weight = p.Weight,
                    ImageUrl = p.ImageUrl,

                }).FirstOrDefault();

            if (details == null) return NotFound();

            return View(details);
        }

        public IActionResult Search(string searchTerm)
        {
            var productQuery = this.data.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productQuery = productQuery.Where(p =>
                p.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var products = productQuery
                .OrderByDescending(p => p.Name)
                .Select(p => new ProductListingViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    SizeCapacity = p.SizeCapacity,
                    Weight = p.Weight,
                    ImageUrl = p.ImageUrl,
                    Category = p.Category.Name
                }).ToList();

            var productCategories = this.data
                .Products
                .Select(c => c.Category.Name)
                .Distinct()
                .ToList();

            return View(new ProductsSearchQueryModel
            {
                Products = products,
                SearchTerm = searchTerm,
            });
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

        private DeleteProductViewModel GetProductName()
            => this.data
            .Products
            .Select(p => new DeleteProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
            }).FirstOrDefault();
    }
}
