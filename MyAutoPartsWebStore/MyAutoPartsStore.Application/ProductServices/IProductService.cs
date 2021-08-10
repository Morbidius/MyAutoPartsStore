namespace MyAutoPartsStore.Services.ProductServices
{
    using System.Collections.Generic;
    using MyAutoPartsStore.Models.BaseModels;
    using MyAutoPartsStore.Models.ServiceModels.Products;

    public interface IProductService
    {
        ProductServiceQueryModel All(
            string name = null,
            string searchTerm = null,
            bool isAllowed = true);

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

        IEnumerable<TModel> ProductSearch<TModel>(string SearchTerm)
            where TModel : INameModel;

        bool isByDealer(int productId, int dealerId);

        int GetAprovedProductsCount();

        void Approve(int id);
    }
}
