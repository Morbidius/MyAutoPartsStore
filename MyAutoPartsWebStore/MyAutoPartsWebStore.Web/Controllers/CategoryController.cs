namespace MyAutoPartsWebStore.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using MyAutoPartsStore.Models.ServiceModels.Products;
    using MyAutoPartsStore.Services.ProductServices;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using static WebConstants.Cache;

    public class CategoryController : Controller
    {
        private readonly IProductService product;
        private readonly IConfigurationProvider mapper;
        private readonly IMemoryCache cache;

        public CategoryController(IProductService product, IMapper mapper, IMemoryCache cache)
        {
            this.product = product;
            this.mapper = mapper.ConfigurationProvider;
            this.cache = cache;
        }

        public IActionResult ViewCategory(int? categoryId = null)
        {
            if (categoryId == null || categoryId <= 0) return BadRequest();

            var categories = this.cache.Get<IList<ProductServiceModel>>(CategoriesCacheKey);

            if (categories == null)
            {
                categories = this.product.GetCategory();

                var cacheOptions = new MemoryCacheEntryOptions()
                     .SetAbsoluteExpiration(TimeSpan.FromMinutes(360));

                this.cache.Set(CategoriesCacheKey, categories, cacheOptions);
            }
            else if (categories.Count != categories.Where(x => x.IsAllowed).Count())
            {
                categories = this.product.GetCategory();

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
