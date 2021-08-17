namespace MyAutoPartsStore.Services.OrderServices
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using System.Collections.Generic;
    using System.Linq;
    using MyAutoPartsStore.Data;
    using MyAutoPartsStore.Data.Models;
    using MyAutoPartsStore.Models.ServiceModels.Orders;
    using Microsoft.EntityFrameworkCore;
    using System;

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

        public bool RemoveFromCart(int? productId,string userId)
        {
            var product = this.data.ShoppingCarts.FirstOrDefault(x => x.UserId == userId && x.ProductId == productId);

            if (product == null)
            {
                return false;
            }

            this.data.ShoppingCarts.Remove(product);
            this.data.SaveChanges();

            return true;
        }

        public int CheckoutFormToDealer(string email, string phoneNumber, string address, string Note, string userId)
        {
            var newOrder = new Order
            {
                BuyerPhone = phoneNumber,
                BuyerAddress = address,
                Note = Note,
                UserId = userId,
                OrderedOn = DateTime.UtcNow,
            };

            this.data.Orders.Add(newOrder);
            this.data.SaveChanges();

            return newOrder.Id;
        }

        public IEnumerable<ShoppingCartServiceModel> GetCart(string userId)
            => this.data
                .ShoppingCarts
                .Where(x => x.UserId == userId)
                .Include(x => x.Product)
                .ProjectTo<ShoppingCartServiceModel>(mapper.ConfigurationProvider);

        public int GetUserShoppingCartProductsCount(string userId)
            => data.ShoppingCarts.Count(x => x.UserId == userId);
    }
}
