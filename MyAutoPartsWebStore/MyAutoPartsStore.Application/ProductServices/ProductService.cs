namespace MyAutoPartsStore.Services.ProductServices
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MyAutoPartsStore.Data;
    using MyAutoPartsStore.Data.Models;
    using MyAutoPartsStore.Models;
    using MyAutoPartsStore.Models.BaseModels;
    using MyAutoPartsStore.Models.ServiceModels.Products;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductService : IProductService
    {
        private readonly MyAutoPartsStoreDbContext data;
        private readonly IMapper mapper;

        public ProductService(MyAutoPartsStoreDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public IEnumerable<ProductServiceModel> All(
            string searchTerm = null,
            ProductSorting sorting = ProductSorting.DateCreated,
            bool isAllowed = true)
        {
            var productsQuery = this.data.Products
                .Where(p => !isAllowed || p.IsAllowed);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productsQuery = productsQuery
                    .Where(p => p.Name.ToLower().Contains(searchTerm.Trim().ToLower()));
            }

            productsQuery = sorting switch
            {
                ProductSorting.DateCreated or _ => productsQuery.OrderByDescending(c => c.Id)
            };

            var products = GetProducts(productsQuery);

            return products;
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

        public bool Delete(int? productId)
        {
            var product = this.data.Products.Find(productId);

            if (product == null)
            {
                return false;
            }

            this.data.Remove(product);
            this.data.SaveChanges();

            return true;
        }

        public ProductServiceDetailsModel Details(int? id = null)
            => this.data
                   .Products
                   .Where(p => p.Id == id)
                   .ProjectTo<ProductServiceDetailsModel>(this.mapper.ConfigurationProvider)
                   .FirstOrDefault();

        private IEnumerable<ProductServiceModel> GetProducts(IQueryable<Product> productQuery)
           => productQuery
                   .ProjectTo<ProductServiceModel>(this.mapper.ConfigurationProvider)
                   .ToList();

        public IEnumerable<TModel> ProductSearch<TModel>(string SearchTerm)
            where TModel : INameModel
            => this.data.Products
                   .Where(p => p.Name.ToLower().Contains(SearchTerm.Trim().ToLower()))
                   .OrderByDescending(p => p.Name)
                   .ProjectTo<TModel>(this.mapper.ConfigurationProvider)
                   .ToList();

        public bool isByDealer(int productId, int dealerId)
            => this.data
                   .Products
                   .Any(p => p.Id == productId && p.DealerId == dealerId);

        public IEnumerable<ProductServiceModel> ProductByUser(string userId)
            => GetProducts(this.data
                   .Products
                   .Where(p => p.Dealer.UserId == userId));

        public ProductServiceDeleteModel GetProductName()
            => this.data
                   .Products
                   .ProjectTo<ProductServiceDeleteModel>(this.mapper.ConfigurationProvider)
                   .FirstOrDefault();

        public ProductServiceDeleteModel GetProductById(int? productId)
            => this.data
                .Products
                .ProjectTo<ProductServiceDeleteModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefault(p => p.Id == productId);

        public int GetAprovedProductsCount()
            => this.data
                   .Products
                   .Count(x => x.IsAllowed);

        public void Approve(int id)
        {
            var product = this.data.Products.Find(id);

            product.IsAllowed = !product.IsAllowed;

            this.data.SaveChanges();
        }

        public int AllCounts(
            string searchTerm = null,
            bool isAllowed = true)
        {
            var productsQuery = this.data.Products
                .Where(p => !isAllowed || p.IsAllowed);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productsQuery = productsQuery
                    .Where(p => p.Name.ToLower().Contains(searchTerm.Trim().ToLower()));
            }

            var products = GetProducts(productsQuery);

            return products.Count();
        }
    }
}
