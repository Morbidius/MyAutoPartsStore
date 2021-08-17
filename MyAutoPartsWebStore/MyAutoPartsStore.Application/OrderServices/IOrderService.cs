namespace MyAutoPartsStore.Services.OrderServices
{
    using System.Collections.Generic;
    using MyAutoPartsStore.Models.ServiceModels.Orders;

    public interface IOrderService
    {
        IEnumerable<ShoppingCartServiceModel> getCart(int? productId);

        bool AddProductToUserCart(string userId, int? productId);

        int GetUserShoppingCartProductsCount(string userId);
    }
}
