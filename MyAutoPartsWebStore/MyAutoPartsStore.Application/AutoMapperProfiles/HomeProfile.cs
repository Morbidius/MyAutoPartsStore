namespace MyAutoPartsStore.Services.AutoMapperProfiles
{
    using AutoMapper;
    using MyAutoPartsStore.Data.Models;
    using MyAutoPartsWebStore.Web.Models.Products;

    public class HomeProfile : Profile
    {
        public HomeProfile()
        {
            this.CreateMap<Product, ProductListingViewModel>()
                .ForMember(p => p.Category, cfg => cfg.MapFrom(p => p.Category.Name));
        }
    }
}
