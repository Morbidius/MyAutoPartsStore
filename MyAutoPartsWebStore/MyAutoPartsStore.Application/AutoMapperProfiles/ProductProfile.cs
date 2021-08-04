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
        }

        //context.Products.Include(x => x.Category).Where(x => x.Id == givenidhere)
    }
}
