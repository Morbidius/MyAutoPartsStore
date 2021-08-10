namespace MyAutoPartsStore.Services.CategoryServices
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MyAutoPartsStore.Data;
    using MyAutoPartsStore.Models.ServiceModels.Products;
    using System.Collections.Generic;
    using System.Linq;

    public class CategoryService : ICategoryService
    {
        private readonly MyAutoPartsStoreDbContext data;
        private readonly IMapper mapper;

        public CategoryService(MyAutoPartsStoreDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public bool CategoryЕxists(int categoryId)
            => this.data
            .Categories
            .Any(c => c.Id == categoryId);

        public IEnumerable<ProductServiceCategoryModel> AllCategories()
           => this.data
           .Categories
           .ProjectTo<ProductServiceCategoryModel>(this.mapper.ConfigurationProvider)
           .ToList();

        public IList<ProductServiceModel> GetCategory()
            => this.data
                   .Products
                   .Where(p => p.IsAllowed == true)
                   .OrderByDescending(p => p.Name)
                   .ProjectTo<ProductServiceModel>(this.mapper.ConfigurationProvider)
                   .ToList();
    }
}
