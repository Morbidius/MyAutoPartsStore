namespace MyAutoPartsStore.Services.OrderServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MyAutoPartsStore.Models.ServiceModels.Orders;

    public interface IOrderService
    {
        bool AddProductToUserCart(string userId, int? productId);

        bool RemoveFromCart(int? productId, string userId);

        int CheckoutFormToDealer(
            string email,
            string phoneNumber,
            string address,
            string Note,
            string userId);

        IEnumerable<ShoppingCartServiceModel> GetCart(string userId);

        int GetUserShoppingCartProductsCount(string userId);

        bool SaveCart(string userId, int productId, int productQuantity);

        decimal GetFinalPrice(string userId);

        Task CreateOrder(DealerOrderFormServiceModel order, string userId);

        IEnumerable<DealerOrderFormServiceModel> GetOrderedItemsFromDealer(int userId);
    }
}
