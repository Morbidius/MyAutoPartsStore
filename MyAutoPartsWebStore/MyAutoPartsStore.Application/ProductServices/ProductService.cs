namespace MyAutoPartsStore.Services.ProductServices
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MyAutoPartsStore.Data;
    using MyAutoPartsStore.Data.Models;
    using MyAutoPartsStore.Models.ServiceModels.Products;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductService : IProductService
    {
        private readonly MyAutoPartsStoreDbContext data;
        private readonly IConfigurationProvider mapper;

        public ProductService(MyAutoPartsStoreDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

        public ProductQueryServiceModel All(string name = null, string searchTerm = null, bool isAllowed = true)
        {
            var productsQuery = this.data.Products
                .Where(p => !isAllowed || p.IsAllowed);

            if (!string.IsNullOrWhiteSpace(name))
            {
                productsQuery = productsQuery.Where(p => p.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productsQuery = productsQuery
                    .Where(p => p.Name.ToLower().Contains(searchTerm.Trim().ToLower()));
            }

            var totalProducts = productsQuery.Count();

            var products = GetProducts(productsQuery);

            return new ProductQueryServiceModel
            {
                TotalProducts = totalProducts,
                Products = products,
            };
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
                IsAllowed = false,
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
            productData.IsAllowed = false;

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

        private IEnumerable<ProductServiceModel> GetProducts(IQueryable<Product> productQuery)
           => productQuery
            .ProjectTo<ProductServiceModel>(this.mapper)
            .ToList();

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
            .ProjectTo<ProductServiceCategoryModel>(this.mapper)
            .ToList();

        public IList<ProductServiceModel> GetCategory()
            => this.data
                   .Products
                   .Where(p => p.IsAllowed == true)
                   .OrderByDescending(p => p.Name)
                   .ProjectTo<ProductServiceModel>(this.mapper)
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

        public void Approve(int id)
        {
            var product = this.data.Products.Find(id);

            product.IsAllowed = !product.IsAllowed;

            this.data.SaveChanges();
        }
    }
}
