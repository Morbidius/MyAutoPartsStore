namespace MyAutoPartsStore.Services.AutoMapperProfiles
{
    using AutoMapper;
    using MyAutoPartsStore.Data.Models;
    using MyAutoPartsStore.Models.ServiceModels.Orders;

    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            this.CreateMap<ShoppingCart, ShoppingCartServiceModel>();
        }
    }
}
