namespace MyAutoPartsWebStore.Web.Infrastructure.Extentions
{
    using AutoMapper;
    using Microsoft.Extensions.DependencyInjection;
    using MyAutoPartsStore.Services;
    using MyAutoPartsStore.Services.AutoMapperProfiles;
    using MyAutoPartsStore.Services.ProductServices;

    public static class ServiceBuilderExtentions
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProductProfile());
                mc.AddProfile(new HomeProfile());
                mc.AddProfile(new CategoryProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddSingleton<Guard>();

            services.AddTransient<IProductService, ProductService>();
        }
    }
}
