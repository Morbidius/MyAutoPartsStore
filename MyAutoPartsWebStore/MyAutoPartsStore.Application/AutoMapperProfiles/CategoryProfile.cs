namespace MyAutoPartsStore.Services.AutoMapperProfiles
{
    using AutoMapper;
    using MyAutoPartsStore.Data.Models;
    using MyAutoPartsStore.Models.ServiceModels.Products;

    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            this.CreateMap<Category, ProductServiceCategoryModel>();

            this.CreateMap<Product, ProductServiceModel>()
                .ForMember(p => p.Category, cfg => cfg.MapFrom(p => p.Category.Name));
        }
    }
}
