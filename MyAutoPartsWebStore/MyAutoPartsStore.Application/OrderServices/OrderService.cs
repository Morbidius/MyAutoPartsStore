namespace MyAutoPartsStore.Services.OrderServices
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using System.Collections.Generic;
    using System.Linq;
    using MyAutoPartsStore.Data;
    using MyAutoPartsStore.Data.Models;
    using MyAutoPartsStore.Models.ServiceModels.Orders;

    public class OrderService : IOrderService
    {
        private readonly MyAutoPartsStoreDbContext data;
        private readonly IMapper mapper;

        public OrderService(MyAutoPartsStoreDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }


        public bool AddProductToUserCart(string userId, int? productId)
        {
            var product = data.Products.FirstOrDefault(x => x.Id == productId);

            if (product == null) return false;


            var shoppingCart = new ShoppingCart
            {
                ProductId = (int)productId,
                UserId = userId,
                Quantity = 1
            };

            data.ShoppingCarts.Add(shoppingCart);
            data.SaveChanges();

            return true;
        }

        public IEnumerable<ShoppingCartServiceModel> getCart(int? productId)
        {
            var productsQuery = this.data
                .ShoppingCarts
                .Where(x => x.ProductId == productId);

            var products = GetProducts(productsQuery);

            return products;
        }

        public int GetUserShoppingCartProductsCount(string userId)
            => data.ShoppingCarts.Count(x => x.UserId == userId);

        private IEnumerable<ShoppingCartServiceModel> GetProducts(IQueryable<ShoppingCart> getCartQuery)
           => getCartQuery
                   .ProjectTo<ShoppingCartServiceModel>(this.mapper.ConfigurationProvider)
                   .ToList();
    }
}
