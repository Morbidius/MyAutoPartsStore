namespace MyAutoPartsWebStore.Web.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using MyAutoPartsStore.Data;
    using MyAutoPartsWebStore.Web.Models.Products;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CategoryController : Controller
    {
        private readonly MyAutoPartsStoreDbContext data;
        private readonly IConfigurationProvider mapper;
        private readonly IMemoryCache cache;

        public CategoryController(MyAutoPartsStoreDbContext data, IMapper mapper, IMemoryCache cache)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
            this.cache = cache;
        }

        public IActionResult ViewCategory(int? categoryId = null)
        {
            if (categoryId == null || categoryId <= 0) return BadRequest();

            const string categoriesCacheKey = "CategoriesCacheKey";
            var categories = this.cache.Get<List<ProductListingViewModel>>(categoriesCacheKey);

            if (categories == null)
            {
                categories = this.data
                   .Products
                   .Where(p => p.CategoryId == categoryId)
                   .OrderByDescending(p => p.Name)
                   .ProjectTo<ProductListingViewModel>(this.mapper)
                   .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                     .SetAbsoluteExpiration(TimeSpan.FromMinutes(360));

                this.cache.Set(categoriesCacheKey, categories, cacheOptions);
            }

            return View(categories);
        }

        public IActionResult Error() => View();
    }
}
