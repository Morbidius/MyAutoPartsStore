namespace MyAutoPartsStore.Tests.Mocks
{
    using AutoMapper;
    using Moq;
    using MyAutoPartsStore.Services.AutoMapperProfiles;

    public static class MapperMock
    {
        public static IMapper Instance
        {
            get
            {
                var mapperConfiguration = new MapperConfiguration(config =>
                {
                    config.AddProfile<HomeProfile>();
                });

                return new Mapper(mapperConfiguration);
            }
        }
    }
}
