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
    using System.Threading.Tasks;

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

        public bool RemoveFromCart(int? productId, string userId)
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

        public bool SaveCart(string userId, int productId, int productQuantity)
        {
            var userProduct = data.ShoppingCarts.FirstOrDefault(x => x.UserId == userId && x.ProductId == productId);

            if (userProduct == null)
            {
                return false;
            }

            userProduct.Quantity = productQuantity;

            data.SaveChanges();

            return true;
        }

        public decimal GetFinalPrice(string userId)
            => data.ShoppingCarts
            .Where(x => x.UserId == userId)
            .Include(x => x.Product)
            .Sum(x => x.Product.Price * x.Quantity);

        public async Task CreateOrder(DealerOrderFormServiceModel order, string userId)
        {
            var userShoppingCart = GetCart(userId);

            var newlyOrder = new Order()
            {
                UserId = userId,
                BuyerPhone = order.BuyerPhone,
                BuyerAddress = order.BuyerAddress,
                Note = order.Note
            };

            data.Orders.Add(newlyOrder);

            await data.SaveChangesAsync();

            foreach (var product in userShoppingCart)
            {
                var newlyProduct = new OrderProducts()
                {
                    OrderId = newlyOrder.Id,
                    ProductId = product.ProductId,
                    Quantity = product.Quantity
                };

                data.OrderProducts.Add(newlyProduct);
            }
            await data.SaveChangesAsync();

            await DeleteUserCart(userId);
        }

        private async Task DeleteUserCart(string userId)
        {
            var shoppingCart = data.ShoppingCarts.Where(x => x.UserId == userId).ToList();

            foreach (var product in shoppingCart)
            {
                data.ShoppingCarts.Remove(product);
            }
                await data.SaveChangesAsync();
        }
    }
}