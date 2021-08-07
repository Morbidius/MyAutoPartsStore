﻿namespace MyAutoPartsWebStore.Web.Controllers
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

    using static WebConstants.Cache;

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

            var categories = this.cache.Get<List<ProductListingViewModel>>(CategoriesCacheKey);

            if (categories == null)
            {
                categories = this.data
                   .Products
                   .OrderByDescending(p => p.Name)
                   .ProjectTo<ProductListingViewModel>(this.mapper)
                   .ToList();

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
