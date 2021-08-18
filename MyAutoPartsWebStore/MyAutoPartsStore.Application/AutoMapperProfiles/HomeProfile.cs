namespace MyAutoPartsStore.Services.AutoMapperProfiles
{
    using AutoMapper;
    using MyAutoPartsStore.Data.Models;
    using MyAutoPartsStore.Models.ServiceModels.Products;

    public class HomeProfile : Profile
    {
        public HomeProfile()
        {
            this.CreateMap<Product, ProductServiceModel>()
                .ForMember(p => p.Category, cfg => cfg.MapFrom(p => p.Category.Name));
        }
    }
}
