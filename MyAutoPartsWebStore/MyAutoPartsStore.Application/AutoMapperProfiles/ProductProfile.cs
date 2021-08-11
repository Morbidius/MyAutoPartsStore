namespace MyAutoPartsStore.Services.AutoMapperProfiles
{
    using AutoMapper;
    using MyAutoPartsStore.Data.Models;
    using MyAutoPartsStore.Models.ServiceModels.Products;
    using MyAutoPartsWebStore.Web.Models.Products;

    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            this.CreateMap<ProductServiceDetailsModel, ProductFormModel>();
            this.CreateMap<Product, ProductServiceQueryModel>();
            this.CreateMap<Product, ProductServiceDeleteModel>()
            .ForMember(p => p.Name, cfg => cfg.MapFrom(p => p.Category.Name))
            .ForMember(p => p.Id, cfg => cfg.MapFrom(p => p.DealerId));

            this.CreateMap<Product, ProductServiceModel>()
                .ForMember(p => p.Category, cfg => cfg.MapFrom(p => p.Category.Name));
        }

        //context.Products.Include(x => x.Category).Where(x => x.Id == givenidhere)
    }
}
