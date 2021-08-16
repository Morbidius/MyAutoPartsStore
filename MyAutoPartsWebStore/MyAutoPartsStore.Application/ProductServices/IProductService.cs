namespace MyAutoPartsStore.Services.ProductServices
{
    using System.Collections.Generic;
    using MyAutoPartsStore.Models;
    using MyAutoPartsStore.Models.BaseModels;
    using MyAutoPartsStore.Models.ServiceModels.Products;

    public interface IProductService
    {
        IEnumerable<ProductServiceModel> All(
            string searchTerm = null,
            ProductSorting sorting = ProductSorting.DateCreated,
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

        bool Delete(int? productId);

        ProductServiceDetailsModel Details(int? id = null);

        ProductServiceDeleteModel GetProductName();

        ProductServiceDeleteModel GetProductById(int? productId);

        IEnumerable<ProductServiceModel> ProductByUser(string userId);

        IEnumerable<TModel> ProductSearch<TModel>(string SearchTerm)
            where TModel : INameModel;

        bool isByDealer(int productId, int dealerId);

        int GetAprovedProductsCount();

        void Approve(int id);
        int AllCounts(
            string searchTerm = null,
            bool isAllowed = true);
    }
}
