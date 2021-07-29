namespace MyAutoPartsStore.Services.ProductServices
{
    using System.Collections.Generic;

    public interface IProductService
    {
        ProductQueryServiceModel Add(
            string name,
            string description,
            decimal price,
            string sizeCapacity,
            float weight,
            string imageUrl,
            string categoryId,
            int dealerId);

        IEnumerable<ProductServiceModel> ProductByUser(string userId);
    }
}
