namespace MyAutoPartsWebStore.Web.Infrastructure
{
    using AutoMapper;
    using Microsoft.Extensions.DependencyInjection;
    using MyAutoPartsStore.Services.AutoMapperProfiles;

    public static class ServiceBuilderExtentions
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProductProfile());
                mc.AddProfile(new HomeProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
