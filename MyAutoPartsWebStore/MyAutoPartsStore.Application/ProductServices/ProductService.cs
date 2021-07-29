namespace MyAutoPartsStore.Services.ProductServices
{
    using MyAutoPartsStore.Data;
    using MyAutoPartsStore.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductService : IProductService
    {
        private readonly MyAutoPartsStoreDbContext data;

        public ProductService(MyAutoPartsStoreDbContext data)
        {
            this.data = data;
        }

        public ProductQueryServiceModel Add(string name, string description, decimal price, string sizeCapacity, float weight, string imageUrl, string categoryId, int dealerId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ProductServiceModel> ProductByUser(string userId)
            => GetProducts(this.data
            .Products
            .Where(p => p.Dealer.UserId == userId));



        private static IEnumerable<ProductServiceModel> GetProducts(IQueryable<Product> productQuery)
            => productQuery
            .Select(p => new ProductServiceModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                SizeCapacity = p.SizeCapacity,
                Weight = p.Weight,
                ImageUrl = p.ImageUrl,
                Category = p.Category.Name,
            });
    }
}
