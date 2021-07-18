namespace MyAutoPartsWebStore.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyAutoPartsStore.Data;
    using MyAutoPartsWebStore.Web.Models.Products;
    using System.Linq;

    public class CategoryController : Controller
    {
        private readonly MyAutoPartsStoreDbContext data;

        public CategoryController(MyAutoPartsStoreDbContext data)
        {
            this.data = data;
        }
        
        public IActionResult ViewCategory(int? categoryId = null)
        {
            if (categoryId == null || categoryId <= 0) return BadRequest();

            var categoryProducts = this.data
                .Products
                .Where(p => p.CategoryId == categoryId)
                .OrderByDescending(p => p.Name)
                .Select(p => new ProductListingViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    SizeCapacity = p.SizeCapacity,
                    Weight = p.Weight,
                    ImageUrl = p.ImageUrl,
                    Category = p.Category.Name,
                });

            return View(categoryProducts);
        }
    }
}
