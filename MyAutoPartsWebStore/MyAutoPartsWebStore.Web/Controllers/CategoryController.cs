namespace MyAutoPartsWebStore.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using MyAutoPartsStore.Models.ServiceModels.Products;
    using MyAutoPartsStore.Services.CategoryServices;
    using MyAutoPartsStore.Services.ProductServices;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using static WebConstants.Cache;

    public class CategoryController : Controller
    {
        private readonly IMemoryCache cache;
        private readonly ICategoryService category;
        private readonly IProductService product;

        public CategoryController(IMemoryCache cache, ICategoryService category, IProductService product)
        {
            this.category = category;
            this.product = product;
            this.cache = cache;
        }

        public IActionResult ViewCategory(int? categoryId = null)
        {
            if (categoryId == null || categoryId <= 0) return BadRequest();

            var categories = this.cache.Get<IList<ProductServiceModel>>(CategoriesCacheKey);

            if (categories == null)
            {
                categories = this.category.GetCategory();

                var cacheOptions = new MemoryCacheEntryOptions()
                     .SetAbsoluteExpiration(TimeSpan.FromMinutes(360));

                this.cache.Set(CategoriesCacheKey, categories, cacheOptions);
            }
            else if (this.product.GetAprovedProductsCount() != categories.Where(x => x.IsAllowed == true).Count())
            {
                categories = this.category.GetCategory();

                var cacheOptions = new MemoryCacheEntryOptions()
                     .SetAbsoluteExpiration(TimeSpan.FromMinutes(360));

                this.cache.Set(CategoriesCacheKey, categories, cacheOptions);
            }

            categories = categories.Where(c => c.CategoryId == categoryId).ToList();

            return View(categories);
        }

        public IActionResult Error() => View();
    }
}
