namespace MyAutoPartsWebStore.Web.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Mvc;
    using MyAutoPartsStore.Data;
    using MyAutoPartsWebStore.Web.Models.Products;
    using System.Linq;

    public class CategoryController : Controller
    {
        private readonly MyAutoPartsStoreDbContext data;
        private readonly IConfigurationProvider mapper;

        public CategoryController(MyAutoPartsStoreDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

        public IActionResult ViewCategory(int? categoryId = null)
        {
            if (categoryId == null || categoryId <= 0) return BadRequest();

            var categoryProducts = this.data
                .Products
                .Where(p => p.CategoryId == categoryId)
                .OrderByDescending(p => p.Name)
                .ProjectTo<ProductListingViewModel>(this.mapper);

            return View(categoryProducts);
        }
    }
}
