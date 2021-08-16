namespace MyAutoPartsStore.Services.OrderServices
{
    using MyAutoPartsStore.Models.ServiceModels.Orders;

    public interface IOrderService
    {
        ShoppingCartServiceModel Cart();
    }
}
