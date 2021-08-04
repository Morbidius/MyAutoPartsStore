namespace MyAutoPartsStore.Services.ProductServices
{
    using MyAutoPartsStore.Data;
    using MyAutoPartsStore.Data.Models;
    using MyAutoPartsStore.Models.ServiceModels.Products;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductService : IProductService
    {
        private readonly MyAutoPartsStoreDbContext data;

        public ProductService(MyAutoPartsStoreDbContext data)
        {
            this.data = data;
        }

        public int Create(
            string name, string description, decimal price, string sizeCapacity,
            float weight, string imageUrl, int categoryId, int dealerId)
        {
            var newProduct = new Product
            {
                Name = name,
                Description = description,
                Price = price,
                SizeCapacity = sizeCapacity,
                Weight = weight,
                ImageUrl = imageUrl,
                CategoryId = categoryId,
                DealerId = dealerId,
            };

            this.data.Products.Add(newProduct);
            this.data.SaveChanges();

            return newProduct.Id;
        }

        public bool Edit(
            int productId, string name, string description, decimal price,
            string sizeCapacity, float weight, string imageUrl, int categoryId)
        {
            var productData = this.data.Products.Find(productId);

            if (productData == null)
            {
                return false;
            }

            productData.Name = name;
            productData.Description = description;
            productData.Price = price;
            productData.SizeCapacity = sizeCapacity;
            productData.Weight = weight;
            productData.ImageUrl = imageUrl;
            productData.CategoryId = categoryId;

            this.data.SaveChanges();

            return true;
        }

        public ProductServiceDetailsModel Details(int? id = null)
            => this.data
                 .Products
                 .Where(p => p.Id == id)
                 .Select(p => new ProductServiceDetailsModel
                 {
                     Id = p.Id,
                     Name = p.Name,
                     Description = p.Description,
                     Price = p.Price,
                     SizeCapacity = p.SizeCapacity,
                     Weight = p.Weight,
                     ImageUrl = p.ImageUrl,
                     CategoryId = p.CategoryId,
                     Category = p.Category.Name,
                     DealerId = p.DealerId,
                     DealerName = p.Dealer.Name,
                     UserId = p.Dealer.UserId,
                 }).FirstOrDefault();

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
               DealerId = p.DealerId,
           });

        public bool isByDealer(int productId, int dealerId)
            => this.data
            .Products
            .Any(p => p.Id == productId && p.DealerId == dealerId);

        public IEnumerable<ProductServiceModel> ProductByUser(string userId)
            => GetProducts(this.data
            .Products
            .Where(p => p.Dealer.UserId == userId));

        public IEnumerable<ProductServiceCategoryModel> AllCategories()
            => this.data
            .Categories
            .Select(p => new ProductServiceCategoryModel
            {
                Id = p.Id,
                Name = p.Name,
            })
            .ToList();

        public bool CategoryЕxists(int categoryId)
            => this.data
            .Categories
            .Any(c => c.Id == categoryId);

        public ProductServiceDeleteModel GetProductName() 
            => this.data
            .Products
            .Select(p => new ProductServiceDeleteModel
            {
                Id = p.Id,
                Name = p.Name,
            }).FirstOrDefault();
    }
}
