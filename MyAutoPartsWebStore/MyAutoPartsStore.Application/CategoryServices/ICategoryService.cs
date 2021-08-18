namespace MyAutoPartsStore.Services.CategoryServices
{
    using MyAutoPartsStore.Models.ServiceModels.Products;
    using System.Collections.Generic;

    public interface ICategoryService
    {
        IEnumerable<ProductServiceCategoryModel> AllCategories();

        IList<ProductServiceModel> GetCategory();

        bool CategoryЕxists(int categoryId);
    }
}
