namespace MyAutoPartsStore.Services.ProductServices
{
    using System.Collections.Generic;
    using MyAutoPartsStore.Models.ServiceModels.Products;

    public interface IProductService
    {
        int Create(
            string name,
            string description,
            decimal price,
            string sizeCapacity,
            float weight,
            string imageUrl,
            int categoryId,
            int dealerId);

        bool Edit(
            int productId,
            string name,
            string description,
            decimal price,
            string sizeCapacity,
            float weight,
            string imageUrl,
            int categoryId);

        ProductServiceDetailsModel Details(int? id = null);

        ProductServiceDeleteModel GetProductName();

        IEnumerable<ProductServiceModel> ProductByUser(string userId);

        bool isByDealer(int productId, int dealerId);

        bool CategoryЕxists(int categoryId);

        IEnumerable<ProductServiceCategoryModel> AllCategories();
    }
}
